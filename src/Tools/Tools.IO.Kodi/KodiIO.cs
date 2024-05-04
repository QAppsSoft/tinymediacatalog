using Domain;
using Domain.Models.Movie;
using Domain.Models.Multimedia;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Domains;
using Tools.Enums;
using Tools.XML.Interfaces;
using Movie = Tools.IO.Kodi.Models.Movie;

namespace Tools.IO.Kodi;

public class KodiIO(IXmlRead xmlRead, IDbContextFactory<MediaManagerDatabaseContext> databaseContextFactory,
    IMovieContainerManager movieContainerManager, IMovieContainerProvider movieContainerProvider) : IOInterface
{
    private readonly IXmlRead _xmlRead = xmlRead ?? throw new ArgumentNullException(nameof(xmlRead));
    private readonly IDbContextFactory<MediaManagerDatabaseContext> _databaseContextFactory = databaseContextFactory ?? throw new ArgumentNullException(nameof(databaseContextFactory));
    
    public bool ShowInSettings { get; set; } = true;
    public NfoType Type { get; set; } = NfoType.KODI;
    public string IOHandlerName { get; set; } = "KODI";
    public string IOHandlerDescription { get; set; } = "IO handler for KODI";
    public Uri IOHandlerUri { get; set; } = new("https://kodi.tv/");

    public async Task LoadMovieAsync(Guid movieContainerId)
    {
        Movie? movie;
        
        var nfoPath = await GetNfoPathAsync(movieContainerId, _databaseContextFactory).ConfigureAwait(false);
        if (nfoPath is null)
        {
            return;
        }
        
        try
        {
            movie = _xmlRead.ParseXml<Movie>(nfoPath);
        }
        catch (InvalidOperationException _)
        {
            return;
        }
            
        if (movie is null)
        {
            return;
        }

        var dontExist = await MovieContainerDontExistAsync(movieContainerId).ConfigureAwait(false);
        if (dontExist)
        {
            return;
        }

        await UpdateSingleInfoAsync(movieContainerId, movie).ConfigureAwait(false);

        // UniqueIds
        await UpdateUniqueIdsAsync(movieContainerId, movie).ConfigureAwait(false);

        // Ratings
        await UpdateRatingsAsync(movieContainerId, movie).ConfigureAwait(false);

        // Cast (actors)
        await UpdateCastAsync(movieContainerId, movie).ConfigureAwait(false);
        
        // Sets
        // Thumb
        // Mpaa
        // Certification
        // Countries
        // Watched
        // Play count
        // Genres
        // Studios
        // Tags
        // Trailer
        // Fileinfo
    }

    public Task SaveMovieAsync(Guid movieContainerId)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> MovieContainerDontExistAsync(Guid movieContainerId)
    {
        var exist = await movieContainerProvider.ExistAsync(movieContainerId).ConfigureAwait(false);
        return !exist;
    }

    private async Task UpdateSingleInfoAsync(Guid movieContainerId, Movie movie) =>
        await movieContainerManager.UpdateAsync(movieContainerId, movieContainer =>
            movieContainer.SetTitle(movie.Title)
                .SetOriginalTitle(movie.OriginalTitle)
                .SetSlogan(movie.TagLine)
                .SetRuntime(movie.Runtime)
                .SetReleaseDate(movie.Premiered)
                .SetOriginalLanguage(movie.Languages)
                .SetDateAdded(movie.DateAdded)
        ).ConfigureAwait(false);

    private async Task UpdateUniqueIdsAsync(Guid movieContainerId, Movie movie)
    {
        var ids = movie.UniqueIds.ConvertAll(value => (value.Type, value.Text));
        await movieContainerManager.UpdateUniqueIdsAsync(movieContainerId, ids).ConfigureAwait(false);
    }

    private async Task UpdateCastAsync(Guid movieContainerId, Movie movie)
    {
        if (movie.Cast.Count == 0)
        {
            return;
        }

        var castMembers = movie.Cast.ConvertAll(member =>
            new CastMemberDto(member.Name, member.Role, member.TmdbId, member.Thumb, member.Profile));

        await movieContainerManager.UpdateCastAsync(movieContainerId, castMembers).ConfigureAwait(false);
    }

    private async Task UpdateRatingsAsync(Guid movieContainerId, Movie movie)
    {
        if (movie.RatingsContainer is null)
        {
            return;
        }

        if (movie.RatingsContainer.Rating.Count == 0)
        {
            return;
        }

        var ratings = movie.RatingsContainer.Rating.ConvertAll(rating =>
            new RatingDto(rating.Name, rating.Default, rating.Max, rating.Value, rating.Votes));

        await movieContainerManager.UpdateRatingsAsync(movieContainerId, ratings).ConfigureAwait(false);
    }

    private static async Task<string?> GetNfoPathAsync(Guid movieContainerId, IDbContextFactory<MediaManagerDatabaseContext> factory)
    {
        var context = await factory.CreateDbContextAsync().ConfigureAwait(false);
        await using (context.ConfigureAwait(false))
        {
            var nfoFile = await context.Movies
                .Where(movie => movie.Id == movieContainerId)
                .Select(movie => movie.Files)
                .SelectMany(files => files)
                .Where(file => file is NfoFile)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return nfoFile?.FilePath;
        }
    }
}
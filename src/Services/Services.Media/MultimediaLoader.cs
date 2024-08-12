using System.Collections.Immutable;
using System.Reactive.Linq;
using Common.Extensions.Enumerable;
using Domain;
using Domain.Models.Movie;
using Domain.Models.Multimedia;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Media;
using Services.Abstractions.Settings;
using Services.Settings.Models;
using Tools.IO;

namespace Services.Media;

public class MultimediaLoader(
    IDbContextFactory<MediaManagerDatabaseContext> databaseContextFactory,
    IOInterface nfoReader,
    ISetting<GeneralSettings> setting) : IMultimediaLoader
{
    /// <summary>
    /// Load new movie nfo in the database
    /// </summary>
    /// <param name="path">Path to the root movie folder</param>
    /// <returns>Return Guid of the recently loaded movie or <see cref="Guid.Empty"/> if movie already exist in DB</returns>
    public async Task<Guid> LoadMovieAsync(string path)
    {
        var context = await databaseContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (context.ConfigureAwait(false))
        {
            var exist = await context.Movies.AnyAsync(m => m.BasePath == path).ConfigureAwait(false);

            if (exist)
            {
                return Guid.Empty;
            }

            var movie = new MovieContainer { BasePath = path };

            context.Add(movie);
            await context.SaveChangesAsync().ConfigureAwait(false);

            await UpdateFilesAsync(movie, path).ConfigureAwait(false);
            
            await nfoReader.LoadMovieAsync(movie.Id).ConfigureAwait(false);
            
            return movie.Id;
        }
    }

    private async Task UpdateFilesAsync(MovieContainer movie, string path)
    {
        var generalSettings = await setting.Value;
        
        var files = Directory.EnumerateFiles(path).ToImmutableArray();

        var videos = files.Where(file => IsVideo(file, generalSettings.SupportedVideoTypes)).ToImmutableArray();
        var audios = files.Where(file => IsAudio(file, generalSettings.SupportedAudioTypes)).ToImmutableArray();
        var subtitles = files.Where(file => IsSubtitle(file, generalSettings.SupportedSubtitleTypes)).ToImmutableArray();
        var pics = files.Where(IsImage).ToImmutableArray();
        var nfo = files.Where(IsNfo).ToImmutableArray();
        var others = files.RemoveRange(videos, StringComparer.Ordinal)
            .RemoveRange(audios, StringComparer.Ordinal)
            .RemoveRange(subtitles, StringComparer.Ordinal)
            .RemoveRange(pics, StringComparer.Ordinal)
            .RemoveRange(nfo, StringComparer.Ordinal);

        movie.Files
            .AddRange(videos.Select(file => new VideoFile { FilePath = file })) // TODO: Get Video info and others
            .AddRange(audios.Select(file => new AudioFile { FilePath = file }))
            .AddRange(subtitles.Select(file => new SubtitleFile { FilePath = file }))
            .AddRange(pics.Select(file => new ImageFile { FilePath = file }))
            .AddRange(nfo.Select(file => new NfoFile { FilePath = file }))
            .AddRange(others.Select(file => new OtherFile { FilePath = file }));
    }

    private static bool IsNfo(string file)
    {
        var fileExtension = Path.GetExtension(file);
        return string.Equals(fileExtension, ".nfo", StringComparison.Ordinal);
    }

    private static bool IsImage(string file)
    {
        var fileExtension = Path.GetExtension(file);
        return string.Equals(fileExtension, ".jpg", StringComparison.Ordinal);
    }

    private static bool IsSubtitle(string file, IEnumerable<SubtitleType> subtitleTypes)
    {
        var fileExtension = Path.GetExtension(file);
        var videoExtensions = subtitleTypes.Select(x => x.Extension);
        return ContainsExtension(fileExtension, videoExtensions);
    }

    private static bool IsAudio(string file, IEnumerable<AudioType> audioTypes)
    {
        var fileExtension = Path.GetExtension(file);
        var videoExtensions = audioTypes.Select(x => x.Extension);
        return ContainsExtension(fileExtension, videoExtensions);
    }

    private static bool IsVideo(string file, IEnumerable<VideoType> videoTypes)
    {
        var fileExtension = Path.GetExtension(file);
        var videoExtensions = videoTypes.Select(x => x.Extension);
        return ContainsExtension(fileExtension, videoExtensions);
    }

    private static bool ContainsExtension(string fileExtension, IEnumerable<string> baseExtensions)
    {
        return baseExtensions.Any(x => string.Equals(x, fileExtension, StringComparison.Ordinal));
    }

    public Task<Guid> UpdateMovieAsync(string path)
    {
        throw new NotImplementedException();
    }
}
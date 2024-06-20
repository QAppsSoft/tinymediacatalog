using System.Globalization;
using Domain;
using Domain.Models;
using Domain.Models.Movie;
using Domain.Models.Multimedia;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Domains;

namespace Services.Domains;

public class MovieContainerManager(IDbContextFactory<MediaManagerDatabaseContext> databaseContextFactory) : IMovieContainerManager, IMovieContainerProvider
{
    public async Task UpdateAsync(Guid movieContainerId, Action<MovieContainer> update)
    {
        var context = await databaseContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (context.ConfigureAwait(false))
        {
            var movieContainer = await context.Movies
                .FirstAsync(x => x.Id == movieContainerId)
                .ConfigureAwait(false);

            update(movieContainer);

            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    public async Task UpdateUniqueIdsAsync(Guid movieContainerId, IReadOnlyCollection<(string Name, string Id)> uniqueIds)
    {
        if (uniqueIds.Count == 0)
        {
            return;
        }

        var context = await databaseContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (context.ConfigureAwait(false))
        {
            var movieContainer = await context.Movies
                .Include(x => x.UniqueIds)
                .FirstAsync(x => x.Id == movieContainerId)
                .ConfigureAwait(false);
        
            movieContainer.UniqueIds.Clear();
            var newUniqueIds = uniqueIds.Select(value => new UniqueId { Name = value.Name, Id = value.Id });
            foreach (var uniqueId in newUniqueIds)
            {
                await context.AddAsync(uniqueId).ConfigureAwait(false);
                movieContainer.UniqueIds.Add(uniqueId);
            }
        
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    public async Task UpdateRatingsAsync(Guid movieContainerId, IReadOnlyCollection<RatingDto> ratingsCollection)
    {
        if (ratingsCollection.Count == 0)
        {
            return;
        }
        
        var context = await databaseContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (context.ConfigureAwait(false))
        {
            var movieContainer = await context.Movies
                .Include(x => x.Ratings)
                .FirstAsync(x => x.Id == movieContainerId)
                .ConfigureAwait(false);

            movieContainer.Ratings.Clear();
            
            var ratings = ratingsCollection.Select(rating => new Rating
            {
                Name = rating.Name,
                Default = rating.Default,
                Max = rating.Max,
                Value = rating.Value,
                Votes = rating.Votes,
            });


            foreach (var rating in ratings)
            {
                await context.AddAsync(rating).ConfigureAwait(false);
                movieContainer.Ratings.Add(rating);
            }
            
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    public async Task<MovieContainer?> GetAsync(Guid movieContainerId)
    {
        var context = await databaseContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (context.ConfigureAwait(false))
        {
            var movieContainer = await context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == movieContainerId)
                .ConfigureAwait(false);

            return movieContainer;
        }
    }

    public async Task<bool> ExistAsync(Guid movieContainerId)
    {
        var context = await databaseContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (context.ConfigureAwait(false))
        {
            return await context.Movies
                .AsNoTracking()
                .AnyAsync(x => x.Id == movieContainerId)
                .ConfigureAwait(false);
        }
    }

    public async Task<string?> GetNfoPathAsync(Guid movieContainerId)
    {
            var context = await databaseContextFactory.CreateDbContextAsync().ConfigureAwait(false);
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

    public async Task UpdateCastAsync(Guid movieContainerId, IReadOnlyCollection<CastMemberDto> castMembers)
    {
        if (castMembers.Count == 0)
        {
            return;
        }
        
        var context = await databaseContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (context.ConfigureAwait(false))
        {
            var movieContainer = await context.Movies
                .Include(x => x.Cast)
                .FirstAsync(x => x.Id == movieContainerId)
                .ConfigureAwait(false);
            
            var order = 0;
            movieContainer.Cast.Clear();
            foreach (var castMember in castMembers)
            {
                ++order;
                var actor = await GetActorAsync(movieContainer, castMember).ConfigureAwait(false);
                actor.Order = order;
                await context.AddAsync(actor).ConfigureAwait(false);
                movieContainer.Cast.Add(actor);
            }

            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    private async Task<Actor> GetActorAsync(MovieContainer movieModel, CastMemberDto castMember)
    {
        var personId = await GetPersonAsync(castMember).ConfigureAwait(false);
        var actor = new Actor
        {
            MovieContainerId = movieModel.Id,
            PersonId = personId,
            Role = castMember.Role,
        };
        
        return actor;
    }

    private async Task<Guid> GetPersonAsync(CastMemberDto castMember)
    {
        var context = await databaseContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (context.ConfigureAwait(false))
        {
            var existingPerson = await context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.UniqueIds.Any(y =>
                        y.Name == UniqueId.ValidNames.Tmdb &&
                        y.Id == castMember.TmdbId.ToString(CultureInfo.InvariantCulture)))
                .ConfigureAwait(false);

            if (existingPerson is not null)
            {
                return existingPerson.Id;
            }
            
            var newPerson = new Person
            {
                Name = castMember.Name,
                Profile = castMember.Profile,
                Thumb = castMember.Thumb,
                UniqueIds = new List<UniqueId>
                {
                    new()
                    {
                        Id = castMember.TmdbId.ToString(CultureInfo.InvariantCulture),
                        Name = UniqueId.ValidNames.Tmdb,
                    },
                },
            };

            await context.AddAsync(newPerson).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return newPerson.Id;
        }
    }
}
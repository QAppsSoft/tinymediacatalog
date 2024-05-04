using Domain;
using Domain.Models;
using Domain.Models.Movie;
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
                context.Add(uniqueId);
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
                context.Add(rating);
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
}
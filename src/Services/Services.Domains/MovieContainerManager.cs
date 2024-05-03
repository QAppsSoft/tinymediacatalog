using Domain;
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
}
using Domain.Models.Movie;

namespace Services.Abstractions.Domains;

public interface IMovieContainerManager
{
    Task UpdateAsync(Guid movieContainerId, Action<MovieContainer> update);

    Task UpdateUniqueIdsAsync(Guid movieContainerId, IReadOnlyCollection<(string Name, string Id)> uniqueIds);
}
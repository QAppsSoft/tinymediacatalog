using Domain.Models.Movie;

namespace Services.Abstractions.Domains;

public interface IMovieContainerProvider
{
    Task<MovieContainer?> GetAsync(Guid movieContainerId);

    Task<bool> ExistAsync(Guid movieContainerId);
}
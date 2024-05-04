using Domain.Models.Movie;

namespace Services.Abstractions.Domains;

public interface IMovieContainerManager
{
    Task UpdateAsync(Guid movieContainerId, Action<MovieContainer> update);

    Task UpdateUniqueIdsAsync(Guid movieContainerId, IReadOnlyCollection<(string Name, string Id)> uniqueIds);
    
    Task UpdateRatingsAsync(Guid movieContainerId, IReadOnlyCollection<RatingDto> ratingsCollection);

    Task UpdateCastAsync(Guid movieContainerId, IReadOnlyCollection<CastMemberDto> castMembers);
}
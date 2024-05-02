using Domain.Models.Movie;
using Tools.Enums;

namespace Tools.IO;

/// <summary>
/// Interface for all IO handlers
/// </summary>
public interface IOInterface
{
    bool ShowInSettings { get; set; }

    NfoType Type { get; set; }

    string IOHandlerName { get; set; }

    string IOHandlerDescription { get; set; }

    Uri IOHandlerUri { get; set; }

    /// <summary>
    /// Loads the movie.
    /// </summary>
    /// <param name="movieContainerId">The movie id.</param>
    Task LoadMovieAsync(Guid movieContainerId);
    
    /// <summary>
    /// Save the movie.
    /// </summary>
    /// <param name="movieContainerId">The movie id.</param>
    Task SaveMovieAsync(Guid movieContainerId);
}
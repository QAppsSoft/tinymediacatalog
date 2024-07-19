namespace Services.Abstractions.Media;

public interface IMultimediaLoader
{
    /// <summary>
    /// Get a path and load all data from media file and secondary files
    /// </summary>
    /// <param name="path"></param>
    /// <returns>Returns a Guid representing the item index in the DataBase</returns>
    Task<Guid> LoadMovieAsync(string path);

    /// <summary>
    /// Get a path and update all data from media file and secondary files
    /// </summary>
    /// <param name="path"></param>
    /// <returns>Returns a Guid representing the item index in the DataBase</returns>
    Task<Guid> UpdateMovieAsync(string path);
}
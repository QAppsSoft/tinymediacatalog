namespace Services.Abstractions.Media;

public interface IMediaEnumerator
{
    IEnumerable<string> GetMovies(string movieContainerPath);
}
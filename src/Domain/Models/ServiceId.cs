namespace Domain.Models;

public abstract class ServiceId
{
    // anidb
    // imdb
    // moviemeter
    // mpdbtv
    // ofdb
    // omdbapi
    // tmdb
    // trakt
    // tvdb
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
}
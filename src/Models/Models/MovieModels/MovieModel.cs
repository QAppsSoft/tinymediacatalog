using Models.Common;

namespace Models.MovieModels;

public class MovieModel
{
    public string NfoPathOnDisk { get; set; }
    
    public string Title { get; set; }

    public string OriginalTitle { get; set; }

    public string SortTitle { get; set; }

    public string EpBookmark { get; set; }

    public string Year { get; set; }

    public RatingsModel Ratings { get; set; }

    public string UserRating { get; set; }

    public string Top250 { get; set; }

    public SetModel Set { get; set; }

    public string Plot { get; set; }

    public string Outline { get; set; }

    public string Tagline { get; set; }

    public string Runtime { get; set; }

    public ThumbModel Thumb { get; set; }

    public string Mpaa { get; set; }

    public string Certification { get; set; }

    public string Id { get; set; }

    public string Tmdbid { get; set; }

    public List<UniqueIdModel> UniqueId { get; set; }

    public List<string> Country { get; set; }

    public string Status { get; set; }

    public string Code { get; set; }

    public string Premiered { get; set; }

    public string Watched { get; set; }

    public string PlayCount { get; set; }

    public List<string> Genre { get; set; }

    public List<string> Studio { get; set; }

    public List<string> Tag { get; set; }

    public List<ActorModel> Cast { get; set; }

    public string Trailer { get; set; }

    public string Languages { get; set; }

    public string DateAdded { get; set; }

    public string LockData { get; set; }

    public FileinfoModel FileInfo { get; set; }

    public string Source { get; set; }

    public string Edition { get; set; }

    public string OriginalFilename { get; set; }

    public string UserNote { get; set; }
}
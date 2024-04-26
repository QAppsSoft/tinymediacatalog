using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "movie")]
public class Movie
{
    private List<Actor> _cast = [];
    private string _certification = string.Empty;
    private string _code = string.Empty;
    private List<string> _country = [];
    private string _dateAdded = string.Empty;
    private string _edition = string.Empty;
    private string _epBookmark = string.Empty;
    private List<string> _genre = [];
    private string _id = string.Empty;
    private string _languages = string.Empty;
    private string _lockData = string.Empty;
    private string _mpaa = string.Empty;
    private string _originalFilename = string.Empty;
    private string _originalTitle = string.Empty;
    private string _outline = string.Empty;
    private string _playCount = string.Empty;
    private string _plot = string.Empty;
    private string _premiered = string.Empty;
    private string _sortTitle = string.Empty;
    private string _source = string.Empty;
    private string _status = string.Empty;
    private List<string> _studio = [];
    private List<string> _tag = [];
    private string _tagLine = string.Empty;
    private string _title = string.Empty;
    private string _tmdbId = string.Empty;
    private string _trailer = string.Empty;
    private List<UniqueId> _uniqueIds = [];
    private string _userNote = string.Empty;
    private string _watched = string.Empty;

    [AllowNull]
    [XmlElement(ElementName = "title")]
    public string Title
    {
        get => _title;
        set => _title = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "originaltitle")]
    public string OriginalTitle
    {
        get => _originalTitle;
        set => _originalTitle = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "sorttitle")]
    public string SortTitle
    {
        get => _sortTitle;
        set => _sortTitle = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "epbookmark")]
    public string EpBookmark
    {
        get => _epBookmark;
        set => _epBookmark = value ?? string.Empty;
    }

    [XmlElement(ElementName = "year")]
    public int Year { get; set; }

    [XmlElement(ElementName = "ratings")]
    public RatingsContainer? RatingsContainer { get; set; }

    [XmlElement(ElementName = "userrating")]
    public int UserRating { get; set; }

    [XmlElement(ElementName = "top250")]
    public int Top250 { get; set; }

    [XmlElement(ElementName = "set")]
    public SetContainer? Set { get; set; }

    [AllowNull]
    [XmlElement(ElementName = "plot")]
    public string Plot
    {
        get => _plot;
        set => _plot = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "outline")]
    public string Outline
    {
        get => _outline;
        set => _outline = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "tagline")]
    public string TagLine
    {
        get => _tagLine;
        set => _tagLine = value ?? string.Empty;
    }

    [XmlElement(ElementName = "runtime")]
    public int Runtime { get; set; }

    [XmlElement(ElementName = "thumb")]
    public Thumb? Thumb { get; set; }

    [AllowNull]
    [XmlElement(ElementName = "mpaa")]
    public string Mpaa
    {
        get => _mpaa;
        set => _mpaa = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "certification")]
    public string Certification
    {
        get => _certification;
        set => _certification = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "id")]
    public string Id
    {
        get => _id;
        set => _id = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "tmdbid")]
    public string TmdbId
    {
        get => _tmdbId;
        set => _tmdbId = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "uniqueid")]
    public List<UniqueId> UniqueIds
    {
        get => _uniqueIds;
        set => _uniqueIds = value ?? [];
    }

    [AllowNull]
    [XmlElement(ElementName = "country")]
    public List<string> Country
    {
        get => _country;
        set => _country = value ?? [];
    }

    [AllowNull]
    [XmlElement(ElementName = "status")]
    public string Status
    {
        get => _status;
        set => _status = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "code")]
    public string Code
    {
        get => _code;
        set => _code = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "premiered")]
    public string Premiered
    {
        get => _premiered;
        set => _premiered = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "watched")]
    public string Watched
    {
        get => _watched;
        set => _watched = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "playcount")]
    public string PlayCount
    {
        get => _playCount;
        set => _playCount = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "genre")]
    public List<string> Genre
    {
        get => _genre;
        set => _genre = value ?? [];
    }

    [AllowNull]
    [XmlElement(ElementName = "studio")]
    public List<string> Studio
    {
        get => _studio;
        set => _studio = value ?? [];
    }

    [AllowNull]
    [XmlElement(ElementName = "tag")]
    public List<string> Tag
    {
        get => _tag;
        set => _tag = value ?? [];
    }

    [AllowNull]
    [XmlElement(ElementName = "actor")]
    public List<Actor> Cast
    {
        get => _cast;
        set => _cast = value ?? [];
    }

    [AllowNull]
    [XmlElement(ElementName = "trailer")]
    public string Trailer
    {
        get => _trailer;
        set => _trailer = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "languages")]
    public string Languages
    {
        get => _languages;
        set => _languages = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "dateadded")]
    public string DateAdded
    {
        get => _dateAdded;
        set => _dateAdded = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "lockdata")]
    public string LockData
    {
        get => _lockData;
        set => _lockData = value ?? string.Empty;
    }

    [XmlElement(ElementName = "fileinfo")]
    public FileInfo? Fileinfo { get; set; }

    [AllowNull]
    [XmlElement(ElementName = "source")]
    public string Source
    {
        get => _source;
        set => _source = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "edition")]
    public string Edition
    {
        get => _edition;
        set => _edition = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "original_filename")]
    public string OriginalFilename
    {
        get => _originalFilename;
        set => _originalFilename = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "user_note")]
    public string UserNote
    {
        get => _userNote;
        set => _userNote = value ?? string.Empty;
    }
}
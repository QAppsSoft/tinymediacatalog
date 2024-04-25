using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName="movie")]
public class Movie {
	[XmlElement(ElementName="title")]
	public string Title { get; set; }
	[XmlElement(ElementName="originaltitle")]
	public string OriginalTitle { get; set; }
	[XmlElement(ElementName="sorttitle")]
	public string SortTitle { get; set; }
	[XmlElement(ElementName="epbookmark")]
	public string EpBookmark { get; set; }
	[XmlElement(ElementName="year")]
	public int Year { get; set; }
	[XmlElement(ElementName="ratings")]
	public RatingsContainer RatingsContainer { get; set; }
	[XmlElement(ElementName="userrating")]
	public int UserRating { get; set; }
	[XmlElement(ElementName="top250")]
	public int Top250 { get; set; }
	[XmlElement(ElementName="set")]
	public SetContainer Set { get; set; }
	[XmlElement(ElementName="plot")]
	public string Plot { get; set; }
	[XmlElement(ElementName="outline")]
	public string Outline { get; set; }
	[XmlElement(ElementName="tagline")]
	public string TagLine { get; set; }
	[XmlElement(ElementName="runtime")]
	public int Runtime { get; set; }
	[XmlElement(ElementName="thumb")]
	public Thumb Thumb { get; set; }
	[XmlElement(ElementName="mpaa")]
	public string Mpaa { get; set; }
	[XmlElement(ElementName="certification")]
	public string Certification { get; set; }
	[XmlElement(ElementName="id")]
	public string Id { get; set; }
	[XmlElement(ElementName="tmdbid")]
	public string Tmdbid { get; set; }
	[XmlElement(ElementName="uniqueid")]
	public List<Uniqueid> UniqueIds { get; set; }
	[XmlElement(ElementName="country")]
	public List<string> Country { get; set; }
	[XmlElement(ElementName="status")]
	public string Status { get; set; }
	[XmlElement(ElementName="code")]
	public string Code { get; set; }
	[XmlElement(ElementName="premiered")]
	public string Premiered { get; set; }
	[XmlElement(ElementName="watched")]
	public string Watched { get; set; }
	[XmlElement(ElementName="playcount")]
	public string PlayCount { get; set; }
	[XmlElement(ElementName="genre")]
	public List<string> Genre { get; set; }
	[XmlElement(ElementName="studio")]
	public List<string> Studio { get; set; }
	[XmlElement(ElementName="tag")]
	public List<string> Tag { get; set; }
	[XmlElement(ElementName="actor")]
	public List<Actor> Cast { get; set; }
	[XmlElement(ElementName="trailer")]
	public string Trailer { get; set; }
	[XmlElement(ElementName="languages")]
	public string Languages { get; set; }
	[XmlElement(ElementName="dateadded")]
	public string DateAdded { get; set; }
	[XmlElement(ElementName="lockdata")]
	public string LockData { get; set; }
	[XmlElement(ElementName="fileinfo")]
	public Fileinfo Fileinfo { get; set; }
	[XmlElement(ElementName="source")]
	public string Source { get; set; }
	[XmlElement(ElementName="edition")]
	public string Edition { get; set; }
	[XmlElement(ElementName="original_filename")]
	public string OriginalFilename { get; set; }
	[XmlElement(ElementName="user_note")]
	public string UserNote { get; set; }
}
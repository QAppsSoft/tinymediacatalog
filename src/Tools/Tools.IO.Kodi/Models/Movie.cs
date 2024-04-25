using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName="movie")]
public class Movie {
	[XmlElement(ElementName="title")]
	public string Title { get; set; }
	[XmlElement(ElementName="originaltitle")]
	public string Originaltitle { get; set; }
	[XmlElement(ElementName="sorttitle")]
	public string Sorttitle { get; set; }
	[XmlElement(ElementName="epbookmark")]
	public string Epbookmark { get; set; }
	[XmlElement(ElementName="year")]
	public int Year { get; set; }
	[XmlElement(ElementName="ratings")]
	public Ratings Ratings { get; set; }
	[XmlElement(ElementName="userrating")]
	public int Userrating { get; set; }
	[XmlElement(ElementName="top250")]
	public int Top250 { get; set; }
	[XmlElement(ElementName="set")]
	public Set Set { get; set; }
	[XmlElement(ElementName="plot")]
	public string Plot { get; set; }
	[XmlElement(ElementName="outline")]
	public string Outline { get; set; }
	[XmlElement(ElementName="tagline")]
	public string Tagline { get; set; }
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
	public int Tmdbid { get; set; }
	[XmlElement(ElementName="uniqueid")]
	public List<Uniqueid> Uniqueid { get; set; }
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
	public string Playcount { get; set; }
	[XmlElement(ElementName="genre")]
	public List<string> Genre { get; set; }
	[XmlElement(ElementName="studio")]
	public List<string> Studio { get; set; }
	[XmlElement(ElementName="tag")]
	public List<string> Tag { get; set; }
	[XmlElement(ElementName="actor")]
	public List<Actor> Actor { get; set; }
	[XmlElement(ElementName="trailer")]
	public string Trailer { get; set; }
	[XmlElement(ElementName="languages")]
	public string Languages { get; set; }
	[XmlElement(ElementName="dateadded")]
	public string Dateadded { get; set; }
	[XmlElement(ElementName="lockdata")]
	public string Lockdata { get; set; }
	[XmlElement(ElementName="fileinfo")]
	public Fileinfo Fileinfo { get; set; }
	[XmlElement(ElementName="source")]
	public string Source { get; set; }
	[XmlElement(ElementName="edition")]
	public string Edition { get; set; }
	[XmlElement(ElementName="original_filename")]
	public string Original_filename { get; set; }
	[XmlElement(ElementName="user_note")]
	public string User_note { get; set; }
}
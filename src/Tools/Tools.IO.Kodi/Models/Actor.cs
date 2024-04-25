using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName="actor")]
public class Actor {
    [XmlElement(ElementName="name")]
    public string Name { get; set; }
    [XmlElement(ElementName="role")]
    public string Role { get; set; }
    [XmlElement(ElementName="thumb")]
    public string Thumb { get; set; }
    [XmlElement(ElementName="profile")]
    public string Profile { get; set; }
    [XmlElement(ElementName="tmdbid")]
    public int Tmdbid { get; set; }
}
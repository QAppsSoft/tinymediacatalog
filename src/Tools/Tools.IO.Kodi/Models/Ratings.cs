using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName="ratings")]
public class Ratings {
    [XmlElement(ElementName="rating")]
    public List<Rating> Rating { get; set; }
}
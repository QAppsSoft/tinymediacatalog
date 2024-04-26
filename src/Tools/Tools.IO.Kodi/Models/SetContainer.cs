using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "set")]
public class SetContainer
{
    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "overview")]
    public string Overview { get; set; }
}
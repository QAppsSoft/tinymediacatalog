using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "uniqueid")]
public class Uniqueid
{
    [XmlAttribute(AttributeName = "default")]
    public string Default { get; set; }

    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }

    [XmlText]
    public string Text { get; set; }
}
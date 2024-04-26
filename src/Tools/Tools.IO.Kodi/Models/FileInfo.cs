using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "fileinfo")]
public class FileInfo
{
    [XmlElement(ElementName = "streamdetails")]
    public StreamDetails? StreamDetails { get; set; }
}
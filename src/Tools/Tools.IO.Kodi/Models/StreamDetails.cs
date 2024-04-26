using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "streamdetails")]
public class StreamDetails
{
    private List<Audio> _audio = [];
    private List<Subtitle> _subtitle = [];

    [AllowNull]
    [XmlElement(ElementName = "video")]
    public Video Video { get; set; }

    [AllowNull]
    [XmlElement(ElementName = "audio")]
    public List<Audio> Audio
    {
        get => _audio;
        set => _audio = value ?? [];
    }

    [AllowNull]
    [XmlElement(ElementName = "subtitle")]
    public List<Subtitle> Subtitle
    {
        get => _subtitle;
        set => _subtitle = value ?? [];
    }
}
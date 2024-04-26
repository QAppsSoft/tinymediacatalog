using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "actor")]
public class Actor
{
    private string _name = string.Empty;
    private string _role = string.Empty;
    private string _thumb = string.Empty;
    private string _profile = string.Empty;

    [AllowNull]
    [XmlElement(ElementName = "name")]
    public string Name
    {
        get => _name;
        set => _name = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "role")]
    public string Role
    {
        get => _role;
        set => _role = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "thumb")]
    public string Thumb
    {
        get => _thumb;
        set => _thumb = value ?? string.Empty;
    }

    [AllowNull]
    [XmlElement(ElementName = "profile")]
    public string Profile
    {
        get => _profile;
        set => _profile = value ?? string.Empty;
    }

    [XmlElement(ElementName = "tmdbid")]
    public int TmdbId { get; set; }
}
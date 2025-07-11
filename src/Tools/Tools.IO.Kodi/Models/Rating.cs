﻿using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Tools.IO.Kodi.Models;

[XmlRoot(ElementName = "rating")]
public class Rating
{
    private string _name = string.Empty;

    [XmlElement(ElementName = "value")]
    public double Value { get; set; }

    [XmlElement(ElementName = "votes")]
    public int Votes { get; set; }

    [XmlAttribute(AttributeName = "default")]
    public bool Default { get; set; }

    [XmlAttribute(AttributeName = "max")]
    public int Max { get; set; }

    [AllowNull]
    [XmlAttribute(AttributeName = "name")]
    public string Name
    {
        get => _name;
        set => _name = value ?? string.Empty;
    }
}
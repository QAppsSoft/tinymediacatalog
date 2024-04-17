﻿namespace Domain.Models.Multimedia;

public class AudioFile : MultimediaFile
{
    public override Kind Kind { get; set; } = Kind.Audio;
    public TimeSpan Duration { get; set; }
    public string Codec { get; set; }
    public int BitRate { get; set; }
    public string Language { get; set; }
}
﻿namespace Services.Settings.Models;

public record GeneralSettings(string[] MovieSources, string[] TvShowSources, AudioType[] SupportedAudioTypes, VideoType[] SupportedVideoTypes)
{
    private static readonly AudioType[] DefaultAudios =
    [
        new AudioType(".mp3"),
        new AudioType(".m4a"),
    ];
    
    private static readonly VideoType[] DefaultVideos =
    [
        new VideoType(".mkv"),
        new VideoType(".mp4"),
        new VideoType(".avi"),
        new VideoType(".mpg"),
        new VideoType(".mpeg"),
    ];

    public static GeneralSettings Default { get; } = new([], [], DefaultAudios, DefaultVideos);

    public override int GetHashCode()
    {
        var hashCode = 0;

        foreach (var movieSource in MovieSources)
        {
            hashCode ^= StringComparer.Ordinal.GetHashCode(movieSource);
        }

        foreach (var tvShowSource in TvShowSources)
        {
            hashCode ^= StringComparer.Ordinal.GetHashCode(tvShowSource);
        }

        return hashCode;
    }
}

using System.Reactive.Linq;
using Services.Abstractions.Settings;
using Services.Settings.Models;

namespace Services.Media.Strategy;

public sealed class FileKindSelector
{
    private readonly ISetting<GeneralSettings> _setting;

    public FileKindSelector(ISetting<GeneralSettings> setting)
    {
        _setting = setting;
    }
    
    public async Task<FileKind> SelectAsync(string path)
    {
        var generalSettings = await _setting.Value;
        
        if (IsNfo(path))
        {
            return FileKind.Nfo;
        }

        if (IsVideo(path, generalSettings.SupportedVideoTypes))
        {
            return FileKind.Video;
        }

        if (IsAudio(path, generalSettings.SupportedAudioTypes))
        {
            return FileKind.Audio;
        }

        if (IsSubtitle(path, generalSettings.SupportedSubtitleTypes))
        {
            return FileKind.Subtitle;
        }
        
        if (IsImage(path))
        {
            return FileKind.Audio;
        }
        
        return FileKind.Other;
    }
    
    private static bool IsNfo(string file)
    {
        var fileExtension = Path.GetExtension(file);
        return string.Equals(fileExtension, ".nfo", StringComparison.Ordinal);
    }

    private static bool IsVideo(string file, IEnumerable<VideoType> videoTypes)
    {
        var fileExtension = Path.GetExtension(file);
        var videoExtensions = videoTypes.Select(x => x.Extension);
        return ContainsExtension(fileExtension, videoExtensions);
    }

    private static bool IsAudio(string file, IEnumerable<AudioType> audioTypes)
    {
        var fileExtension = Path.GetExtension(file);
        var videoExtensions = audioTypes.Select(x => x.Extension);
        return ContainsExtension(fileExtension, videoExtensions);
    }

    private static bool IsSubtitle(string file, IEnumerable<SubtitleType> subtitleTypes)
    {
        var fileExtension = Path.GetExtension(file);
        var videoExtensions = subtitleTypes.Select(x => x.Extension);
        return ContainsExtension(fileExtension, videoExtensions);
    }

    private static bool IsImage(string file)
    {
        var fileExtension = Path.GetExtension(file);
        return string.Equals(fileExtension, ".jpg", StringComparison.Ordinal);
    }

    private static bool ContainsExtension(string fileExtension, IEnumerable<string> baseExtensions)
    {
        return baseExtensions.Any(x => string.Equals(x, fileExtension, StringComparison.Ordinal));
    }
}
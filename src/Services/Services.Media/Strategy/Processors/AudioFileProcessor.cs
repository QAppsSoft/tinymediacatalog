﻿using Domain.Models.Multimedia;
using FFMpegCore;

namespace Services.Media.Strategy.Processors;

public class AudioFileProcessor : NoMediaFileBase<AudioFile>
{
    public override FileKind MediaKind { get; } = FileKind.Audio;
    
    public override async Task<MultimediaFile> PrepareAsync(string path)
    {
        var mediaInfo = await FFProbe.AnalyseAsync(path).ConfigureAwait(false);

        var file = GetBasicData(path);

        if (mediaInfo.PrimaryAudioStream != null)
        {
            var audio = mediaInfo.PrimaryAudioStream;

            file.BitRate = audio.BitRate;
            file.Codec = audio.CodecName;
            file.Duration = audio.Duration;
            file.Language = audio.Language ?? string.Empty;
        }

        return file;
    }
}
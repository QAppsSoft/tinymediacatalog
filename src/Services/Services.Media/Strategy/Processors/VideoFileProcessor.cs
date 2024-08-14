using System.Globalization;
using Common.Extensions.Enumerable;
using Domain.Models.Multimedia;
using FFMpegCore;
using AudioStream = Domain.Models.Multimedia.AudioStream;
using SubtitleStream = Domain.Models.Multimedia.SubtitleStream;

namespace Services.Media.Strategy.Processors;

public class VideoFileProcessor : NoMediaFileBase<VideoFile>
{
    public override FileKind MediaKind { get; } = FileKind.Video;
    
    public override async Task<MultimediaFile> PrepareAsync(string path)
    {
        var mediaInfo = await FFProbe.AnalyseAsync(path).ConfigureAwait(false);

        var file = GetBasicData(path);

        if (mediaInfo.PrimaryVideoStream != null)
        {
            var video = mediaInfo.PrimaryVideoStream;

            file.BitRate = video.BitRate;
            file.Codec = video.CodecName;
            file.Duration = video.Duration;
            file.Resolution = new Resolution(video.Width, video.Height);
            file.FrameRate = video.FrameRate.ToString(CultureInfo.InvariantCulture);
        }
        
        // TODO: Get additional audio and subtitle data
            
        var audios = mediaInfo.AudioStreams.Select(audio => new AudioStream
        {
            BitRate = (int)audio.BitRate,
            Channels = audio.Channels.ToString(CultureInfo.InvariantCulture),
            Language = audio.Language ?? string.Empty,
        });

        file.Audios.AddRange(audios);

        var subtitles = mediaInfo.SubtitleStreams.Select(subtitle => new SubtitleStream()
        {
            Format = subtitle.CodecName,
            Language = subtitle.Language ?? string.Empty,
        });

        file.Subtitles.AddRange(subtitles);

        return file;
    }
}
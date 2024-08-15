using Avalonia.Media.Imaging;
using Domain.Models.Multimedia;

namespace Services.Media.Strategy.Processors;

public class ImageFileProcessor : NoMediaFileBase<ImageFile>
{
    public override FileKind MediaKind { get; } = FileKind.Imagen;
    
    public override Task<MultimediaFile> PrepareAsync(string path)
    {
        var image = new Bitmap(path);
        
        var file = GetBasicData(path);

        file.Resolution = new Resolution(image.PixelSize.Width, image.PixelSize.Height);
        
        return Task.FromResult<MultimediaFile>(file);
    }
}
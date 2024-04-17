using System.Runtime.InteropServices;

namespace Domain.Models.Multimedia;

[StructLayout(LayoutKind.Auto)]
public readonly struct Resolution
{
    private static readonly Resolution ZeroResolution = new() { Width = 0, Height = 0 };
    
    public int Width { get; init; }
    public int Height { get; init; }

    public static Resolution Zero() => ZeroResolution;
}
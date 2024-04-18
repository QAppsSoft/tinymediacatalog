using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Multimedia;

[ComplexType]
public record Resolution
{
    private static readonly Resolution ZeroResolution = new() { Width = 0, Height = 0 };
    
    public int Width { get; init; }
    public int Height { get; init; }

    public static Resolution Zero() => ZeroResolution;
}
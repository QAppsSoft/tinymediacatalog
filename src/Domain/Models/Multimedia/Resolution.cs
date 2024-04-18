using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Multimedia;

[ComplexType]
public record Resolution(int Width, int Height)
{
    private static readonly Resolution ZeroResolution = new(0, 0);

    public static Resolution Zero() => ZeroResolution;
}
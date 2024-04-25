using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace MediaManager.Views.Converters;

public static class StaticResourceConverters
{
    /// <summary>
    /// Gets a Converter that takes a string resource key as input and converts it into a DrawingImage
    /// </summary>
    public static FuncValueConverter<string?, DrawingImage?> ToDrawingImage { get; } =
        new(resourceKey =>
        {
            ArgumentNullException.ThrowIfNull(resourceKey);

            return (DrawingImage?)Application.Current?.FindResource(resourceKey);
        });
}
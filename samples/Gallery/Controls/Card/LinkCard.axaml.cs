using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Gallery.Controls;

public class LinkCard : ContentControl
{
    public static readonly StyledProperty<Geometry?> IconDataProperty =
        AvaloniaProperty.Register<LinkCard, Geometry?>(nameof(IconData));

    public Geometry? IconData
    {
        get => GetValue(IconDataProperty);
        set => SetValue(IconDataProperty, value);
    }

    public static readonly StyledProperty<string?> TitleProperty =
        AvaloniaProperty.Register<LinkCard, string?>(nameof(Title));

    public string? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}
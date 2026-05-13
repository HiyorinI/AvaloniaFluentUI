using Avalonia;
using Avalonia.Media;

namespace AvaloniaFluentUI.UI.Controls;

public class ToolButton : Avalonia.Controls.Button
{
    public static readonly StyledProperty<Geometry> IconDataProperty =
        AvaloniaProperty.Register<ToolButton, Geometry?>(nameof(IconData));

    public Geometry? IconData
    {
        get => GetValue(IconDataProperty);
        set => SetValue(IconDataProperty, value);
    }
}

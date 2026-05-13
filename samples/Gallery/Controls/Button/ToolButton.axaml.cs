using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Gallery.Controls;

public class ToolButton : Button
{
    // PathData 属性，存矢量图形
    public static readonly StyledProperty<Geometry?> PathDataProperty =
        AvaloniaProperty.Register<ToolButton, Geometry?>(nameof(PathData));

    // Icon颜色（可根据 IsEnabled 改变颜色）
    public static readonly StyledProperty<IBrush?> IconBrushProperty =
        AvaloniaProperty.Register<ToolButton, IBrush?>(nameof(IconBrush), Brushes.Black);

    public Geometry? PathData
    {
        get => GetValue(PathDataProperty);
        set => SetValue(PathDataProperty, value);
    }

    public IBrush? IconBrush
    {
        get => GetValue(IconBrushProperty);
        set => SetValue(IconBrushProperty, value);
    }
}
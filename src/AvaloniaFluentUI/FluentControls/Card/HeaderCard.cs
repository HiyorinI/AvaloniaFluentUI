using Avalonia;

namespace AvaloniaFluentUI.Controls;

public class HeaderCard : Card
{
    public static readonly StyledProperty<object> HeaderProperty =
        AvaloniaProperty.Register<HeaderCard, object?>(nameof(Header));

    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
}

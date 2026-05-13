using Avalonia;
using Avalonia.Controls;

namespace Gallery.Controls;

public class NavigationButton : Button, INavigationItem
{
    public static readonly StyledProperty<NavigationViewItemPosition> PositionProperty =
        AvaloniaProperty.Register<NavigationButton, NavigationViewItemPosition>(nameof(Position));

    public NavigationViewItemPosition Position
    {
        get => GetValue(PositionProperty);
        set => SetValue(PositionProperty, value);
    }
}
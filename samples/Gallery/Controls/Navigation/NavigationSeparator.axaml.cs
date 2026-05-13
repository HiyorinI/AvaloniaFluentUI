using Avalonia;
using Avalonia.Controls;

namespace Gallery.Controls;

public class NavigationSeparator : Separator, INavigationItem
{
    /// <summary>
    ///     位置属性
    /// </summary>
    public static readonly StyledProperty<NavigationViewItemPosition> PositionProperty =
        NavigationViewItem.PositionProperty.AddOwner<NavigationSeparator>();

    public NavigationViewItemPosition Position
    {
        get => GetValue(PositionProperty);
        set => SetValue(PositionProperty, value);
    }
}
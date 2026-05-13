using System;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Gallery.Controls;

public enum NavigationViewItemPosition
{
    Top,
    Scroll,
    Bottom
}

public class NavigationViewItem : ContentControl, INavigationItem
{
    /// <summary>
    ///     点击命令
    /// </summary>
    public static readonly StyledProperty<ICommand?> CommandProperty =
        AvaloniaProperty.Register<NavigationViewItem, ICommand?>(nameof(Command));

    /// <summary>
    ///     命令参数
    /// </summary>
    public static readonly StyledProperty<object?> CommandParameterProperty =
        AvaloniaProperty.Register<NavigationViewItem, object?>(nameof(CommandParameter));

    /// <summary>
    ///     点击事件
    /// </summary>
    public static readonly RoutedEvent<RoutedEventArgs> ClickEvent =
        RoutedEvent.Register<NavigationViewItem, RoutedEventArgs>(
            nameof(Click), RoutingStrategies.Bubble);

    /// <summary>
    ///     是否选中
    /// </summary>
    public static readonly StyledProperty<bool> IsSelectedProperty =
        AvaloniaProperty.Register<NavigationViewItem, bool>(nameof(IsSelected));

    public static readonly StyledProperty<NavigationViewItemPosition> PositionProperty =
        AvaloniaProperty.Register<NavigationViewItem, NavigationViewItemPosition>(nameof(Position),
            NavigationViewItemPosition.Scroll);

    private bool _isPressed;

    public ICommand? Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public bool IsSelected
    {
        get => GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    /// <summary>
    ///     是否按下状态
    /// </summary>
    private bool IsPressed
    {
        get => _isPressed;
        set
        {
            if (_isPressed != value)
            {
                _isPressed = value;
                PseudoClasses.Set(":pressed", value);
            }
        }
    }

    private bool IsEnter { get; set; }

    public NavigationViewItemPosition Position
    {
        get => GetValue(PositionProperty);
        set => SetValue(PositionProperty, value);
    }

    public event EventHandler<RoutedEventArgs>? Click
    {
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
#if DEBUG
            Debug.WriteLine("OnPointerPressed");
#endif
            IsPressed = true;
            e.Handled = true;
        }
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);

        if (IsPressed && e.InitialPressMouseButton == MouseButton.Left && IsEnter)
        {
            IsPressed = false;

#if DEBUG
            Debug.WriteLine("OnPointerReleased");
#endif
            // 触发点击事件
            RaiseClickEvent();

            // 执行命令
            if (Command?.CanExecute(CommandParameter) == true) Command.Execute(CommandParameter);

            e.Handled = true;
        }
    }

    private void RaiseClickEvent()
    {
        var args = new RoutedEventArgs(ClickEvent);
        RaiseEvent(args);
    }

    protected override void OnPointerEntered(PointerEventArgs e)
    {
        base.OnPointerEntered(e);
        IsEnter = true;
    }

    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        IsEnter = false;
        IsPressed = false;
    }
}
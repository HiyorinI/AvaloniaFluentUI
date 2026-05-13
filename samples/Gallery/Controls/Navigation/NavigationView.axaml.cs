using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Metadata;

namespace Gallery.Controls;

[TemplatePart("PART_TopLayout", typeof(StackPanel))]
[TemplatePart("PART_ScrollLayout", typeof(StackPanel))]
[TemplatePart("PART_BottomLayout", typeof(StackPanel))]
public class NavigationView : ContentControl
{
    public static readonly StyledProperty<AvaloniaList<object>> ItemsProperty =
        AvaloniaProperty.Register<NavigationView, AvaloniaList<object>>(nameof(Items),
            defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    ///     选中的项 - 定义为 Avalonia 属性
    /// </summary>
    public static readonly StyledProperty<NavigationViewItem?> SelectedItemProperty =
        AvaloniaProperty.Register<NavigationView, NavigationViewItem?>(
            nameof(SelectedItem), defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    ///     选中项改变事件
    /// </summary>
    public static readonly RoutedEvent<RoutedEventArgs> SelectedItemChangedEvent =
        RoutedEvent.Register<NavigationView, RoutedEventArgs>(
            nameof(SelectedItemChanged), RoutingStrategies.Bubble);

    private StackPanel? _bottomLayout;

    private StackPanel? _scrollLayout;

    // private AvaloniaList<object> _items = new();
    private StackPanel? _topLayout;

    public NavigationView()
    {
        Items = new AvaloniaList<object>();
        Items.CollectionChanged += OnItemsCollectionChanged;
    }

    [Content]
    public AvaloniaList<object> Items
    {
        get => GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public NavigationViewItem? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public event EventHandler<RoutedEventArgs> SelectedItemChanged
    {
        add => AddHandler(SelectedItemChangedEvent, value);
        remove => RemoveHandler(SelectedItemChangedEvent, value);
    }

    private void UpdateItems()
    {
        if (_topLayout == null || _scrollLayout == null || _bottomLayout == null)
            return;

#if DEBUG
        Debug.WriteLine("Update NavigationView Items");
#endif
        
        // 清空所有面板
        _topLayout.Children.Clear();
        _scrollLayout.Children.Clear();
        _bottomLayout.Children.Clear();

        // 根据 Position 属性将项分配到不同的面板
        foreach (var item in Items)
        {
            if (item is NavigationViewItem navigationItem)
            {
                // 监听点击事件
                navigationItem.Click -= OnNavigationItemClick;
                navigationItem.Click += OnNavigationItemClick;
            }

            if (item is Control control && item is INavigationItem navItem)
                // 根据 Position 属性分配到不同区域
                switch (navItem.Position)
                {
                    case NavigationViewItemPosition.Top:
                        _topLayout.Children.Add(control);
                        break;

                    case NavigationViewItemPosition.Scroll:
                        _scrollLayout.Children.Add(control);
                        break;

                    case NavigationViewItemPosition.Bottom:
                        _bottomLayout.Children.Add(control);
                        break;
                }
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _topLayout = e.NameScope.Find<StackPanel>("PART_TopLayout");
        _scrollLayout = e.NameScope.Find<StackPanel>("PART_ScrollLayout");
        _bottomLayout = e.NameScope.Find<StackPanel>("PART_BottomLayout");
        UpdateItems();
    }

    private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateItems();
    }

    private void OnNavigationItemClick(object? sender, RoutedEventArgs e)
    {
        if (sender is NavigationViewItem item)
        {
            SelectedItem = item;
            RaiseEvent(new RoutedEventArgs(SelectedItemChangedEvent));
        }

        // if (sender is NavigationViewItem clickedItem)
        // {
        // if (SelectedItem != null && SelectedItem != clickedItem)
        // {
        // SelectedItem.IsSelected = false;
        // }

        // SelectedItem = clickedItem;
        // SelectedItem.IsSelected = true;

        // RaiseEvent(new RoutedEventArgs(SelectedItemChangedEvent));
        // }
    }

    // protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    // {
    // base.OnPropertyChanged(change);

    // if (change.Property == SelectedItemProperty)
    // {
    // var oldItem = change.OldValue as NavigationViewItem;
    // var newItem = change.NewValue as NavigationViewItem;

    // if (oldItem != null)
    // {
    // oldItem.IsSelected = false;
    // }

    // if (newItem != null)
    // {
    // newItem.IsSelected = true;
    // }
    // }
    // }

    public void SetCurrentItemByContent(object content)
    {
        var item = Items
            .OfType<NavigationViewItem>()
            .FirstOrDefault(x => Equals(x.Content, content));

        if (item != null)
            SelectedItem = item;
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SelectedItemProperty) UpdateSelectionStates();
    }

    private void UpdateSelectionStates()
    {
        foreach (var item in Items.OfType<NavigationViewItem>()) item.IsSelected = item == SelectedItem;
    }
}
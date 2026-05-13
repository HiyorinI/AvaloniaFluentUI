using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Styling;
using AvaloniaFluentUI.UI.Controls;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gallery.Controls;
using Gallery.Messages.IconViewMessages;
using Gallery.Themes;

namespace Gallery.Views;

public partial class IconsView : UserControl 
{
    private readonly int _iconWidth = 92;
    private readonly int _iconHeight = 92;

    private CheckedBorder? _currentItem;
    // public event Action<IconsView>? OnResizeChanged;

    public IconsView()
    {
#if DEBUG
        Debug.WriteLine("IconsView Init");
#endif
        
        InitializeComponent();
        InitIcons();
        
        InitIcons();
        // Loaded += async (_, _) =>
        // {
            // var target = ScrollViewer.Extent.Height - ScrollViewer.Viewport.Height;
        
            // for (double y = 0; y <= target; y += 12)
            // {
                // ScrollViewer.Offset = ScrollViewer.Offset.WithY(y);
                // await Task.Delay(8);
            // }
        // };
    }

    private void InitIcons()
    {
#if DEBUG
        Console.WriteLine($"InitIcons, Count: {GetAllIcons().Count}");
#endif

        foreach (var icons in GetAllIcons())
        {
            var path = icons.Value;
            var name = icons.Key;

            var iconCard = new CheckedBorder
            {
                Classes = { "IconCard" },
                Width = _iconWidth,
                Height = _iconHeight,
                Child =  new StackPanel
                {
                    Children = { 
                        new PathIcon { Name = "PART_PathIcon", Tag = name, Data = StreamGeometry.Parse(path) }, 
                        new TextBlock { Name = "PART_Name", Text = name }
                    }
                },
                ContextFlyout = new FluentMenuFlyout
                {
                    Items =
                    {
                        new MenuItem
                        {
                            Header = "复制Svg",
                            Command = new RelayCommand(() =>
                            {
                                TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync(path); })
                        }
                    }
                }
            };

            iconCard.PointerReleased += (sender, e) =>
            {
                if (_currentItem != null) _currentItem.IsChecked = false;

                if (sender is CheckedBorder border)
                {
                    var icon = border.FindLogicalDescendantOfType<PathIcon>();
                    if (icon == null) return;
                    
                    _currentItem = border;
                    WeakReferenceMessenger.Default.Send(new CheckedIconChangedMessage((string)icon.Tag!, icon.Data!));
                }

                e.Handled = true;
            };
            
            UniformGrid.Children.Add(iconCard);
        }
    }

    private Dictionary<string, string> GetAllIcons()
    {
        return typeof(FluentIcon)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .ToDictionary(f => f.Name, f => (string)f.GetValue(null)!);
    }

    protected override void OnSizeChanged(SizeChangedEventArgs e)
    {
        base.OnSizeChanged(e);
        // int rows = (int)UniformGrid.Bounds.Height / (_iconHeight + (int)UniformGrid.RowSpacing);
        var columns = (int)UniformGrid.Bounds.Width / (_iconWidth + (int)UniformGrid.ColumnSpacing);

        // UniformGrid.Rows = rows;
        UniformGrid.Columns = columns;
    }

    private void OnToggleThemeClicked(object? sender, RoutedEventArgs e)
    {
        var app = Application.Current;
        if (app != null)
        {
            var theme = app.RequestedThemeVariant == ThemeVariant.Light ? ThemeVariant.Dark : ThemeVariant.Light;
            app.RequestedThemeVariant = theme;
        }
    }
}
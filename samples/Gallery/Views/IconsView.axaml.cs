using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
    private const int BatchSize = 20;
    private readonly int _iconWidth = 92;
    private readonly int _iconHeight = 92;

    private CheckedBorder? _currentItem;

    public IconsView()
    {
#if DEBUG
        Debug.WriteLine("IconsView Init");
#endif

        InitializeComponent();

        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        await LoadIconsAsync();
    }

    private async Task LoadIconsAsync()
    {
        var allIcons = GetAllIcons();
        var iconList = allIcons.ToList();
        var total = iconList.Count;
        var loaded = 0;

        foreach (var chunk in Chunk(iconList, BatchSize))
        {
            foreach (var (name, path) in chunk)
            {
                var iconCard = CreateIconCard(name, path);
                UniformGrid.Children.Add(iconCard);
                loaded++;
            }

            LoadingIndicator.Text = $"正在加载图标... {loaded}/{total}";

            await Task.Delay(1);
        }

        LoadingIndicator.IsVisible = false;
    }

    private static IEnumerable<List<KeyValuePair<string, string>>> Chunk(
        List<KeyValuePair<string, string>> source, int batchSize)
    {
        for (int i = 0; i < source.Count; i += batchSize)
            yield return source.GetRange(i, Math.Min(batchSize, source.Count - i));
    }

    private CheckedBorder CreateIconCard(string name, string path)
    {
        var iconCard = new CheckedBorder
        {
            Classes = { "IconCard" },
            Width = _iconWidth,
            Height = _iconHeight,
            Child = new StackPanel
            {
                Children =
                {
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
                            TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync(path);
                        })
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

        return iconCard;
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
        var columns = (int)UniformGrid.Bounds.Width / (_iconWidth + (int)UniformGrid.ColumnSpacing);
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gallery.Extensions;

namespace Gallery.ViewModels;

public partial class LayoutViewModel : ViewModelBase
{ 
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentBorderBrush))]
    private Brush? _borderBrush = new SolidColorBrush(Colors.DarkOrange);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentBorderBackground))]
    private Brush? _borderBackground = new SolidColorBrush(Colors.Azure);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentBoxShadow))]
    private BoxShadows _boxShadow = new BoxShadows(
        new BoxShadow {Color = Color.Parse("#000000"), Blur = 12, OffsetX = 0, OffsetY = 5, Spread = 0, IsInset = false}
    );

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentBorderThickness))]
    private Thickness _borderThickness = new Thickness(1);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentBorderRadius))]
    private CornerRadius _borderRadius = new CornerRadius(8);

    [ObservableProperty]
    private bool _isEnableBorderShadow = true;

    [ObservableProperty]
    private string? _borderBrushHex;

    [ObservableProperty]
    private string? _borderBackgroundHex;

    [ObservableProperty]
    private string? _borderWidth;

    [ObservableProperty]
    private string? _br;

    public string CurrentBorderBrush => $"BorderBrush: {BorderBrush}";
    public string CurrentBorderBackground => $"BorderBackground: {BorderBackground}";
    public string CurrentBoxShadow => $"BoxShadow: {BoxShadow}";
    public string CurrentBorderThickness => $"BorderThickness: {BorderThickness}";
    public string CurrentBorderRadius => $"BorderRadius: {BorderRadius}";

    [ObservableProperty]
    private double _canvasLeft = 100;

    [ObservableProperty]
    private double _canvasTop = 100;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DockLeftWidth))]
    private string? _inputDockLeftWidth = "100";
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DockRightWidth))]
    private string? _inputDockRightWidth = "100";
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DockTopHeight))]
    private string? _inputDockTopHeight = "64";
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DockBottomHeight))]
    private string? _inputDockBottomHeight = "64";
    
    public double DockLeftWidth => InputDockLeftWidth.ToDoubleOrZero();
    public double DockRightWidth => InputDockRightWidth.ToDoubleOrZero();
    public double DockBottomHeight => InputDockBottomHeight.ToDoubleOrZero();
    public double DockTopHeight => InputDockTopHeight.ToDoubleOrZero();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MenuHorizontalAlignment))]
    private bool _isPaneOpen;
    
    [RelayCommand]
    private void TogglePanel() => IsPaneOpen = !IsPaneOpen;

    [ObservableProperty]
    private Orientation _stackPanelOrientation = Orientation.Vertical;

    [ObservableProperty]
    private int _selectedOrientationIndex;

    [ObservableProperty]
    private ObservableCollection<TabItem> _tabItems =
    [
        new TabItem { Header = "Tab 1", Content = "Tab 1 Interface" },
        new TabItem { Header = "Tab 2", Content = "Tab 2 Interface" },
        new TabItem { Header = "Tab 3", Content = "Tab 3 Interface" },
        new TabItem { Header = "Tab 4", Content = "Tab 4 Interface" },
        new TabItem { Header = "Tab 5", Content = "Tab 5 Interface" },
    ];

    [RelayCommand]
    private void OnAddTabItem()
    {
        TabItems.Add(new TabItem { Header = $"Tab {TabItems.Count + 1}", Content = $"Tab {TabItems.Count + 1} Interface" });
    }

    partial void OnSelectedOrientationIndexChanged(int value)
    {
        StackPanelOrientation = value == 0 ? Orientation.Vertical : Orientation.Horizontal;
    }
    
    partial void OnIsEnableBorderShadowChanged(bool value)
    {
        BoxShadow = value
            ? new BoxShadows(new BoxShadow { Color = Color.Parse("#000000"), Blur = 12, OffsetX = 0, OffsetY = 5, Spread = 0, IsInset = false })
            : new BoxShadows();
    }

    partial void OnBorderBrushHexChanged(string? value)
    {
        if (VerifyColor(value, out var brush)) BorderBrush = brush;
    }

    partial void OnBorderBackgroundHexChanged(string? value)
    {
        if (VerifyColor(value, out var brush)) BorderBackground = brush;
    }

    partial void OnBorderWidthChanged(string? value) => BorderThickness = new Thickness(value.ToDoubleOrZero());

    partial void OnBrChanged(string? value) => BorderRadius = new CornerRadius(value.ToDoubleOrZero());

    private bool VerifyColor(string? hex, out Brush? brush)
    {
        hex = hex?.Trim();
        if (!string.IsNullOrEmpty(hex) && Color.TryParse(hex, out var c))
        {
            brush = new SolidColorBrush(c);
            return true;
        }
        brush = null;
        return false;
    }

    public HorizontalAlignment MenuHorizontalAlignment => IsPaneOpen ? HorizontalAlignment.Right : HorizontalAlignment.Left;

    [ObservableProperty]
    private ObservableCollection<string> _items = new ObservableCollection<string>();

    public LayoutViewModel()
    {
        for (int i = 1; i <= 20; i++)
        {
            Items.Add($"Item: {i}");
        }
        Items.CollectionChanged += (_, _) =>
        {
            ItemCountFormat = $"项目个数: {Items.Count}";
        };
    }

    [ObservableProperty]
    private int[] _addItemCounts = [10, 50, 100, 200, 500, 1000];

    [ObservableProperty]
    private string _itemCountFormat = "项目个数: 20"; 
    
    [ObservableProperty]
    private int _addCount;
    
    [RelayCommand]
    private async Task AddItem()
    {
        int count = Items.Count;
        
        for (int i = count + 1; i <= AddCount + count; i++)
        {
            Items.Add($"Item {i}");

            await Task.Yield();
            await Task.Delay(10);
        }
    }
}
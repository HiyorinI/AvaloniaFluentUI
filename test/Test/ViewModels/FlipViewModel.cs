using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using AvaloniaFluentUI.Controls.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Test.ViewModels;

public partial class FlipViewModel : ViewModelBase
{

    [ObservableProperty]
    private ObservableCollection<string> _images;

    private string _crd = "BC1";

    public FlipViewModel()
    {
        List<string> images = new List<string>();
        foreach (var file in Directory.GetFiles($@"C:\Projects\C#\AvaloniaFluentUi\test\Test\Assets\{_crd}"))
        {
            images.Add(file);
        }
        Images = new ObservableCollection<string>(images);
    }

    [RelayCommand]
    private void ToggleImageSource()
    {
        List<string> images = new List<string>();
        _crd = _crd == "BC1" ? "BC2" : "BC1";
        foreach (var file in Directory.GetFiles($@"C:\Projects\C#\AvaloniaFluentUi\test\Test\Assets\{_crd}"))
        {
            images.Add(file);
        }
        
        Images = new ObservableCollection<string>(images);
    }

    [ObservableProperty]
    private BitmapInterpolationMode _mode = BitmapInterpolationMode.HighQuality;
    
    public BitmapInterpolationMode[] Modes => 
    [
        BitmapInterpolationMode.None,
        BitmapInterpolationMode.Unspecified, 
        BitmapInterpolationMode.LowQuality,
        BitmapInterpolationMode.MediumQuality, 
        BitmapInterpolationMode.HighQuality
    ];

    public int[] Intervals => [600, 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000];
    public FlipOrientation[] Orientations => [FlipOrientation.Horizontal, FlipOrientation.Vertical];
    public Stretch[] Stretchs => [ Stretch.None, Stretch.Fill, Stretch.Uniform, Stretch.UniformToFill];

    [ObservableProperty]
    private int _interval = 600;

    [ObservableProperty]
    private bool _isAutoPlay;

    [ObservableProperty]
    private FlipOrientation _orientation = FlipOrientation.Horizontal;

    [ObservableProperty]
    private double _decodeToHeight = 1024;

    [ObservableProperty]
    private double _decodeToWidth = 1920;

    [ObservableProperty]
    private int _maxVisiblePips = 6;

    [ObservableProperty]
    private Stretch _stretch = Stretch.UniformToFill;
}

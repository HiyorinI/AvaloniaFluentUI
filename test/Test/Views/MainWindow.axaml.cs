using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using AvaloniaFluentUI.Windowing;
using AvaloniaFluentUI.Locale;
using AvaloniaFluentUI.Styling;

namespace Test.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();
        
        // SplashScreen = new MainWindowSplashScreen();
        // EnabledAcrylicBlue(true);
        
        FluentAvaloniaTheme.Instance.ThemeChanged += (_, theme) =>
        {
            Console.WriteLine($"Theme Changed: {theme}");
            Console.WriteLine(Background.ToString());
            
            // Background = Brush.Parse(FluentAvaloniaTheme.Instance.IsDarkTheme ? "#30161616" : "#30F3F3F3");  
        };

        FluentAvaloniaTheme.Instance.ThemeColorChanged += (_, color) =>
        {
            Console.WriteLine($"Theme Color Changed: {color}");
        };

        
    }

    private void OnClicked(object? sender, RoutedEventArgs e)
    {
        FluentAvaloniaTheme.Instance.CurrentAccentColor = GetRandomColor();
    }

    public Color GetRandomColor()
    {
        var random = new Random();
        return Color.Parse($"#{random.Next(256):X2}" +
                           $"{random.Next(256):X2}" +
                           $"{random.Next(256):X2}");
    }

    private void ToggleToDefaultColor(object? sender, RoutedEventArgs e)
    {
        FluentAvaloniaTheme.Instance.CustomAccentColor = null;
    }

    private void OnAcrylicClicked(object? sender, RoutedEventArgs e)
    {
        if (sender is CheckBox cb)
        {
            EnabledAcrylicBlue((bool)cb.IsChecked);
        }
    }
}

class MainWindowSplashScreen : IApplicationSplashScreen
{
    public string AppName => "Test";
    public IImage AppIcon => null;
    public object SplashScreenContent => null; 
    public Task RunTasks(CancellationToken cancellationToken)
    {
        return Task.Delay(1000);
    }

    public int MinimumShowTime => 1000;
}

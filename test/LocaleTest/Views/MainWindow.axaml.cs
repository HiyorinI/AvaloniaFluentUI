using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using AvaloniaFluentUI.Controls.Windowing;
using AvaloniaFluentUI.Locale;
using AvaloniaFluentUI.Styling;

namespace LocaleTest.Views;

public partial class MainWindow : AppWindow 
{
    public MainWindow()
    {
        InitializeComponent();

        Title = LocalizationService.Instance.GetString("Title");

        LocalizationService.Instance.PropertyChanged += (_, _) =>
        {
            SetValue(TitleProperty, LocalizationService.Instance.GetString("Title"));
        };
       
        EnableWindowEffect(true);

        MinWidth = 800;
        MinHeight = 400;
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
    }

    private void OnSearch(string value)
    {
    }
}

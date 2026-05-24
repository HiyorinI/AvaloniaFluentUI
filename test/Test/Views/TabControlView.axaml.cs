using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaFluentUI.Controls;
using test.Test.ViewModels;

namespace Test.Views;

public partial class TabControlView : UserControl
{
    public TabControlView()
    {
        InitializeComponent();
        DataContext = new TabControlViewModel();
    }

    private void ChangeValue(object? sender, RoutedEventArgs e)
    {
        var random = new Random();

        ProgressBar.Value = random.Next(0, 101);
    }

    private void Error(object? sender, RoutedEventArgs e)
    {
        // ToastInfoBar.Instance.Position = ToastPosition.TopLeft;
        // ToastInfoBar.Instance.ShowError("Title", "This is an error");
    }

    private void Info(object? sender, RoutedEventArgs e)
    {
        // ToastInfoBar.Instance.Position = ToastPosition.TopRight;
        // ToastInfoBar.Instance.ShowInfo("Title", "This is an info");
    }

    private void Success(object? sender, RoutedEventArgs e)
    {
        // ToastInfoBar.Instance.Position = ToastPosition.BottomRight;
        // ToastInfoBar.Instance.ShowSuccess("Title", "This is an success");
        
    }

    private void Warning(object? sender, RoutedEventArgs e)
    {
        // ToastInfoBar.Instance.Position = ToastPosition.BottomLeft;
        // ToastInfoBar.Instance.ShowWarning("Title", "This is an warning");
        
    }
}


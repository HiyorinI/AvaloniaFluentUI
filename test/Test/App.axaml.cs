using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaFluentUI.Controls;
using AvaloniaFluentUI.Locale;
using Test.Pages;
using Test.ViewModels;
using Test.Views;

namespace Test;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        
        // Register pages for AOT-compatible navigation
        Frame.RegisterPage<PageA>();
        Frame.RegisterPage<PageB>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel(), };
        }
        

        base.OnFrameworkInitializationCompleted();
    }
}

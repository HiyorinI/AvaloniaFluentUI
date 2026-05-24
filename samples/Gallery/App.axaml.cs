using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaFluentUI.Controls;
using AvaloniaFluentUI.Locale;
using Gallery.Pages;
using Gallery.Services;
using Gallery.ViewModels;
using Gallery.Views;

namespace Gallery;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void InitializeCulture()
    {
        LocalizationService.Instance.AddValues(
            ["zh-CN:Home", "zh-CN:Icon", "zh-CN:BasicInput", "zh-CN:DialogAndPopup", "zh-CN:Layout", "zh-CN:Navigation", "zh-CN:Text", "zh-CN:View", "zh-CN:Scroll", "zh-CN:Setting", "zh-CN:StatusAndInformation", "zh-CN:MenuAndToolBar", "zh-CN:DateTime", "zh-CN:OnlineDocument", "zh-CN:SourceCode"],
            ["主页", "图标", "基本输入", "对话框和弹出窗口", "布局", "导航", "文本", "视图", "滚动", "设置", "状态和信息", "菜单和工具栏", "日期和时间", "在线文档", "源代码"]
            );
        LocalizationService.Instance.AddValues(
            ["en-US:Home", "en-US:Icon", "en-US:BasicInput", "en-US:DialogAndPopup", "en-US:Layout", "en-US:Navigation", "en-US:Text", "en-US:View", "en-US:Scroll", "en-US:Setting", "en-US:StatusAndInformation", "en-US:MenuAndToolBar", "en-US:DateTime", "en-US:OnlineDocument", "en-US:SourceCode"],
            ["Home", "Icon", "Basic Input", "Dialog and Popup", "Layout", "Navigation", "Text", "View", "Scroll", "Settings", "Status and Information", "Menu and ToolBar", "Date and Time", "Online documentation", "Source Code"]
        );

        LocalizationService.Instance.AddValues(
            ["ja-JP:Home", "ja-JP:Icon", "ja-JP:BasicInput", "ja-JP:DialogAndPopup", "ja-JP:Layout", "ja-JP:Navigation", "ja-JP:Text", "ja-JP:View", "ja-JP:Scroll", "ja-JP:Setting", "ja-JP:StatusAndInformation", "ja-JP:MenuAndToolBar", "ja-JP:DateTime", "ja-JP:OnlineDocument", "ja-JP:SourceCode"],
            ["ホーム", "アイコン", "基本入力", "ダイアログとポップアップ", "レイアウト", "ナビゲーション", "テキスト", "ビュー", "スクロール", "設定", "状態と情報", "メニューとツールバー", "日時", "オンラインドキュメント", "ソースコード"]
        );
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var config = ThemeService.LoadConfig();
        LocalizationService.Instance.SetCulture(config?.Language);
        InitializeCulture();
        
        try
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit.
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(config)
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
            {
                singleView.MainView = new MainView
                {
                    DataContext = new MainWindowViewModel(config)
                };
            }
            else
            {
                Console.Error.WriteLine($"Unhandled ApplicationLifetime type: {ApplicationLifetime?.GetType()}");
            }

            Frame.RegisterPage<FramePage1>();
            Frame.RegisterPage<FramePage2>();
            Frame.RegisterPage<FramePage3>();
            Frame.RegisterPage<FramePage4>();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"FATAL: App initialization failed: {ex}");
        }
        
        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove) BindingPlugins.DataValidators.Remove(plugin);
    }

    private void OnClicked(object? sender, EventArgs e)
    {
        // Environment.Exit(0);
        if (Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow?.Close();
        }
    }
}

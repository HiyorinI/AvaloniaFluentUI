using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaFluentUI.Locale;
using LocaleTest.ViewModels;
using LocaleTest.Views;

namespace LocaleTest;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void InitializeLocale()
    {
        // 添加多语言 运行时切换
        
        // Key 过多可重启设置 SetCulture(language)
        
        // 通过RESX文件
        // LocalizationService.Instance.LoadResxDirectory(@"C:\Projects\C#\AvaloniaFluentUi\test\Test\Assets\Locale");
        LocalizationService.Instance.SetCulture("ja-JP");
        
        // 通过字典
        LocalizationService.Instance
            .AddValue("zh-CN:Home", "主页")
            .AddValue("zh-CN:Icon", "图标")
            .AddValue("zh-CN:BasicInput", "基本输入")
            .AddValue("zh-CN:View", "视图")
            .AddValue("zh-CN:Layout", "布局")
            .AddValue("zh-CN:Scroll", "滚动")
            .AddValue("zh-CN:Navigation", "导航")
            .AddValue("zh-CN:Setting", "设置")
            .AddValue("zh-CN:CurrentPage", "当前页面")
            .AddValue("zh-CN:Title", "多语言切换示例")
            ;
        
        LocalizationService.Instance.AddValues(
            ["en-US:Home", "en-US:Icon", "en-US:BasicInput", "en-US:View", "en-US:Layout", "en-US:Scroll", "en-US:Navigation", "en-US:Setting", "en-US:CurrentPage", "en-US:Title"],
            ["Home", "Icon", "BasicInput", "View", "Layout", "Scroll", "Navigation", "Setting", "CurrentPage", "Multi-languageSwitchingExample"]
            );
        
        LocalizationService.Instance.AddValues(
            ["ja-JP:Home", "ja-JP:Icon", "ja-JP:BasicInput", "ja-JP:View", "ja-JP:Layout", "ja-JP:Scroll", "ja-JP:Navigation", "ja-JP:Setting",  "ja-JP:CurrentPage", "ja-JP:Title"],
            ["ホーム", "アイコン", "基本入力", "ビュー", "レイアウト", "スクロール", "ナビゲーション", "設定", "カレントページ", "多言語切り替え例"]
        );
    }

    public override void OnFrameworkInitializationCompleted()
    {
        InitializeLocale();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel(), };
        }

        base.OnFrameworkInitializationCompleted();
    }
}

using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using AvaloniaFluentUI.Controls.Windowing;

namespace Test.Views;

public partial class MainWindow : Window 
{
    public MainWindow()
    {
        InitializeComponent();

        // ── 启动画面 SplashScreen ──
        // 实现 IApplicationSplashScreen 接口，设置 SplashScreen 属性
        // 窗口加载时自动显示启动画面，RunTasks 完成后自动关闭
        // SplashScreen = new TestSplashScreen();

        // ── 标题栏自定义 ──
        // 扩展内容到标题栏区域（使 Window 内容填充整个客户区）
        // TitleBar.ExtendsContentIntoTitleBar = true;
        
        // 自定义标题栏颜色（仅 Windows 有效）
        // TitleBar.BackgroundColor = Color.Parse("#1E1E1E");
        // TitleBar.ForegroundColor = Colors.White;
        // TitleBar.InactiveBackgroundColor = Color.Parse("#2D2D2D");
        // TitleBar.InactiveForegroundColor = Color.Parse("#999999");

        // 默认 Light 主题
        // Application.Current?.RequestedThemeVariant = ThemeVariant.Light;
    }

    private void OnToggleTheme(object? sender, RoutedEventArgs e)
    {
        Application.Current!.RequestedThemeVariant =
            Application.Current.RequestedThemeVariant == ThemeVariant.Light
                ? ThemeVariant.Dark
                : ThemeVariant.Light;
    }
}

/// <summary>
/// 自定义启动画面——实现 IApplicationSplashScreen
/// 在 AppWindow 加载时显示，后台执行 RunTasks，完成后自动过渡到主窗口
/// </summary>
public class TestSplashScreen : IApplicationSplashScreen
{
    /// <summary>启动画面显示的应用名称</summary>
    public string AppName => "AvaloniaFluentUI Demo";

    /// <summary>启动画面图标（可空）</summary>
    public IImage? AppIcon => null;

    /// <summary>启动画面自定义内容（可空，如额外的加载动画或提示）</summary>
    public object? SplashScreenContent => null;

    /// <summary>最短显示时间（毫秒），避免一闪而过</summary>
    public int MinimumShowTime => 1500;

    /// <summary>
    /// 后台任务：窗口显示启动画面期间在这里执行初始化工作
    /// 例如加载配置、连接数据库等
    /// </summary>
    public Task RunTasks(CancellationToken cancellationToken)
    {
        // 模拟耗时初始化
        return Task.Delay(600, cancellationToken);
    }
}

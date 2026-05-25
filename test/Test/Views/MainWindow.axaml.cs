using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using AvaloniaFluentUI.Controls.Windowing;

namespace Test.Views;

public partial class MainWindow : AppWindow 
{
    public MainWindow()
    {
        InitializeComponent();

        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        EnableWindowEffect(true);

        SplashScreen = new MainWindowSplashScreen();

        // TitleBarHeight = 100;
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

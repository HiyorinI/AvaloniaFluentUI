using System.Runtime.CompilerServices;
using AvaloniaFluentUI.Controls.Interop;

namespace AvaloniaFluentUI.Windowing;

public partial class AppWindow
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void InitializeAppWindow()
    {
        IsWindows = true;
        IsWindows11 = OSVersionHelper.IsWindows11();

        _win32Manager = new Win32WindowManager(this);

        // Force AppWindow into darkmode at the system level
        Win32Interop.ApplyTheme(_win32Manager.Hwnd, true);
        PseudoClasses.Add(":windows");
        PlatformFeatures = new Win32AppWindowFeatures(this);
    }

    private Win32WindowManager _win32Manager;
}

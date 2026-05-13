using System.Diagnostics;
using Avalonia.Controls;

namespace Gallery.Views;

public partial class SettingsView : UserControl 
{
    public SettingsView()
    {
#if DEBUG
        Debug.WriteLine("SettingsView Init");
#endif
        InitializeComponent();

        // this.AttachedToLogicalTree += (s, e) =>
        // {
        // if (this.DataContext is SettingsViewModel vm)
        // {
        // vm.TogglePageAnimationChanged += OnToggleSwitchStatusChanged;
        // }
        // };

        // 注册
    }

    // protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    // {
    //     base.OnAttachedToVisualTree(e);
    //     // 挂载到视觉树时注册
    //     WeakReferenceMessenger.Default.Register(this);
    // }
    //
    // protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    // {
    //     base.OnDetachedFromVisualTree(e);
    //     // 从视觉树移除时注销，释放资源
    //     WeakReferenceMessenger.Default.UnregisterAll(this);
    // }
}
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Threading;
using AvaloniaFluentUI.Media.Animation;

namespace AvaloniaFluentUI.Controls;

[TemplatePart(s_tpContentPresenter, typeof(ContentPresenter))]
public class StackPanel : ContentControl
{
    private CancellationTokenSource _cts;
    private ContentPresenter _presenter;

    public static readonly StyledProperty<NavigationTransitionInfo> TransitionInfoProperty =
        AvaloniaProperty.Register<StackPanel, NavigationTransitionInfo>(nameof(TransitionInfo));

    public NavigationTransitionInfo TransitionInfo
    {
        get => GetValue(TransitionInfoProperty);
        set => SetValue(TransitionInfoProperty, value);
    }

    public StackPanel()
    {
        TransitionInfo = new EntranceNavigationTransitionInfo();
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _presenter = e.NameScope.Find<ContentPresenter>(s_tpContentPresenter);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == ContentProperty && change.NewValue != change.OldValue)
        {
            AnimateContent();
        }
    }

    private void AnimateContent()
    {
        if (_presenter == null)
            return;

        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        _presenter.Opacity = 0;

        Dispatcher.UIThread.Post(() => { TransitionInfo?.RunAnimation(_presenter, token); }, DispatcherPriority.Render);
    }

    private const string s_tpContentPresenter = "PART_ContentPresenter";
}

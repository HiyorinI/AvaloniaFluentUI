using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;

namespace AvaloniaFluentUI.UI.Controls;

[TemplatePart(Name = "PreviousButton", Type = typeof(ToolButton))]
[TemplatePart(Name = "NextButton", Type = typeof(ToolButton))]
public class FlipView : Carousel
{
    private ToolButton _previousButton;
    private ToolButton _nextButton;
    private DispatcherTimer _autoPlayTimer;

    // public static readonly StyledProperty<double> ViewPortWidthProperty =
    //     AvaloniaProperty.Register<FlipView, double>(nameof(ViewPortWidth));
    //
    // public double ViewPortWidth
    // {
    //     get => GetValue(ViewPortWidthProperty);
    //     set => SetValue(ViewPortWidthProperty, value);
    // }

    public static readonly StyledProperty<double> IntervalProperty =
        AvaloniaProperty.Register<FlipView, double>(nameof(Interval), defaultValue: 1500);

    public double Interval
    {
        get => GetValue(IntervalProperty);
        set => SetValue(IntervalProperty, value);
    }

    public static readonly StyledProperty<bool> IsAutoPlayProperty =
        AvaloniaProperty.Register<FlipView, bool>(nameof(IsAutoPlay));

    public bool IsAutoPlay
    {
        get => GetValue(IsAutoPlayProperty);
        set => SetValue(IsAutoPlayProperty, value);
    }

    public void Start()
    {
        _autoPlayTimer.Start();
    }

    public void Stop()
    {
        _autoPlayTimer.Stop();
    }

    private void OnAutoPlay(object sender, EventArgs e)
    {
        if (SelectedIndex >= ItemCount - 1)
        {
            SetCurrentValue(SelectedIndexProperty, 0);
        }
        else
        {
            Next();
        }
        // else if (SelectedIndex <= 0) { Next(); }
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (IsAutoPlay) { _autoPlayTimer.Start(); }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _autoPlayTimer.Stop();
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        
        _previousButton = e.NameScope.Find<ToolButton>("PreviousButton");
        _nextButton = e.NameScope.Find<ToolButton>("NextButton");

        var scroller = e.NameScope.Find<ScrollViewer>("PART_ScrollViewer");
        if (scroller != null)
        {
            scroller.BringIntoViewOnFocusChange = false;
        }
    }

    public FlipView()
    {
        _autoPlayTimer = new DispatcherTimer{ Interval = TimeSpan.FromMilliseconds(Interval) };
        _autoPlayTimer.Tick += OnAutoPlay;
        
        AddHandler(RequestBringIntoViewEvent, OnRequestBringIntoView);
    }

    private void OnRequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
    {
        e.Handled = true;
    }

    private void UpdateButtonStatus()
    {
        _previousButton?.IsVisible = SelectedIndex != 0;
        _nextButton?.IsVisible = SelectedIndex != ItemCount - 1;
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == SelectedIndexProperty)
        {
            UpdateButtonStatus();
        }

        if (change.Property == IsAutoPlayProperty)
        {
            if (IsAutoPlay) { Start(); }
            else { Stop(); }
        }

        if (change.Property == IntervalProperty)
        {
            Stop();
            _autoPlayTimer.Interval = TimeSpan.FromMilliseconds(Interval);
            if (IsAutoPlay) { Start(); }
        }
    }

    protected override void OnPointerEntered(PointerEventArgs e)
    {
        base.OnPointerEntered(e);
        UpdateButtonStatus();
    }

    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        _previousButton?.IsVisible = false;
        _nextButton?.IsVisible = false;
    }

    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        Console.WriteLine(true);
        Console.WriteLine(e.Delta.Y < 0);
        if (e.Delta.Y < 0)
        {
            Next();
        }
        else
        {
            Previous();
        }

        e.Handled = true;
        base.OnPointerWheelChanged(e);
    }
}

public class FlipViewItem
{
    public IImage? ImageSource { get; set; }
    // public string? Title { get; set; }
}

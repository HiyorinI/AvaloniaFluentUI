using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Gallery.Controls;

public class AnimatedWrapPanel : WrapPanel
{
    public static readonly StyledProperty<TimeSpan> AnimationDurationProperty =
        AvaloniaProperty.Register<AnimatedWrapPanel, TimeSpan>(
            nameof(AnimationDuration), TimeSpan.FromMilliseconds(300));

    public static readonly StyledProperty<TimeSpan> ItemStaggerDelayProperty =
        AvaloniaProperty.Register<AnimatedWrapPanel, TimeSpan>(
            nameof(ItemStaggerDelay), TimeSpan.FromMilliseconds(30));

    public static readonly StyledProperty<bool> IsAnimationEnabledProperty =
        AvaloniaProperty.Register<AnimatedWrapPanel, bool>(
            nameof(IsAnimationEnabled), true);

    public TimeSpan AnimationDuration
    {
        get => GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    public TimeSpan ItemStaggerDelay
    {
        get => GetValue(ItemStaggerDelayProperty);
        set => SetValue(ItemStaggerDelayProperty, value);
    }

    public bool IsAnimationEnabled
    {
        get => GetValue(IsAnimationEnabledProperty);
        set => SetValue(IsAnimationEnabledProperty, value);
    }

    private readonly List<Control> _animatedChildren = new();
    private bool _isInitialized = false;

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _isInitialized = true;
        AnimateExistingChildren();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _isInitialized = false;
        _animatedChildren.Clear();
    }

    protected override void ChildrenChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        base.ChildrenChanged(sender, e);

        if (!IsAnimationEnabled || !_isInitialized)
            return;

        if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
        {
            for (int i = 0; i < e.NewItems.Count; i++)
            {
                if (e.NewItems[i] is Control control && !_animatedChildren.Contains(control))
                {
                    var index = e.NewStartingIndex + i;
                    _animatedChildren.Add(control);
                    _ = AnimateChildIn(control, index);
                }
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
        {
            foreach (var item in e.OldItems)
            {
                if (item is Control control)
                {
                    _animatedChildren.Remove(control);
                }
            }
        }
    }

    private void AnimateExistingChildren()
    {
        if (!IsAnimationEnabled) return;

        for (int i = 0; i < Children.Count; i++)
        {
            if (Children[i] is Control control && !_animatedChildren.Contains(control))
            {
                _animatedChildren.Add(control);
                _ = AnimateChildIn(control, i);
            }
        }
    }

    private async Task AnimateChildIn(Control control, int index)
    {
        var duration = AnimationDuration;
        var delay = TimeSpan.FromTicks(ItemStaggerDelay.Ticks * index);

        // 设置初始状态：透明 + 轻微缩小 + 向下偏移
        // control.Opacity = 0;
        control.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
        var transform = new TransformGroup();
        transform.Children.Add(new ScaleTransform(0.85, 0.85));
        transform.Children.Add(new TranslateTransform(0, 15));
        control.RenderTransform = transform;

        // 等待交错延迟
        if (delay.Ticks > 0)
            await Task.Delay(delay);

        // 使用 Transitions 实现透明度过渡（从 0 → 1 平滑淡入）
        control.Transitions = new Transitions
        {
            new DoubleTransition
            {
                Property = OpacityProperty,
                Duration = duration,
                Easing = new CircularEaseInOut()
            }
        };

        // 设置目标状态
        // control.Opacity = 1;

        // 等待动画完成
        await Task.Delay(duration);

        // 清理，让控件恢复正常状态
        control.Transitions = null;
        control.RenderTransform = null;
    }

    // protected override Type StyleKeyOverride => typeof(WrapPanel);
}



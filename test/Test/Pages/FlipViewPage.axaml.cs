using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace Test.Pages;

public partial class FlipViewPage : UserControl
{
    private readonly TranslateTransform _currentTransform = new();
    private readonly TranslateTransform _nextTransform = new();

    public static readonly StyledProperty<IEnumerable<string>?> ImageSourcesProperty =
        AvaloniaProperty.Register<FlipViewPage, IEnumerable<string>?>(nameof(ImageSources));

    public static readonly StyledProperty<int> CurrentIndexProperty =
        AvaloniaProperty.Register<FlipViewPage, int>(nameof(CurrentIndex), defaultValue: -1);

    public static readonly StyledProperty<BitmapInterpolationMode> ImageInterpolationModeProperty =
        AvaloniaProperty.Register<FlipViewPage, BitmapInterpolationMode>(nameof(ImageInterpolationMode), defaultValue: BitmapInterpolationMode.MediumQuality);

    public BitmapInterpolationMode ImageInterpolationMode
    {
        get => GetValue(ImageInterpolationModeProperty);
        set => SetValue(ImageInterpolationModeProperty, value);
    }

    public IEnumerable<string>? ImageSources
    {
        get => GetValue(ImageSourcesProperty);
        set => SetValue(ImageSourcesProperty, value);
    }

    public int CurrentIndex
    {
        get => GetValue(CurrentIndexProperty);
        set => SetValue(CurrentIndexProperty, value);
    }

    public static readonly StyledProperty<int> ItemCountProperty =
        AvaloniaProperty.Register<FlipViewPage, int>(nameof(ItemCount));
    
    public int ItemCount
    {
        get => GetValue(ItemCountProperty);
        private set => SetValue(ItemCountProperty, value);
    }

    private List<Bitmap> _items = new List<Bitmap>();

    public List<Bitmap> Items => _items;

    private bool _isRunning;

    public TimeSpan Duration { get; set; }

    public PageSlide.SlideAxis Orientation { get; set; }

    public Easing SlideInEasing { get; set; } = new CubicEaseOut();

    public Easing SlideOutEasing { get; set; } = new LinearEasing();

    public FlipViewPage()
    {
        InitializeComponent();

        CurrentImage.RenderTransform = _currentTransform;
        NextImage.RenderTransform = _nextTransform;

        Orientation = PageSlide.SlideAxis.Vertical;

        PB.Click += (_, _) => { Previous(); };
        NB.Click += (_, _) => { Next(); };
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (_items.Count <= 0 && ItemCount > 0)
        {
            ReloadImages();
        }
    }

    protected override async void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        await Task.Yield();
        DisposeImage();
    }

    private void DisposeImage()
    {
        if (_items.Count > 0)
        {
            CurrentImage.Source = null;
            NextImage.Source = null;
#if DEBUG
            Debug.WriteLine("Dispose Image");
#endif
            foreach (var bitmap in _items)
            {
                bitmap.Dispose();
            }

            _items.Clear();
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == ImageSourcesProperty)
        {
            DisposeImage();
            
            
            if (this.IsAttachedToVisualTree())
            {
                ReloadImages();
            }
            else
            {
                var newValue = ImageSources?.ToList();
                if (newValue != null)
                {
                    ItemCount = newValue.Count;
                }
            }
            // ReloadImages();
        }
    }

    private void ReloadImages()
    {
        var newValue = ImageSources?.ToList();
        if (newValue == null) { return; }

#if DEBUG
        Debug.WriteLine("Reload Image");
#endif

        foreach (var path in newValue)
        {
            if (path.StartsWith("avares://"))
            {
                _items.Add(new Bitmap(AssetLoader.Open(new Uri(path))));
#if DEBUG
                Debug.WriteLine("Avares Image");
#endif
            }
            else
            {
                _items.Add(new Bitmap(path));
#if DEBUG
                Debug.WriteLine("Local Image");
#endif
            }
        }

        if (_items.Count > 0)
        {
#if DEBUG
            Debug.WriteLine("Count > 0");
#endif
            ItemCount = _items.Count;
            CurrentIndex = 0;

            CurrentImage.Source = _items[0];
            NextImage.Source = null;

            _currentTransform.X = 0;
            _currentTransform.Y = 0;

            _nextTransform.X = 0;
            _nextTransform.Y = 0;
        }
        else
        {
            ItemCount = 0;
            CurrentIndex = -1;

            CurrentImage.Source = null;
            NextImage.Source = null;
        }
    }

    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        base.OnPointerWheelChanged(e);

        if (ItemCount == 0)
        {
            return;
        }

        if (e.Delta.Y < 0)
        {
            Next();
        }
        else if (e.Delta.Y > 0)
        {
            Previous();
        }
    }

    private async Task RunSliderAnimationAsync(IImage image, int targetIndex, bool forward)
    {
        _isRunning = true;

        double distance = Orientation == PageSlide.SlideAxis.Horizontal ? Bounds.Width : Bounds.Height;
        var property = Orientation == PageSlide.SlideAxis.Horizontal ? TranslateTransform.XProperty : TranslateTransform.YProperty;

        NextImage.Source = image;
        NextImage.IsVisible = true;

        if (forward)
        {
            Duration = TimeSpan.FromMilliseconds(300);
        }
        else
        {
            Duration = TimeSpan.FromMilliseconds(400);
        }

        var currentAnimation = new Animation
        {
            Duration = Duration,
            FillMode = FillMode.Forward,
            Easing = SlideOutEasing,
            Children =
            {
                new KeyFrame
                {
                    Cue = new Cue(0),
                    Setters = { new Setter(property, 0d) }
                },
                new KeyFrame
                {
                    Cue = new Cue(1),
                    Setters = { new Setter(property, forward ? -distance : distance) }
                }
            }
        };

        var nextAnimation = new Animation
        {
            Duration = Duration,
            FillMode = FillMode.Forward,
            Easing = SlideInEasing,
            Children =
            {
                new KeyFrame
                {
                    Cue = new Cue(0),
                    Setters = { new Setter(property, forward ? distance : -distance) }
                },
                new KeyFrame
                {
                    Cue = new Cue(1), 
                    Setters = { new Setter(property, 0d) }
                }
            }
        };

        await Task.WhenAll(currentAnimation.RunAsync(CurrentImage), nextAnimation.RunAsync(NextImage));

        CurrentImage.Source = image;

        NextImage.Source = null;
        NextImage.IsVisible = false;

        _currentTransform.X = 0;
        _currentTransform.Y = 0;

        _nextTransform.X = 0;
        _nextTransform.Y = 0;

        CurrentIndex = targetIndex;
        _isRunning = false;
    }

    public void Next()
    {
        if (_isRunning || _items.Count == 0 || CurrentIndex >= ItemCount - 1)
        {
            return;
        }
        
        int nextIndex = CurrentIndex + 1;
        _=RunSliderAnimationAsync(_items[nextIndex], nextIndex, true);
    }

    public void Previous()
    {
        if (_isRunning || _items.Count == 0 || CurrentIndex <= 0) 
        {
            return;
        }

        int prevIndex = CurrentIndex - 1;
        _=RunSliderAnimationAsync(_items[prevIndex], prevIndex, false);
    }
}

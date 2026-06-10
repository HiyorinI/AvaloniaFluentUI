using System;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;

namespace Test.Views;

public partial class NaviView : UserControl
{
    public NaviView()
    {
        InitializeComponent();
        CarouselPage.PageTransition = new PageSlide
        {
            Duration =  TimeSpan.FromMilliseconds(300),
            SlideInEasing = new CubicEaseInOut()
        };
    }
}


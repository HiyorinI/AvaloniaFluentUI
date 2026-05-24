using System;
using Avalonia.Controls;
using AvaloniaFluentUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Gallery.ViewModels;

public partial class DialogBoxAndPopupViewModel : ViewModelBase
{
    [ObservableProperty]
    private PlacementMode _flyoutPlacement = PlacementMode.Top;

    [ObservableProperty]
    private PlacementMode[] _flyoutPlacements =
    [
        PlacementMode.Top,
        PlacementMode.Left,
        PlacementMode.Right,
        PlacementMode.Bottom,
        PlacementMode.Center,
        PlacementMode.Pointer,
    ];

    [ObservableProperty]
    private bool _fluentFlyoutIsOpen;

    public TeachingTipPlacementMode[] TeachingTipPlacements => [ TeachingTipPlacementMode.Auto, TeachingTipPlacementMode.Bottom, TeachingTipPlacementMode.BottomLeft, TeachingTipPlacementMode.BottomRight, TeachingTipPlacementMode.Center, TeachingTipPlacementMode.Left, TeachingTipPlacementMode.LeftBottom, TeachingTipPlacementMode.LeftTop, TeachingTipPlacementMode.Right, TeachingTipPlacementMode.RightBottom, TeachingTipPlacementMode.RightTop, TeachingTipPlacementMode.Top, TeachingTipPlacementMode.TopLeft, TeachingTipPlacementMode.TopRight]; 
    
    [ObservableProperty]
    private TeachingTipPlacementMode _teachingTipPlacement = TeachingTipPlacementMode.Top;

    [RelayCommand]
    private void CloseFlyout() => FluentFlyoutIsOpen = false;

    [ObservableProperty]
    private bool _teachingTipIsOpen;

    [RelayCommand]
    private void CloseTeachingTip() => TeachingTipIsOpen = false;

    [RelayCommand]
    private void ShowTeachingTip()
    {
        Console.WriteLine(TeachingTipIsOpen);
        if (TeachingTipIsOpen) TeachingTipIsOpen = false;
        TeachingTipIsOpen = true;
    }
}

using System;
using Avalonia.Controls;
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
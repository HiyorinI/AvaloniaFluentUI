using System;
using Avalonia.Controls;
using AvaloniaFluentUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Test.ViewModels;

public partial class ButtonViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _teachingTipIsOpen;

    [ObservableProperty]
    private TeachingTipPlacementMode _teachingTipPlacement = TeachingTipPlacementMode.Top;

    [ObservableProperty]
    private Control _teachingTipTarget;

    [RelayCommand]
    private void ShowTeachingTip(object value)
    {
        if (value is not Control control) { return; }

        switch (control.Tag!.ToString())
        {
            case "Top":
                TeachingTipPlacement = TeachingTipPlacementMode.Top;
                break;
            case "Right":
                TeachingTipPlacement = TeachingTipPlacementMode.Right;
                break;
        }

        TeachingTipTarget = control;

        if (TeachingTipIsOpen)
        {
            TeachingTipIsOpen = false;
        }

        TeachingTipIsOpen = !TeachingTipIsOpen;
        Console.WriteLine("Triggered");
    }
}

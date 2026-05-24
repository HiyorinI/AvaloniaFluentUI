using System;
using Avalonia.Media;
using AvaloniaFluentUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Gallery.ViewModels;

public partial class StatusAndInformationViewModel : ViewModelBase
{
    public InfoBarSeverity[] InfoBarSeverityItems => 
    [
        InfoBarSeverity.Informational,
        InfoBarSeverity.Success,
        InfoBarSeverity.Warning,
        InfoBarSeverity.Error
    ];

    [ObservableProperty]
    private InfoBarSeverity _currentInfoBarSeverity = InfoBarSeverity.Success;

    [ObservableProperty]
    private bool _infoBarIsClosable;

    [ObservableProperty]
    private bool _infoBarIsOpen = true;

    [RelayCommand]
    private void ResetInfoBar()
    {
        InfoBarIsOpen = true;
    }

    [ObservableProperty]
    private bool _progressBarIsIndeterminate = true;

    [ObservableProperty]
    private double _progressBarCurrentValue = 24.0;

    [ObservableProperty]
    private bool _progressRingIsIndeterminate = true;

    [ObservableProperty]
    private double _progressRingCurrentValue = 24.0;

    public IBrush ProgressRingBackground => new SolidColorBrush(ProgressRingColor);
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ProgressRingBackground))]
    private Color _progressRingColor = Colors.Transparent;
}

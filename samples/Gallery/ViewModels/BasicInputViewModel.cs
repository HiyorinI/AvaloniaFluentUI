using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Layout;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Gallery.ViewModels;

public partial class BasicInputViewModel : ViewModelBase
{
    [ObservableProperty]
    private HorizontalAlignment[] _horizontalAlignments =
    [
        HorizontalAlignment.Left,
        HorizontalAlignment.Right,
        HorizontalAlignment.Stretch,
        HorizontalAlignment.Center
    ];

    #region PushButton
    
    [ObservableProperty]
    private double _pushButtonWidth = 256;
    
    [ObservableProperty]
    private double _pushButtonHeight = 35;

    [ObservableProperty]
    private HorizontalAlignment _pushButtonContentAlignment = HorizontalAlignment.Center;

    [ObservableProperty]
    private bool _pushButtonIsDisable;

    [ObservableProperty]
    private double _pushButtonMinimumWidth = 256;

    [ObservableProperty]
    private double _pushButtonMinimumHeight = 35;

    [ObservableProperty]
    private double _pushButtonMaximumWidth = 512;

    [ObservableProperty]
    private double _pushButtonMaximumHeight = 128;
    
    #endregion

    #region ToolButton 

    [ObservableProperty]
    private double _toolButtonWidth = 64;

    [ObservableProperty]
    private double _toolButtonHeight = 35;
    
    [ObservableProperty]
    private HorizontalAlignment _toolButtonContentAlignment = HorizontalAlignment.Center;

    [ObservableProperty]
    private double _toolButtonMinimumWidth = 40;

    [ObservableProperty]
    private double _toolButtonMaximumWidth = 512;

    [ObservableProperty]
    private double _toolButtonMinimumHeight = 35;

    [ObservableProperty]
    private double _toolButtonMaximumHeight = 128;

    [ObservableProperty]
    private bool _toolButtonIsDisable;

    #endregion

    [ObservableProperty]
    private bool _statusSwitchButtonIsDisable;

    [ObservableProperty]
    private bool _splitButtonIsDisable;

    [ObservableProperty]
    private bool _radiusButtonIsDisable;

    [ObservableProperty]
    private bool _hyperlinkButtonIsDisable;

    [ObservableProperty]
    private bool _toggleSwitchButtonIsDisable;

    [ObservableProperty]
    private bool _checkBoxIsDisable;

    [ObservableProperty]
    private bool _checkBoxIsThreeState;

    [ObservableProperty]
    private bool _dropDownButtonIsDisable;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SliderCurrentValueFormat))]
    private double _sliderCurrentValue;

    public string SliderCurrentValueFormat => "当前值" +  SliderCurrentValue.ToString("F");

    [ObservableProperty]
    private List<string> _items = new List<string>();

    public BasicInputViewModel()
    {
        for (int i = 1; i <= 64; i++)
        {
            Items.Add($"Item {i}");
        }
    }

}

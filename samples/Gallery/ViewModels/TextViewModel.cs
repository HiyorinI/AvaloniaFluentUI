using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaFluentUI.UI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Gallery.ViewModels;

public partial class TextViewModel : ViewModelBase
{
    public TextViewModel()
    {
        for (int i = 6; i <= 64; i += 2)
            _fontSizeItems.Add(i);
    }
    
    /// <summary>
    /// AutoComplete
    /// </summary>
    #region AutoComplete
    
    public AutoCompleteFilterMode AutoCompleterMode => SelectedAutoCompleteMode;

    [ObservableProperty]
    private ObservableCollection<string> _autoCompleteItems = 
    [ 
        "cat",
        "camel", 
        "cow",
        "chameleon", 
        "mouse", 
        "lion", 
        "zebra", 
        "Before She Goes",
        "So in Love", 
        "Obsession", 
        "I Hate Falling In Love", 
        "Thinking Of You" 
    ];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AutoCompleteBoxTextInfo))]
    private string _autoCompleteBoxText = String.Empty;

    public string AutoCompleteBoxTextInfo => $"Input Text: {AutoCompleteBoxText}";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AutoCompleterMode))]
    private AutoCompleteFilterMode _selectedAutoCompleteMode = AutoCompleteFilterMode.Contains;

    [ObservableProperty]
    private bool _autoCompleteBoxSettingsPanelIsExpand;

    [RelayCommand]
    private void ExpandAutoCompleteBoxSettingsPane() => AutoCompleteBoxSettingsPanelIsExpand = !AutoCompleteBoxSettingsPanelIsExpand;
    

    [ObservableProperty]
    private string _autoCompleteElement = String.Empty;

    [RelayCommand]
    private void AddAutoCompleteItem()
    {
        if (!String.IsNullOrEmpty(AutoCompleteElement) && !AutoCompleteItems.Contains(AutoCompleteElement))
        {
            AutoCompleteItems.Add(AutoCompleteElement);
        }
    }

    [ObservableProperty]
    private List<AutoCompleteFilterMode> _autoCompleteFilterModeItems = [
        AutoCompleteFilterMode.None,
        AutoCompleteFilterMode.StartsWith,
        AutoCompleteFilterMode.StartsWithCaseSensitive,
        AutoCompleteFilterMode.StartsWithOrdinal,
        AutoCompleteFilterMode.StartsWithOrdinalCaseSensitive,
        AutoCompleteFilterMode.Contains,
        AutoCompleteFilterMode.ContainsCaseSensitive,
        AutoCompleteFilterMode.ContainsOrdinal,
        AutoCompleteFilterMode.ContainsOrdinalCaseSensitive,
        AutoCompleteFilterMode.Equals,
        AutoCompleteFilterMode.EqualsCaseSensitive,
        AutoCompleteFilterMode.EqualsOrdinal,
        AutoCompleteFilterMode.EqualsOrdinalCaseSensitive,
        // AutoCompleteFilterMode.Custom,
    ];
    
    #endregion

    /// <summary>
    /// TextBlock 
    /// </summary>

    #region TextBlock

    [ObservableProperty]
    private bool _textBlockSettingsPaneIsExpand;

    [ObservableProperty]
    private string _textBlockText = "Hello World!";

    [ObservableProperty]
    private double _textBlockFontSize = 32.0;

    [ObservableProperty]
    private TextDecorationCollection? _textDecorations;

    [ObservableProperty]
    private TextDecorationCollection?[] _textDecorationsItems =
    [
        null,
        Avalonia.Media.TextDecorations.Strikethrough,
        Avalonia.Media.TextDecorations.Underline,
        Avalonia.Media.TextDecorations.Baseline,
        Avalonia.Media.TextDecorations.Overline
    ];

    [ObservableProperty]
    private double _textBlockLetterSpacing;

    [ObservableProperty]
    private FontWeight _textBlockFontWeight = FontWeight.Normal;
    
    [ObservableProperty]
    private FontStyle _textBlockFontStyle = FontStyle.Normal;

    [ObservableProperty]
    private List<double> _fontSizeItems = new();

    [ObservableProperty]
    private List<FontWeight> _fontWeightItems = [
        FontWeight.Thin,
        FontWeight.ExtraLight,
        FontWeight.UltraLight,
        FontWeight.Light,
        FontWeight.SemiLight,
        FontWeight.Normal,
        FontWeight.Regular,
        FontWeight.Medium,
        FontWeight.DemiBold,
        FontWeight.SemiBold,
        FontWeight.Bold,
        FontWeight.ExtraBold,
        FontWeight.UltraBold,
        FontWeight.Black,
        FontWeight.Heavy,
        FontWeight.Solid,
        FontWeight.ExtraBlack,
        FontWeight.UltraBlack,
    ];

    [ObservableProperty]
    private List<FontStyle> _fontStyleItems = [
        FontStyle.Normal,
        FontStyle.Italic,
        FontStyle.Oblique
    ];

    [RelayCommand]
    private void ExpandTextBlockSettings() => TextBlockSettingsPaneIsExpand = !TextBlockSettingsPaneIsExpand;
    
    #endregion
    
    /// <summary>
    /// TextBox
    /// </summary>
    #region TextBox
    
    [ObservableProperty]
    private bool _isAccentReturn;

    [ObservableProperty]
    private bool _isAcceptTab;

    [ObservableProperty]
    private string? _watermark = "请输入文本...";
    
    [ObservableProperty]
    private TextWrapping _textWrapping = TextWrapping.NoWrap;

    [ObservableProperty]
    private string _alternativeCharacters = String.Empty;

    [ObservableProperty]
    private List<TextWrapping> _textWrappingItems = [
        TextWrapping.NoWrap,
        TextWrapping.Wrap,
        TextWrapping.WrapWithOverflow
    ];

    [ObservableProperty]
    private bool _textBoxSettingsPaneIsExpand;

    [RelayCommand]
    private void ExpandTextBoxSettingsPane() => TextBoxSettingsPaneIsExpand = !TextBoxSettingsPaneIsExpand;

    #endregion

    [ObservableProperty]
    private NumberBoxSpinButtonPlacementMode _numberBoxSpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Inline;

    public NumberBoxSpinButtonPlacementMode[] NumberBoxSpinButtonPlacementModes => 
    [
        NumberBoxSpinButtonPlacementMode.Hidden,
        NumberBoxSpinButtonPlacementMode.Inline,
        NumberBoxSpinButtonPlacementMode.Compact
    ];

    [ObservableProperty]
    private Location _spinnerLocation = Location.Right;

    public Location[] SpinnerLocations => [Location.Left, Location.Right];

    [ObservableProperty]
    private bool _isEnabledSpinner = true;
}
using System.Collections.Generic;
using Avalonia.Controls;
using AvaloniaFluentUI.Controls;
using AvaloniaFluentUI.Locale;
using AvaloniaFluentUI.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Test.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [RelayCommand]
    private void ToggleTheme()
    {
        FluentAvaloniaTheme.Instance.ToggleTheme();
    }

    public MainViewModel()
    {
        Items = new List<string>();
        
        for (int i = 1; i <= 100; i++)
        {
            Items.Add($"Combo Box Item {i}");
        }
    }
    
    
    public List<string> Items { get; }
    
    public string CurrentLanguageFormat => LocalizationService.Instance.CurrentLanguage;

    

    [RelayCommand]
    private void ToggleLanguage(object value)
    {
        string language = value.ToString();
        if (language != LocalizationService.Instance.CurrentLanguage)
        {
            LocalizationService.Instance.SetCulture(value.ToString());
            OnPropertyChanged(nameof(CurrentLanguageFormat));
        }
        
    }
}

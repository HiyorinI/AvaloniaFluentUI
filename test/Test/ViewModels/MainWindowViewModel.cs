using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using AvaloniaFluentUI.Locale;
using AvaloniaFluentUI.Styling;
using CommunityToolkit.Mvvm.Input;

namespace Test.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [RelayCommand]
    private void ToggleTheme()
    {
        FluentAvaloniaTheme.Instance.ToggleTheme();
    }
    
    public string[] Items { get; } = ["Item 1",  "Item 2", "Item 3",  "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10"] ;
    
    public string CurrentLanguageFormat => LocalizationService.Instance.CurrentCulture;

    [RelayCommand]
    private void ToggleLanguage(object value)
    {
        string language = value.ToString();
        if (language != LocalizationService.Instance.CurrentCulture)
        {
            LocalizationService.Instance.SetCulture(value.ToString());
            OnPropertyChanged(nameof(CurrentLanguageFormat));
        }
        
    }
}

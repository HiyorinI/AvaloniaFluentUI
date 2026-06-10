using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Styling;
using AvaloniaFluentUI.Controls;
using AvaloniaFluentUI.Locale;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LocaleTest.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _targetCulture = "";
    
    public string Setting => LocalizationService.Instance.GetString("Setting");
    public string Navigation => LocalizationService.Instance.GetString("Navigation");
    public string Scroll => LocalizationService.Instance.GetString("Scroll");
    public string Layout => LocalizationService.Instance.GetString("Layout");
    public string View => LocalizationService.Instance.GetString("View");
    public string BasicInput => LocalizationService.Instance.GetString("BasicInput");
    public string Icon => LocalizationService.Instance.GetString("Icon");
    public string Home => LocalizationService.Instance.GetString("Home");
    
    public string SelectedItemFormat => LocalizationService.Instance.GetString("CurrentPage") + ": " + (SelectedItem.Content?.ToString() ?? "Null");

    [RelayCommand]
    private void ToggleTheme()
    {
        Application.Current?.RequestedThemeVariant = Application.Current.RequestedThemeVariant == ThemeVariant.Light ? ThemeVariant.Dark :  ThemeVariant.Light;
    }

    [RelayCommand]
    private void ToggleLanguage(object? parameter)
    {
        LocalizationService.Instance.SetCulture(parameter.ToString());
        _targetCulture = parameter?.ToString() ?? LocalizationService.DefaultCultureInfo.Name;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedItemFormat))]
    private SegmentedItem _selectedItem;

    public MainWindowViewModel()
    {
        Console.WriteLine(LocalizationService.Instance.GetString("Home"));
        
        LocalizationService.Instance.PropertyChanged += (_, _) =>
        {
            if (_targetCulture == LocalizationService.Instance.CurrentLanguage) { return; }
            
            Task.Run(async () =>
            {
                Console.WriteLine($"Toggle Culture To: {_targetCulture}");
                // await Task.Delay(500);
                OnPropertyChanged(nameof(Setting));
                
                // await Task.Delay(500);
                OnPropertyChanged(nameof(Navigation));
                
                // await Task.Delay(500);
                OnPropertyChanged(nameof(Scroll));
                
                // await Task.Delay(500);
                OnPropertyChanged(nameof(Layout));
                
                // await Task.Delay(500);
                OnPropertyChanged(nameof(View));
                
                // await Task.Delay(500);
                OnPropertyChanged(nameof(BasicInput));
                
                // await Task.Delay(500);
                OnPropertyChanged(nameof(Icon));
                
                // await Task.Delay(500);
                OnPropertyChanged(nameof(Home));
                
                // await Task.Delay(500);
                OnPropertyChanged(nameof(SelectedItemFormat));
            });
            
        };
    }
}

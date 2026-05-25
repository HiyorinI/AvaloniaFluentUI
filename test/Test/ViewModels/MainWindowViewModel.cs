using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;

namespace Test.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [RelayCommand]
    private void ToggleTheme()
    {
        Application.Current?.RequestedThemeVariant = Application.Current.RequestedThemeVariant == ThemeVariant.Light ? ThemeVariant.Dark :  ThemeVariant.Light;
    }

    public string[] Items { get; } = ["Item 1",  "Item 2", "Item 3",  "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10"] ;
}

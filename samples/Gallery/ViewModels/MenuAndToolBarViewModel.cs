using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Gallery.ViewModels;

public partial class MenuAndToolBarViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _commandText = "NULL";

    [RelayCommand]
    private void OnClickedMenuItem(string value) => CommandText = value;
}
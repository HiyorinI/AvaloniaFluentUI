using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Test.ViewModels;

namespace Test.ViewModels;

public partial class CardViewModel : ViewModelBase
{
    [ObservableProperty]
    private ExpandDirection _expandDirection = ExpandDirection.Down;

    public ExpandDirection[] ExpandDirections =>
        [ExpandDirection.Left, ExpandDirection.Up, ExpandDirection.Right, ExpandDirection.Down];
}

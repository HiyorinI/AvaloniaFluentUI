using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using Test.ViewModels;

namespace test.Test.ViewModels;

public partial class TabControlViewModel : ViewModelBase
{
    public TabControlViewModel()
    {
    }

    public List<string> ItemSource => ["Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7",  "Item 8", "Item 9", "Item 10", "Item 11", "Item 12", "Item 13", "Item 14", "Item 15", "Item 16"];

    [RelayCommand]
    private void Search(object? value)
    {
        Console.WriteLine($"Search: {value}");
    }
}

using System.Diagnostics;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using Gallery.Messages.IconViewMessages;

namespace Gallery.ViewModels;

public partial class IconsViewModel : ViewModelBase 
{
    public IconsViewModel()
    {
#if DEBUG
        Debug.WriteLine("IconViewModel Init");
#endif
        WeakReferenceMessenger.Default.Register<CheckedIconChangedMessage>(this, OnCheckedIconChanged);
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentItemEnumName))]
    private string _currentIconName = "";

    [ObservableProperty]
    private Geometry? _currentIconData;
    
    public string CurrentItemEnumName => CurrentIconName == "" ? "" : $"Fluent.{CurrentIconName}";

    private void OnCheckedIconChanged(object? sender, CheckedIconChangedMessage message)
    {
        CurrentIconName = message.Name;
        CurrentIconData = message.Data;
    }
}

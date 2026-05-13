using System.Diagnostics;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using Gallery.Messages.IconViewMessages;

namespace Gallery.ViewModels;

public partial class IconsViewModel : ViewModelBase, IRecipient<CheckedIconChangedMessage>
{
    public IconsViewModel()
    {
#if DEBUG
        Debug.WriteLine("IconViewModel Init");
#endif
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentItemEnumName))]
    private string _currentIconName = "";

    [ObservableProperty]
    private Geometry? _currentIconData;
    
    public string CurrentItemEnumName => CurrentIconName == "" ? "" : $"Fluent.{CurrentIconName}";
    
    public void Receive(CheckedIconChangedMessage message)
    {
#if DEBUG
        Debug.WriteLine(message.Name);
        Debug.WriteLine(message.Data);
#endif
        
        CurrentIconName = message.Name;
        CurrentIconData = message.Data;
    }
}
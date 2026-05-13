using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gallery.Messages;

namespace Gallery.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly List<string> _history = new();

    private readonly Dictionary<string, ViewModelBase> _viewModels;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoBackCommand))]
    private bool _canGoBack;

    [ObservableProperty]
    private object _navigationViewSelectedItem;

    partial void OnNavigationViewSelectedItemChanged(object value)
    {
        // Console.WriteLine(value.GetType());
        if (value is AvaloniaFluentUI.UI.Controls.NavigationViewItem item)
        {
            // Console.WriteLine(item.Tag);
            TogglePage(item.Tag+"");
        }
    }

    [ObservableProperty]
    private ViewModelBase? _currentViewModel;

    // public event Action<string, string>? ViewModelChangedEvent;

    public MainWindowViewModel()
    {
#if DEBUG
        Debug.WriteLine("MainWindowViewModel Init");
#endif
        
        // _viewModels = new Dictionary<string, ViewModelBase>()
        // {
            // {"Home", HomeViewModel},
        // }
        
        _viewModels = new Dictionary<string, ViewModelBase>
        {
            // { "Home", HomeViewModel },
            // { "Settings", SettingsViewModel },
            // { "ListBox", ListBoxViewModel },
            // { "Icons", IconsViewModel },
            // { "NavigationDemo", NavigationDemoViewModel },
            // { "DatePicker", DatePickerViewModel },
            // { "BaseInput", BaseInputViewModel },
            // { "Panel", PanelViewModel},
            // { "DrawRect", DrawRectViewModel},
            // { "LayoutControls", LayoutControlsViewModel},
            // { "Buttons", ButtonViewModel},
            // { "DuplicateDataControls", DuplicateDataControlsViewModel},
            // { "TextDisplayAndEditing", TextDisplayAndEditingViewModel},
            // { "ValueSelection", ValueSelectionViewModel},
            // { "Image", DisplayImageViewModel},
            // { "MenusAndPopups", MenusAndPopupsViewModel}
            
            { "Home", HomeViewModel },
            { "Icons", IconsViewModel },
            { "BasicInput", BasicInputViewModel },
            { "DialogBoxAndPopup", DialogBoxAndPopupViewModel },
            { "Layout", LayoutViewModel },
            { "Navigation", NavigationViewModel },
            { "Text", TextViewModel },
            { "View", ViewModel },
            { "Scroll", ScrollViewModel },
            { "StatusAndInformation", StatusAndInformationViewModel},
            { "MenuAndToolBar", MenuAndToolBarViewModel },
            { "DateTime", DateTimeViewModel },
            { "Settings", SettingsViewModel },
        };
        
        HomeViewModel.GotoControlEvent += (page, name) =>
        {
            TogglePage(page);
            WeakReferenceMessenger.Default.Send(new JumpToControlMessage(page, name));
            // ViewModelChangedEvent?.Invoke(page, name);
        };
        CurrentViewModel = HomeViewModel;
    }

    // private HomeViewModel HomeViewModel { get; } = new();
    // private IconsViewModel IconsViewModel { get; } = new();
    // private NavigationDemoViewModel NavigationDemoViewModel { get; } = new();
    // private DatePickerViewModel DatePickerViewModel { get; } = new();
    // private BaseInputViewModel BaseInputViewModel { get; } = new();
    // private ListBoxViewModel ListBoxViewModel { get; } = new();
    // private SettingsViewModel SettingsViewModel { get; } = new();
    // private PanelViewModel PanelViewModel { get; } = new();
    // private DrawRectViewModel DrawRectViewModel { get; } = new();
    // private LayoutControlsViewModel LayoutControlsViewModel { get; } = new();
    // private ButtonViewModel ButtonViewModel { get; } = new();
    // private DuplicateDataControlsViewModel DuplicateDataControlsViewModel { get; } = new();
    // private TextDisplayAndEditingViewModel TextDisplayAndEditingViewModel { get; } = new();
    // private ValueSelectionViewModel ValueSelectionViewModel { get; } = new();
    // private DisplayImageViewModel DisplayImageViewModel { get; } = new();
    // private MenusAndPopupsViewModel MenusAndPopupsViewModel { get; } = new();

    private HomeViewModel HomeViewModel { get; } = new();
    private IconsViewModel IconsViewModel { get; } = new();
    private BasicInputViewModel BasicInputViewModel { get; } = new();
    private DialogBoxAndPopupViewModel DialogBoxAndPopupViewModel { get; } = new();
    private LayoutViewModel  LayoutViewModel { get; } = new();
    private NavigationViewModel  NavigationViewModel { get; } = new();
    private TextViewModel  TextViewModel { get; } = new();
    private ViewModel ViewModel  { get; } = new();
    private ScrollViewModel  ScrollViewModel { get; } = new();
    private StatusAndInformationViewModel StatusAndInformationViewModel { get; } = new();
    private MenuAndToolBarViewModel  MenuAndToolBarViewModel { get; } = new();
    private DateTimeViewModel   DateTimeViewModel { get; } = new();

    public SettingsViewModel SettingsViewModel { get; } = new();
    
    // [ObservableProperty]
    // private PageSlide.SlideAxis _transitioningOrientation = PageSlide.SlideAxis.Vertical;
    //
    // [ObservableProperty]
    // private TimeSpan _transitioningDuration = TimeSpan.FromMilliseconds(500);

    [RelayCommand]
    private void TogglePage(string page)
    {
        _viewModels.TryGetValue(page, out var target);
        if (target == CurrentViewModel)
        {
            return;
        }
        
        if (CurrentViewModel != null)
        {
            // 把当前页加入历史
            var currentPageKey = _viewModels.FirstOrDefault(x => x.Value == CurrentViewModel).Key;
            if (currentPageKey != null)
                _history.Add(currentPageKey);
        }

        CurrentViewModel = target;
        CanGoBack = _history.Count > 0;

#if DEBUG
        Debug.WriteLine($"Toggle Page To: {target}");
#endif
    }

    [RelayCommand(CanExecute = nameof(CanGoBack))]
    private void GoBack()
    {
        if (_history.Count <= 0)
            return;

        Console.WriteLine("Go Back");

        var last = _history[^1];
        _history.RemoveAt(_history.Count - 1);

        _viewModels.TryGetValue(last, out var view);
        CurrentViewModel = view;

        WeakReferenceMessenger.Default.Send(new JumpToControlMessage(last, null));
        // ViewModelChangedEvent?.Invoke(last, "");
        // Send Navigation GoBack Message
        // WeakReferenceMessenger.Default.Send(new NavigationGoBackMessage(last));

        CanGoBack = _history.Count > 0;
    }
}
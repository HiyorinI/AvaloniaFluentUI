using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Threading;
using Gallery.ViewModels;

namespace Gallery;

public class ViewLocator : IDataTemplate
{
    private readonly Dictionary<Type, Func<Control>> _factory = new();
    private readonly Dictionary<Type, WeakReference<Control>> _views = new();

    public ViewLocator()
    {
        Register();
    }

    private void Register()
    {
        _factory[typeof(HomeViewModel)] = () => new Views.HomeView();
        _factory[typeof(IconsViewModel)] = () => new Views.IconsView();
        _factory[typeof(BasicInputViewModel)] = () => new Views.BasicInputView();
        _factory[typeof(DialogBoxAndPopupViewModel)] = () => new Views.DialogBoxAndPopupView();
        _factory[typeof(LayoutViewModel)] = () => new Views.LayoutView();
        _factory[typeof(NavigationViewModel)] = () => new Views.NavigationView();
        _factory[typeof(TextViewModel)] = () => new Views.TextView();
        _factory[typeof(ViewModel)] = () => new Views.View();
        _factory[typeof(ScrollViewModel)] = () => new Views.ScrollView();
        _factory[typeof(StatusAndInformationViewModel)] = () => new Views.StatusAndInformationView();
        _factory[typeof(MenuAndToolBarViewModel)] = () => new Views.MenuAndToolBarView();
        _factory[typeof(DateTimeViewModel)] = () => new Views.DateTimeView();
        _factory[typeof(SettingsViewModel)] = () => new Views.SettingsView();
    }

    public async Task PreloadAllAsync()
    {
        foreach (var (type, factory) in _factory)
        {
            if (_views.TryGetValue(type, out var existing) && existing.TryGetTarget(out _))
                continue;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var view = factory();
                _views[type] = new WeakReference<Control>(view);
            }, DispatcherPriority.Background);

            await Task.Delay(15);
        }
    }

    public Control? Build(object? param)
    {
        Console.WriteLine(param);
        if (param is null) { return null; }
        var vmType = param.GetType();

        if (!_factory.TryGetValue(vmType, out var creator))
        {
            return new TextBlock
            {
                Text = $"View not registered: {vmType.Name}"
            };
        }

        if (_views.TryGetValue(vmType, out var weakRef) && weakRef.TryGetTarget(out var view))
        {
            return view;
        }

        view = creator();
        _views[vmType] = new WeakReference<Control>(view);
        return view;
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}

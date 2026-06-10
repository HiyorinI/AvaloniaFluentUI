using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Test.ViewModels;
using Test.Views;

namespace Test;

/// <summary>
/// Given a view model, returns the corresponding view if possible.
/// </summary>
[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
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
        _factory[typeof(MainViewModel)] = () => new MainView();
        _factory[typeof(FlipViewModel)] = () => new FlipView();
        _factory[typeof(NaviViewModel)] = () => new NaviView();
        _factory[typeof(CardViewModel)] = () => new CardView();
        _factory[typeof(ButtonViewModel)] = () => new ButtonView();
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

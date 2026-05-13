using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Gallery.ViewModels;

namespace Gallery;

[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
public class ViewLocator : IDataTemplate
{
    // 🔹 缓存 View 实例
    private readonly Dictionary<Type, Control> _cache = new();

    private async Task Init()
    {
        await Task.Run(() =>
        {
            var type = Type.GetType("Gallery.Views.LayoutControlsView");

            if (type != null)
            {
                var view = (Control)Activator.CreateInstance(type)!;
                _cache[type] = view;
                Console.WriteLine(type + "Null");
                Console.WriteLine(view + "Null");
            }
            else
            {
                Console.WriteLine("Type Is Null");
            }

            Console.WriteLine(_cache[type!]);
        });
    }

    public Control? Build(object? param)
    {
        if (param is null) { return null; }

        var viewTypeName = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var viewType = Type.GetType(viewTypeName);
        
        if (viewType == null) { return new TextBlock { Text = "Not Found: " + viewTypeName }; }
        if (_cache.TryGetValue(viewType, out var cachedView)) { return cachedView; }

        var view = (Control)Activator.CreateInstance(viewType)!;

        _cache[viewType] = view;
        return view;
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
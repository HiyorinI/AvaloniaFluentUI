using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;

namespace Gallery.Controls;

public class CodeCard : ContentControl
{
    public static readonly StyledProperty<object?> CodeContentProperty =
        AvaloniaProperty.Register<CodeCard, object?>(nameof(CodeContent));

    public static readonly StyledProperty<IDataTemplate?> CodeContentTemplateProperty =
        AvaloniaProperty.Register<CodeCard, IDataTemplate?>(nameof(CodeContentTemplate));

    public static readonly StyledProperty<double> CodeContentHeightProperty =
        AvaloniaProperty.Register<CodeCard, double>(nameof(CodeContentHeight));

    public static readonly StyledProperty<ICommand?> CodeContentCommandProperty =
        AvaloniaProperty.Register<CodeCard, ICommand?>(nameof(CodeContentCommand));

    public static readonly StyledProperty<string?> TitleProperty =
        AvaloniaProperty.Register<CodeCard, string?>(nameof(Title));

    public string? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    static CodeCard()
    {
        // CodeContentProperty.Changed.AddClassHandler<Card>((x, e) => 
        // x.OnHeaderChanged(e));
    }

    public double CodeContentHeight
    {
        get => GetValue(CodeContentHeightProperty);
        set => SetValue(CodeContentHeightProperty, value);
    }

    [DependsOn(nameof(CodeContentTemplate))]
    public object? CodeContent
    {
        get => GetValue(CodeContentProperty);
        set => SetValue(CodeContentProperty, value);
    }

    public IDataTemplate? CodeContentTemplate
    {
        get => GetValue(CodeContentTemplateProperty);
        set => SetValue(CodeContentTemplateProperty, value);
    }

    public ICommand? CodeContentCommand
    {
        get => GetValue(CodeContentCommandProperty);
        set => SetValue(CodeContentCommandProperty, value);
    }

    private void OnHeaderChanged(AvaloniaPropertyChangedEventArgs e)
    {
        // UpdatePseudoClasses();
    }

    // private void UpdatePseudoClasses()
    // {
    // var hasCodeContent = CodeContent != null;
    // PseudoClasses.Set(":has-header", hasCodeContent);
    // PseudoClasses.Set(":empty-header", !hasCodeContent);
    // }
}
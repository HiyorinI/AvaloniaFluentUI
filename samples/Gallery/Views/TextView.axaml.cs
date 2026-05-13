using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Gallery.Controls;

namespace Gallery.Views;

public partial class TextView : ViewBase 
{
    public TextView() : base("Text")
    {
        InitializeComponent();

        CodeCards = new Dictionary<string, CodeCard>()
        {
            {"TextBlock", TextBlockCard},
            {"TextBox", TextBoxCard},
            {"PasswordBox", PasswordBoxCard},
            {"NumberBox", NumberBoxCard}
        };
    }
    
    private void OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;

        if (textBox?.Text?.Length > 1)
        {
            textBox.Text = textBox.Text.Substring(0, 1);
        }
    }
}
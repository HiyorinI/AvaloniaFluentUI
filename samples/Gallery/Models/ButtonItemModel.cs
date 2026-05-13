using System;
using Avalonia.Media.Imaging;

namespace Gallery.Models;

public class ButtonItemModel
{
    public Bitmap Image { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public ButtonItemModel(Bitmap image, string title, string content)
    {
        Image = image;
        Title = title;
        Content = content;
    }
}
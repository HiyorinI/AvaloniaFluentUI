using Avalonia.Media;

namespace Gallery.Models;

public class CarouselData
{
    public string Content { get; set; }
    public IBrush Background { get; set; }

    public CarouselData(string content, IBrush background)
    {
        Content = content;
        Background = background;
    }
}
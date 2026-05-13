using Avalonia.Media;
using Avalonia.Styling;

namespace Gallery.Models;

public class AppConfig
{
    public string Theme { get; set; }
    public string AccentColor { get; set; }
    public bool IsWindowEffectEnabled { get; set; }
    public bool IsEnabledBackgroundImage { get; set; }
}
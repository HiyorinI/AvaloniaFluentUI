using Avalonia.Animation;

namespace Gallery.Messages.MainWindowMessages;

public record PageAnimationTypeChangedMessage(string Type, long Duration, PageSlide.SlideAxis SlideAxis = PageSlide.SlideAxis.Horizontal);

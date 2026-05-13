using Avalonia.Media;

namespace Gallery.Messages.IconViewMessages;

public record CheckedIconChangedMessage(string Name, Geometry? Data);

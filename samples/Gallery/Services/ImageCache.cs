using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Gallery.Services;

public static class ImageCache
{
    private static readonly Dictionary<string, WeakReference<Bitmap>> Cache = new();

    public static Bitmap? GetOrCreate(string uri, int decodeHeight = 0)
    {
        if (Cache.TryGetValue(uri, out var weakRef) && weakRef.TryGetTarget(out var cached))
        {
            return cached;
        }

        try
        {
            using var stream = AssetLoader.Open(new Uri(uri));
            Bitmap bitmap;
            if (decodeHeight > 0)
                bitmap = Bitmap.DecodeToHeight(stream, decodeHeight);
            else
                bitmap = new Bitmap(stream);

            Cache[uri] = new WeakReference<Bitmap>(bitmap);
            return bitmap;
        }
        catch
        {
            return null;
        }
    }

    public static void Clear()
    {
        Cache.Clear();
    }
}

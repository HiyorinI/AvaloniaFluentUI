using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace Gallery.Views;

public partial class DrawRectView : UserControl
{
    private readonly Size _size = new Size(100, 100);
    private readonly double _margin = 10;
    private readonly double _padding = 15;
    
    public DrawRectView()
    {
        InitializeComponent();

        // var grid = new Grid { RowDefinitions = new RowDefinitions("Auto, Auto, Auto") };
        // var rst = new TextBlock { Text = $"Rect Size: {_size.Width} x {_size.Height}", TextAlignment = TextAlignment.Center};
        // var mt = new TextBlock {Text = $"Margin: {_margin}"};
        // var pt = new TextBlock {Text = $"Padding: {_padding}"};
        //
        // Content = grid;
        // grid.Children.AddRange([rst, mt, pt]);
        //
        // Grid.SetRow(rst, 0);
        // Grid.SetRow(mt, 1);
        // Grid.SetRow(pt, 2);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        // var brush = Brushes.DeepSkyBlue;
        var brush = Brush.Parse("#A396FF");
        
        double columns = Bounds.Width / (_size.Width + _margin);
        double rows = Bounds.Height / (_size.Height + _margin);

        for (int r = 0; r <= rows - 1; r++)
        {
            for (int c = 0; c <= columns - 1; c++)
            {
                double x = _padding + c * (_size.Width + _margin);
                double y = _padding + r * (_size.Height + _margin);

                // Draw Rect
                context.DrawRectangle(brush, null, new Rect(x, y, _size.Width, _size.Height), 10, 10);

                // Draw Text
                var text = new FormattedText($"{r},{c}", CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    Typeface.Default, 24, Brushes.White);

                 x += (_size.Width - text.Width) / 2;
                 y += (_size.Height - text.Height) / 2;
                
                context.DrawText(text, new Point(x, y));
            }
        }
    }
}
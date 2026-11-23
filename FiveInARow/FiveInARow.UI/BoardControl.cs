using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using FiveInARow.Core.Models;

namespace FiveInARow.UI;

public class BoardControl : Control
{
    private Board? _board;

    public event Action<int, int>? cell_clicked;

    public void setBoard(Board board)
    {
        _board = board;
        Width = BoardGeom.sizeFor(board.Width);
        Height = BoardGeom.sizeFor(board.Height);
        InvalidateVisual();
    }

    public void Redraw() => InvalidateVisual();

    public override void Render(DrawingContext context)
    {
        // background
        context.DrawRectangle(Brushes.BurlyWood, null, new Rect(Bounds.Size));
        
        if (_board is null) return;

        var linePen = new Pen(Brushes.Black, 1);
        
        // grid
        for (int row = 1; row <= _board.Height; ++row)
        {
            var (x1, y) = BoardGeom.toPixel(row, 1);
            var (x2, _) = BoardGeom.toPixel(row, _board.Width);
            
            context.DrawLine(linePen, new Point(x1, y), new Point(x2, y));
        }

        for (int col = 1; col <= _board.Width; ++col)
        {
            var (x, y1) = BoardGeom.toPixel(1, col);
            var (_, y2) = BoardGeom.toPixel(_board.Height, col);
            
            context.DrawLine(linePen, new Point(x, y1), new Point(x, y2));

        }
        
        // stones

        double radius = BoardGeom.cell_size * 0.42;

        for (int row = 1; row <= _board.Height; ++row)
        {
            for (int col = 1; col <= _board.Width; ++col)
            {
                int mark = _board.Get(row, col);
                if (mark == 0)
                    continue;

                var (x, y) = BoardGeom.toPixel(row, col);

                IBrush fill = mark == 1 ? Brushes.Black : Brushes.White;
                context.DrawEllipse(fill, linePen, new Point(x, y), radius, radius);
                
            }
        }

    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        
        if (_board is null)
            return;

        var p = e.GetPosition(this);
        if (BoardGeom.tryGetCell(p.X, p.Y, _board.Height, _board.Width, out int row, out int col))
            cell_clicked?.Invoke(row, col);

    }
}
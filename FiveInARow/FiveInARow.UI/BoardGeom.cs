using System;

namespace FiveInARow.UI;

public static class BoardGeom
{
    public const double margin = 20;
    public const double cell_size = 40;

    public static double sizeFor(int cells) => margin * 2 + cell_size * (cells - 1);

    public static (double x, double y) toPixel(int row, int col) =>
        (margin + (col - 1) * cell_size, margin + (row - 1) * cell_size);

    public static bool tryGetCell(double x, double y, int height, int width, out int row, out int col)
    {
        col = (int)Math.Round((x - margin) / cell_size) + 1;
        row = (int)Math.Round((y - margin) / cell_size) + 1;

        return row >= 1 && row <= height && col >= 1 && col <= width;
    }
}
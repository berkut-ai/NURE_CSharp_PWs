namespace FiveInARow.Core.Models;

public class Move
{
    private int row, col, val;

    public Move()
    {
        
    }
    public Move(int r, int c, int v)
    {
        row = r;
        col = c;
        val = v;
    }

    public int Row
    {
        get => row;
        set => row = value;
    }

    public int Col
    {
        get => col;
        set => col = value;
    }

    public int Val
    {
        get => val;
        set => val = value;
    }
}
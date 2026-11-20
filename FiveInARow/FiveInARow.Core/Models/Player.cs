namespace FiveInARow.Core.Models;

public class Player : IPlayer
{
    
    protected const int _mark = 1;

    public static int Mark
    {
        get { return _mark; }
    }
    
    public Player(){}

    public Move MakeMove(IBoard board, int row, int col)
    {
        Move move = new Move();
        move.Val = _mark;

        move.Row = row;
        move.Col = col;

        return move;
    }
}




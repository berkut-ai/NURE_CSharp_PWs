using FiveInARow.Core.Models.Strategies;

namespace FiveInARow.Core.Models;


public class Bot : Player2
{
    private IBotStrategy _strategy;

    public Bot(IBotStrategy strategy)
    {
        _strategy = strategy;
    }

    public Move MakeMove(Board board)
    {
        Move move = _strategy.MakeMove(board);

        move.Val = _mark;
        
        return move;
    }
}
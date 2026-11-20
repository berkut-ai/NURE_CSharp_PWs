namespace FiveInARow.Core.Models.Strategies;

public interface IBotStrategy
{
    public Move MakeMove(Board board);
}
namespace FiveInARow.Core.Models;
public interface IPlayer
{
    public Move MakeMove(IBoard board, int row, int col);
}
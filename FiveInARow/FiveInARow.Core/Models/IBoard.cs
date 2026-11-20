using FiveInARow.Core.Models.Memento;

namespace FiveInARow.Core.Models;
public interface IBoard
{ 
    public int Get(int row, int col);
    public void Set(Move move);

    public int CheckWinner();

    public IBoardMemento Save();
    public void Restore(IBoardMemento b_memento);
}
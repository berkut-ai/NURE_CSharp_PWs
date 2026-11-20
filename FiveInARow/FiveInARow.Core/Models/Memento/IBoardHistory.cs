namespace FiveInARow.Core.Models.Memento;

public interface IBoardHistory
{
    public void Store(IBoardMemento b_memento);
    public IBoardMemento? Pop();
}
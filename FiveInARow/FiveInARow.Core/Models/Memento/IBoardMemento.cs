namespace FiveInARow.Core.Models.Memento;

public interface IBoardMemento
{
    public (int[,], int) GetState();
}
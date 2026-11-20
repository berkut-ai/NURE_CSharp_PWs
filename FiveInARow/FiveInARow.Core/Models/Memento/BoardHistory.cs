namespace FiveInARow.Core.Models.Memento;

public class BoardHistory : IBoardHistory
{
    private Stack<IBoardMemento> _stack = new();

    public void Store(IBoardMemento b_memento)
    {
        _stack.Push(b_memento);
    }

    public IBoardMemento? Pop()
    {
        try
        {
            return _stack.Pop();
        }
        catch
        {
            return null;
        }
    }
}
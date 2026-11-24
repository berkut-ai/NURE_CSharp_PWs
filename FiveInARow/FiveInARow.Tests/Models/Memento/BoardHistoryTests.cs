using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Memento;

namespace FiveInARow.Tests.Models.Memento;

public class BoardHistoryTests
{
    [Fact]
    public void Pop_WhenEmpty_ReturnsNull()
    {
        Assert.Null(new BoardHistory().Pop());
    }

    [Fact]
    public void Pop_ReturnsSnapshotsInLastInFirstOutOrder()
    {
        var history = new BoardHistory();
        var first = new Board().Save();
        var board = new Board();
        board.Set(new Move(2, 3, Player.Mark));
        var second = board.Save();

        history.Store(first);
        history.Store(second);

        Assert.Same(second, history.Pop());
        Assert.Same(first, history.Pop());
        Assert.Null(history.Pop());
    }

    [Fact]
    public void Clear_RemovesStoredSnapshots()
    {
        var history = new BoardHistory();
        history.Store(new Board().Save());

        history.Clear();

        Assert.Equal(0, history.Count);
        Assert.Null(history.Pop());
    }
}

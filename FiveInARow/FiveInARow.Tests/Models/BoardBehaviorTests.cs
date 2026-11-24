using FiveInARow.Core.Models;

namespace FiveInARow.Tests.Models;

public class BoardBehaviorTests
{
    [Fact]
    public void CheckWinner_OnEmptyBoard_ReturnsZero()
    {
        Assert.Equal(0, new Board().CheckWinner());
    }

    [Theory]
    [InlineData(1, 1, 0, 1)]
    [InlineData(1, 1, 1, 0)]
    [InlineData(1, 1, 1, 1)]
    [InlineData(5, 1, -1, 1)]
    public void CheckWinner_DetectsFiveInEveryDirection(int startRow, int startCol, int rowStep, int colStep)
    {
        var board = new Board();

        for (var i = 0; i < 5; i++)
        {
            board.Set(new Move
            {
                Row = startRow + i * rowStep,
                Col = startCol + i * colStep,
                Val = Player.Mark
            });
        }

        Assert.Equal(Player.Mark, board.CheckWinner());
    }

    [Fact]
    public void CheckWinner_DoesNotTreatFourInARowAsVictory()
    {
        var board = new Board();
        for (var col = 1; col <= 4; col++)
            board.Set(new Move(8, col, Player2.Mark));

        Assert.Equal(0, board.CheckWinner());
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(16, 1)]
    [InlineData(1, 16)]
    public void Set_RejectsCoordinatesOutsideBoard(int row, int col)
    {
        var board = new Board();

        Assert.Throws<IndexOutOfRangeException>(() => board.Set(new Move(row, col, Player.Mark)));
    }

    [Fact]
    public void SetSameValue_DoesNotChangeFreeCellCount()
    {
        var board = new Board();
        var move = new Move(3, 4, Player.Mark);

        board.Set(move);
        board.Set(move);

        Assert.Equal(224, board.FreeCells);
    }

    [Fact]
    public void Clear_EmptiesBoardAndResetsFreeCellCount()
    {
        var board = new Board();
        board.Set(new Move(3, 4, Player.Mark));

        board.Clear();

        Assert.Equal(0, board.Get(3, 4));
        Assert.Equal(225, board.FreeCells);
    }

    [Fact]
    public void Restore_NullMemento_LeavesBoardUnchanged()
    {
        var board = new Board();
        board.Set(new Move(3, 4, Player.Mark));

        board.Restore(null);

        Assert.Equal(Player.Mark, board.Get(3, 4));
        Assert.Equal(224, board.FreeCells);
    }

    [Fact]
    public void Restore_UsesIndependentSnapshot()
    {
        var board = new Board();
        board.Set(new Move(3, 4, Player.Mark));
        var snapshot = board.Save();

        board.Set(new Move(3, 4, Player2.Mark));
        board.Restore(snapshot);

        var (state, freeCells) = snapshot.GetState();
        state[2, 3] = Player2.Mark;
        var (unchangedState, _) = snapshot.GetState();

        Assert.Equal(Player.Mark, board.Get(3, 4));
        Assert.Equal(224, freeCells);
        Assert.Equal(Player.Mark, unchangedState[2, 3]);
    }
}

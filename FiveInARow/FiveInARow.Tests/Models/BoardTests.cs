using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Memento;

namespace FiveInARow.Tests.Models;

public class BoardTests
{
    [Fact]
    public void NewBoard_IsEmptyAndHasExpectedDimensions()
    {
        var board = new Board();

        Assert.Equal(15, board.Height);
        Assert.Equal(15, board.Width);
        
        Assert.Equal(225, board.FreeCells);
        Assert.Equal(0, board.Get(1, 1));
    }
    

    [Fact]
    public void SetAndGet_StoresPlayerMark()
    {
        var board = new Board();

        
        board.Set(new Move { Row = 4, Col = 7, Val = Player.Mark });

        Assert.Equal(Player.Mark, board.Get(4, 7));
        
        Assert.Equal(224, board.FreeCells);
    }

    [Fact]
    public void SetToZero_FreesAnOccupiedCell()
    {
        var board = new Board();
        board.Set(new Move { Row = 4, Col = 7, Val = Player.Mark });

        board.Set(new Move { Row = 4, Col = 7, Val = 0 });

        
        
        Assert.Equal(0, board.Get(4, 7));
        Assert.Equal(225, board.FreeCells);
    }

    
    
    [Fact]
    public void ReplacingOnePlayerMarkWithAnother_DoesNotChangeFreeCellCount()
    {
        var board = new Board();
        board.Set(new Move { Row = 4, Col = 7, Val = Player.Mark });

        board.Set(new Move { Row = 4, Col = 7, Val = Player2.Mark });

        Assert.Equal(Player2.Mark, board.Get(4, 7));
        Assert.Equal(224, board.FreeCells);
    }

    
    
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(16, 1)]
    [InlineData(1, 16)]
    public void Get_RejectsCoordinatesOutsideTheBoard(int row, int col)
    {
        var board = new Board();
        

        Assert.Throws<IndexOutOfRangeException>(() => board.Get(row, col));
    }

    [Fact]
    public void Get_AllowsLastRowAndColumn()
    {
        var board = new Board();

        Assert.Equal(0, board.Get(board.Height, board.Width));
    }

    
    
    [Fact]
    public void Set_RejectsMarksOutsideZeroThroughTwo()
    {
        var board = new Board();

        Assert.Throws<ArgumentException>(() =>
            board.Set(new Move { Row = 1, Col = 1, Val = 3 }));
    }

    
    
    [Fact]
    public void Restore_ReturnsBoardToSavedState()
    {
        var board = new Board();
        board.Set(new Move { Row = 4, Col = 7, Val = Player.Mark });
        var memento = board.Save();
        board.Set(new Move { Row = 4, Col = 7, Val = Player2.Mark });
        board.Set(new Move { Row = 5, Col = 7, Val = Player2.Mark });

        board.Restore(memento);

        
        
        Assert.Equal(Player.Mark, board.Get(4, 7));
        Assert.Equal(0, board.Get(5, 7));
        
        Assert.Equal(224, board.FreeCells);
    }

    [Fact]
    public void Restore_IgnoresMementoFromAnotherImplementation()
    {
        var board = new Board();
        board.Set(new Move { Row = 4, Col = 7, Val = Player.Mark });

        board.Restore(new ForeignMemento());
        

        Assert.Equal(Player.Mark, board.Get(4, 7));
        
        Assert.Equal(224, board.FreeCells);
    }
    

    private sealed class ForeignMemento : IBoardMemento
    {
        
        
    }
}

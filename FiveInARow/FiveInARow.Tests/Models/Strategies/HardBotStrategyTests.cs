using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Strategies;

namespace FiveInARow.Tests.Models.Strategies;

public class HardBotStrategyTests
{
    [Fact]
    public void MakeMove_OnEmptyBoard_ChoosesCenter()
    {
        var move = new HardBotStrategy().MakeMove(new Board());

        Assert.Equal(8, move.Row);
        
        Assert.Equal(8, move.Col);
    }

    [Fact]
    public void MakeMove_OnOccupiedBoard_ScoresAvailableMovesWithoutChangingBoard()
    {
        var board = new Board();
        for (var col = 1; col < board.Width; col++)
        {
            board.Set(new Move { Row = 1, Col = col, Val = Player.Mark });
            
        }

        for (var row = 2; row < board.Height; row++)
        {
            
            board.Set(new Move { Row = row, Col = 1, Val = Player.Mark });
            
            board.Set(new Move { Row = row, Col = 2, Val = Player2.Mark });
        }

        for (var col = 2; col < board.Width; col++)
        {
            board.Set(new Move { Row = 2, Col = col, Val = Player2.Mark });
        }

        AddLine(board, 4, 4, 4, Player2.Mark);
        AddLine(board, 5, 4, 3, Player2.Mark);
        
        AddLine(board, 6, 4, 2, Player2.Mark);
        AddLine(board, 7, 4, 1, Player2.Mark);
        

        AddLine(board, 9, 4, 3, Player2.Mark);
        board.Set(new Move { Row = 9, Col = 3, Val = Player.Mark });

        AddLine(board, 10, 4, 2, Player2.Mark);
        
        board.Set(new Move { Row = 10, Col = 3, Val = Player.Mark });
        

        board.Set(new Move { Row = 11, Col = 5, Val = Player2.Mark });
        board.Set(new Move { Row = 11, Col = 4, Val = Player.Mark });

        
        
        board.Set(new Move { Row = 12, Col = 7, Val = Player.Mark });
        board.Set(new Move { Row = 12, Col = 9, Val = Player.Mark });

        
        AddLine(board, 13, 4, 3, Player2.Mark);
        board.Set(new Move { Row = 13, Col = 3, Val = Player.Mark });
        board.Set(new Move { Row = 13, Col = 8, Val = Player.Mark });

        var move = new HardBotStrategy().MakeMove(board);

        Assert.InRange(move.Row, 2, 14);
        Assert.InRange(move.Col, 2, 14);
        Assert.Equal(0, board.Get(move.Row, move.Col));
    }

    private static void AddLine(Board board, int row, int firstColumn, int length, int mark)
    {
        for (var col = firstColumn; col < firstColumn + length; col++)
        {
            board.Set(new Move { Row = row, Col = col, Val = mark });
        }
    }
}

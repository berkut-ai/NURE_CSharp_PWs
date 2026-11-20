using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Strategies;

namespace FiveInARow.Tests.Models.Strategies;

public class MediumBotStrategyTests
{
    [Fact]
    public void MakeMove_OnEmptyBoard_ChoosesCenter()
    {
        var strategy = new MediumBotStrategy();

        var move = strategy.MakeMove(new Board());

        Assert.Equal(8, move.Row);
        Assert.Equal(8, move.Col);
    }

    [Fact]
    public void MakeMove_WhenBotCanCompleteFive_ReturnsWinningCell()
    {
        var board = CreateTacticalBoard(Player2.Mark, Player.Mark);

        var move = new MediumBotStrategy().MakeMove(board);

        Assert.Equal(1, move.Row);
        Assert.Equal(6, move.Col);
    }

    [Fact]
    public void MakeMove_WhenPlayerCanCompleteFive_BlocksWinningCell()
    {
        var board = CreateTacticalBoard(Player.Mark, Player2.Mark);

        var move = new MediumBotStrategy().MakeMove(board);

        Assert.Equal(1, move.Row);
        Assert.Equal(6, move.Col);
    }

    [Fact]
    public void MakeMove_WhenNoTacticalMoveExists_FallsBackToEasyStrategy()
    {
        var board = new Board();
        for (var row = 1; row < board.Height; row++)
        {
            
            for (var col = 1; col < board.Width; col++)
            {
                board.Set(new Move { Row = row, Col = col, Val = Player.Mark });
            }
        }

        var move = new MediumBotStrategy().MakeMove(board);

        Assert.Equal(14, move.Row);
        Assert.Equal(15, move.Col);
    }

    private static Board CreateTacticalBoard(int lineMark, int blockingMark)
    {
        var board = new Board();
        board.Set(new Move { Row = 1, Col = 1, Val = blockingMark });
        

        for (var col = 2; col <= 5; col++)
        {
            board.Set(new Move { Row = 1, Col = col, Val = lineMark });
        }

        
        return board;
    }
}

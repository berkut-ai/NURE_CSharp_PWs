using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Strategies;

namespace FiveInARow.Tests.Components;

public class TurnFlowTests
{
    [Fact]
    public void PlayerAndBotMoves_AreAppliedToTheSameBoard()
    {
        var board = new Board();
        var player = new Player();
        var bot = new Bot(new FixedStrategy(8, 8));

        board.Set(player.MakeMove(board, 8, 7));
        board.Set(bot.MakeMove(board));

        Assert.Equal(Player.Mark, board.Get(8, 7));
        Assert.Equal(Player2.Mark, board.Get(8, 8));
        Assert.Equal(223, board.FreeCells);
    }

    private sealed class FixedStrategy(int row, int col) : IBotStrategy
    {
        public Move MakeMove(Board board) => new() { Row = row, Col = col };
    }
}

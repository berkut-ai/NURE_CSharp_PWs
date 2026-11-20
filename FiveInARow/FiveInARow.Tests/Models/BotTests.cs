using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Strategies;

namespace FiveInARow.Tests.Models;

public class BotTests
{
    [Fact]
    public void MakeMove_UsesStrategyPositionAndApplisBoMark()
    {
        var board = new Board();
        
        var strategy = new FixedStrategy(3, 11, 1);
        var bot = new Bot(strategy);

        
        var move = bot.MakeMove(board);

        Assert.Same(board, strategy.ReceivedBoard);
        Assert.Equal(3, move.Row);
        Assert.Equal(11, move.Col);
        Assert.Equal(Player2.Mark, move.Val);
    }

    private sealed class FixedStrategy(int row, int col, int value) : IBotStrategy
    {
        public Board? ReceivedBoard { get; private set; }

        public Move MakeMove(Board board)
        {
            ReceivedBoard = board;
            return new Move { Row = row, Col = col, Val = value };
        }
    }
}

using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Strategies;

namespace FiveInARow.Tests.Models.Strategies;

public class StrategyFallbackTests
{
    [Fact]
    public void EasyStrategy_WhenBotHasNoSafeAdjacentCell_UsesLastFreeCell()
    {
        var board = new Board();
        board.Set(new Move(1, 1, Player2.Mark));

        var move = new EasyBotStrategy().MakeMove(board);

        Assert.Equal(15, move.Row);
        Assert.Equal(15, move.Col);
    }

    [Fact]
    public void MediumStrategy_WithoutImmediateWinOrBlock_FallsBackToEasy()
    {
        var board = new Board();

        var move = new MediumBotStrategy().MakeMove(board);

        Assert.Equal(15, move.Row);
        Assert.Equal(15, move.Col);
    }

    [Fact]
    public void BaseStrategy_WithoutOverride_ReportsNotImplemented()
    {
        Assert.Throws<NotImplementedException>(() => new UnimplementedStrategy().MakeMove(new Board()));
    }

    private sealed class UnimplementedStrategy : BotStrategy
    {
    }
}

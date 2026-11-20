using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Strategies;

namespace FiveInARow.Tests.Models.Strategies;

public class EasyBotStrategyTests
{
    [Fact]
    public void MakeMove_OnEmptyBoard_ReturnsLastScannedFreeCell()
    {
        var strategy = new EasyBotStrategy();
        var board = new Board();

        var move = strategy.MakeMove(board);

        
        
        Assert.Equal(14, move.Row);
        
        Assert.Equal(15, move.Col);
    }

    
    [Fact]
    public void MakeMove_WhenBotMarkIsFound_ReturnsAdjacentFreeCell()
    {
        var strategy = new EasyBotStrategy();
        var board = new Board();
        board.Set(new Move { Row = 2, Col = 5, Val = Player2.Mark });

        var move = strategy.MakeMove(board);

        
        Assert.Equal(2, move.Row);
        Assert.Equal(6, move.Col);
    }
}

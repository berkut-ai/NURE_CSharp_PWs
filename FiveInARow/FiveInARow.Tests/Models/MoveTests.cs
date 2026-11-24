using FiveInARow.Core.Models;

namespace FiveInARow.Tests.Models;

public class MoveTests
{
    [Fact]
    public void DefaultConstructor_InitializesCoordinatesAndValueToZero()
    {
        var move = new Move();

        Assert.Equal(0, move.Row);
        Assert.Equal(0, move.Col);
        Assert.Equal(0, move.Val);
    }

    [Fact]
    public void ParameterizedConstructor_AndPropertiesStoreMoveData()
    {
        var move = new Move(2, 7, Player.Mark)
        {
            Row = 5,
            Col = 9,
            Val = Player2.Mark
        };

        Assert.Equal(5, move.Row);
        Assert.Equal(9, move.Col);
        Assert.Equal(Player2.Mark, move.Val);
    }
}

using FiveInARow.Core.Models;

namespace FiveInARow.Tests.Models;

public class PlayerTests
{
    [Fact]
    public void PlayerMark_IsOne()
    {
        Assert.Equal(1, Player.Mark);
    }

    [Fact]
    public void Player2Mark_IsTwo()
    {
        Assert.Equal(2, Player2.Mark);
    }

    [Fact]
    public void MakeMove_ReturnsRequestedPositionAndPlayerMark()
    {
        var player = new Player();
        var board = new Board();

        var move = player.MakeMove(board, 6, 9);

        Assert.Equal(6, move.Row);
        Assert.Equal(9, move.Col);
        Assert.Equal(Player.Mark, move.Val);
        Assert.Equal(0, board.Get(6, 9));
    }
}

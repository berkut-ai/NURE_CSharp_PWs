using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Strategies;

namespace FiveInARow.Tests.Models.Strategies;

public class BotStrategyTests
{
    [Fact]
    public void BaseStrategy_MakeMove_IsNotImplemented()
    {
        Assert.Throws<NotImplementedException>(() => new BotStrategy().MakeMove(new Board()));
    }
}

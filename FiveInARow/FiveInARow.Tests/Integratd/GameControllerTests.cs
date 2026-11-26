using FiveInARow.Core.Controllers;
using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Strategies;

namespace FiveInARow.Tests.Integratd;

public class GameControllerTests
{
    [Fact]
    public void HandleClick_AppliesPlayerAndBotMovesToBoard()
    {
        var board = new Board();
        var strategy = new FixedStrategy(8, 8);
        var controller = new GameController(board, strategy);
        

        Assert.True(controller.HandleCLick(4, 5));

        Assert.Equal(Player.Mark, board.Get(4, 5));
        Assert.Equal(Player2.Mark, board.Get(8, 8));
        
        Assert.Equal(223, board.FreeCells);
        Assert.Equal(GameResult.None, controller.Result);
        Assert.True(controller.canUndo);
        Assert.Equal(1, strategy.CallCount);
    }

    
    
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(16, 1)]
    [InlineData(1, 16)]
    public void HandleClick_InvalidCoordinatesAreIgnored(int row, int col)
    {
        var board = new Board();
        var strategy = new FixedStrategy(8, 8);
        
        var controller = new GameController(board, strategy);

        Assert.False(controller.HandleCLick(row, col));

        Assert.Equal(225, board.FreeCells);
        Assert.Equal(0, strategy.CallCount);
        
        Assert.False(controller.canUndo);
    }

    
    
    [Fact]
    public void HandleClick_OccupiedCellIsIgnored()
    {
        var board = new Board();
        board.Set(new Move(4, 5, Player.Mark));
        var strategy = new FixedStrategy(8, 8);
        
        var controller = new GameController(board, strategy);
        

        Assert.False(controller.HandleCLick(4, 5));

        Assert.Equal(224, board.FreeCells);
        Assert.Equal(0, strategy.CallCount);
    }

    [Fact]
    public void HandleClick_PlayerWinningMoveEndsGameWithoutCallingBot()
    {
        var board = new Board();
        for (var col = 1; col <= 4; col++)
            board.Set(new Move(7, col, Player.Mark));
        
        var strategy = new FixedStrategy(10, 10);
        var controller = new GameController(board, strategy);

        
        Assert.True(controller.HandleCLick(7, 5));

        Assert.Equal(GameResult.PlayerWon, controller.Result);
        
        Assert.Equal(0, strategy.CallCount);
        Assert.Equal(Player.Mark, board.Get(7, 5));
    }

    [Fact]
    public void HandleClick_BotWinningMoveEndsGame()
    {
        var board = new Board();
        for (var col = 1; col <= 4; col++)
            board.Set(new Move(7, col, Player2.Mark));
        var controller = new GameController(board, new FixedStrategy(7, 5));

        Assert.True(controller.HandleCLick(10, 10));
        

        Assert.Equal(GameResult.BotWon, controller.Result);
        Assert.Equal(Player2.Mark, board.Get(7, 5));
    }

    
    
    [Fact]
    public void HandleClick_FillingLastCellEndsInDraw()
    {
        var board = CreateNearlyFullNonWinningBoard((15, 15));
        Assert.Equal(0, board.CheckWinner());
        var strategy = new FixedStrategy(8, 8);
        
        var controller = new GameController(board, strategy);

        Assert.True(controller.HandleCLick(15, 15));

        Assert.Equal(GameResult.Draw, controller.Result);
        
        Assert.Equal(0, board.FreeCells);
        Assert.Equal(0, strategy.CallCount);
    }

    
    
    [Fact]
    public void HandleClick_BotFillingLastCellEndsInDraw()
    {
        var board = CreateNearlyFullNonWinningBoard((15, 14), (15, 15));
        Assert.Equal(0, board.CheckWinner());
        var strategy = new FixedStrategy(15, 15);
        
        var controller = new GameController(board, strategy);

        Assert.True(controller.HandleCLick(15, 14));

        Assert.Equal(GameResult.Draw, controller.Result);
        
        Assert.Equal(0, board.FreeCells);
        Assert.Equal(1, strategy.CallCount);
    }

    
    
    [Fact]
    public void Undo_WithoutPreviousMoveReturnsFalse()
    {
        var controller = new GameController(new Board(), new FixedStrategy(8, 8));

        Assert.False(controller.Undo());
    }

    
    
    [Fact]
    public void Undo_RestoresBoardAndClearsGameResult()
    {
        var board = new Board();
        
        for (var col = 1; col <= 4; col++)
            board.Set(new Move(7, col, Player.Mark));
        var controller = new GameController(board, new FixedStrategy(8, 8));
        controller.HandleCLick(7, 5);

        Assert.True(controller.Undo());

        Assert.Equal(0, board.Get(7, 5));
        Assert.Equal(GameResult.None, controller.Result);
        
        Assert.Equal(221, board.FreeCells);
    }

    [Fact]
    public void NewGame_ClearsBoardHistoryAndResult()
    {
        var board = new Board();
        var controller = new GameController(board, new FixedStrategy(8, 8));
        
        controller.HandleCLick(4, 5);

        controller.newGame();

        Assert.Equal(0, board.Get(4, 5));
        Assert.Equal(0, board.Get(8, 8));
        
        Assert.Equal(GameResult.None, controller.Result);
        
        Assert.False(controller.canUndo);
    }

    [Theory]
    [InlineData(Difficulty.Easy, typeof(EasyBotStrategy))]
    [InlineData(Difficulty.Medium, typeof(MediumBotStrategy))]
    [InlineData(Difficulty.Hard, typeof(HardBotStrategy))]
    public void SetDifficulty_SelectsRequestedStrategy(Difficulty difficulty, Type strategyType)
    {
        var controller = new GameController(new Board(), new FixedStrategy(8, 8));

        controller.setDifficulty(difficulty);
        

        Assert.IsType(strategyType, controller.Strategy);
    }

    [Fact]
    public void SetDifficulty_UnknownValueFallsBackToEasy()
    {
        var controller = new GameController(new Board(), new FixedStrategy(8, 8));

        controller.setDifficulty((Difficulty)99);

        Assert.IsType<EasyBotStrategy>(controller.Strategy);
    }

    [Fact]
    public void HandleClick_AfterPlayerWinIsIgnored()
    {
        var board = new Board();
        for (var col = 1; col <= 4; col++)
            board.Set(new Move(7, col, Player.Mark));
        var strategy = new FixedStrategy(8, 8);
        var controller = new GameController(board, strategy);
        controller.HandleCLick(7, 5);

        Assert.False(controller.HandleCLick(10, 10));
        Assert.Equal(0, strategy.CallCount);
    }

    private static Board CreateNearlyFullNonWinningBoard(params (int row, int col)[] emptyCells)
    {
        var board = new Board();
        for (var row = 1; row <= board.Height; row++)
        {
            for (var col = 1; col <= board.Width; col++)
            {
                if (emptyCells.Contains((row, col)))
                    continue;

                var mark = (row + 2 * col) % 4 < 2 ? Player.Mark : Player2.Mark;
                board.Set(new Move(row, col, mark));
            }
        }

        return board;
    }

    private sealed class FixedStrategy(int row, int col) : IBotStrategy
    {
        public int CallCount { get; private set; }

        public Move MakeMove(Board board)
        {
            CallCount++;
            return new Move(row, col, Player.Mark);
        }
    }
}

using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Memento;
using FiveInARow.Core.Models.Strategies;

namespace FiveInARow.Core.Controllers;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public enum GameResult
{
    None,
    PlayerWon,
    BotWon,
    Draw
}

public class GameController
{
    public static readonly int player_mark = Player.Mark;
    public static readonly int bot_mark = Bot.Mark;

    private readonly BoardHistory _history = new();
    
    public Board Board { get; }
    public IBotStrategy Strategy { get; set; }
    public GameResult Result { get; private set; } = GameResult.None;

    public bool isGameOver => Result != GameResult.None;
    public bool canUndo => _history.Count > 0;

    public GameController(Board board, IBotStrategy strategy)
    {
        Board = board;
        Strategy = strategy;
    }

    public bool HandleCLick(int row, int col)
    {
        if (isGameOver) return false;
        if (!Board.checkIndexes(row, col) || Board.Get(row, col) != 0) return false;
        
        _history.Store(Board.Save());
        Board.Set(new Move(row, col, player_mark));

        if ( Board.CheckWinner() == player_mark)
        {
            Result = GameResult.PlayerWon;
            return true;
        }

        if (Board.FreeCells == 0)
        {
            Result = GameResult.Draw;
            return true;
        }

        Move move = Strategy.MakeMove(Board);
        move.Val = bot_mark; 
        Board.Set(move);
        
        if ( Board.CheckWinner() == bot_mark)
        {
            Result = GameResult.BotWon;
            return true;
        }

        if (Board.FreeCells == 0)
        {
            Result = GameResult.Draw;
            return true;
        }

        return true;
    }

    public bool Undo()
    {
        if (!canUndo)
            return false;
        Board.Restore(_history.Pop());
        Result = GameResult.None;
        return true;
    }

    public void newGame()
    {
        Board.Clear();
        _history.Clear();
        Result = GameResult.None;
    }

    public void setDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                Strategy = new EasyBotStrategy();
                break;
            case Difficulty.Medium:
                Strategy = new MediumBotStrategy();
                break;
            case Difficulty.Hard:
                Strategy = new HardBotStrategy();
                break;
            default:
                Strategy = new EasyBotStrategy();
                break;
        }
        
    }
}

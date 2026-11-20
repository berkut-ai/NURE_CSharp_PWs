namespace FiveInARow.Core.Models.Strategies;

public class HardBotStrategy : BotStrategy
{
    private readonly Random _rng = new Random();
    private static long evaluateValue(Board board, int row, int col, int mark)
    {
        long total = 0;
        foreach (var (dr, dc) in dirs)
        {
            int fwd = cntDir(board, row, col, dr, dc, mark);
            int back = cntDir(board, row, col, -dr, -dc, mark);
            int len = 1 + fwd + back; // len of chain if put at (row, col)

            // is open at one side
            bool openF = board.checkIndexes(row + dr * (fwd + 1), col + dc * (fwd + 1))
                         && board.Get(row + dr * (fwd + 1), col + dc * (fwd + 1)) == 0;
            // is open form another
            bool openB = board.checkIndexes(row - dr * (back + 1), col - dc * (back + 1))
                         && board.Get(row - dr * (back + 1), col - dc * (back + 1)) == 0;
            int open = (openF ? 1 : 0) + (openB ? 1 : 0);

            total += weight(len, open);
        }
        return total;
    }

    private static long weight(int len, int open)
    {
        if (len >= 5) return 1_000_000;
        if (open == 0) return 0;              //blocked from 2 sides
        return len switch
        {
            4 => open == 2 ? 100_000 : 10_000,
            3 => open == 2 ? 5_000   : 500,
            2 => open == 2 ? 100     : 10,
            _ => open == 2 ? 2       : 1
        };
    }
    
    public Move MakeMove(Board board)
    {
        Move move = new Move();

        if (board.FreeCells == board.Height * board.Width)
        {
            move.Row = board.Height / 2 + 1;
            move.Col = board.Width / 2 + 1;
            return move;
        }
        
        int best = -1;
        var best_cells = new List<(int row, int col)>();

        for (int row = 1; row < board.Height; ++row)
        {
            for (int col = 1; col < board.Width; ++col)
            {
                if (board.Get(row, col) != 0) 
                    continue;
                int score = (int)(evaluateValue(board, row, col, Bot.Mark) + (int)(evaluateValue(board, row, col, Player.Mark) * 0.9));
                if (score > best)
                {
                    best = score;
                    best_cells.Clear();
                    best_cells.Add((row, col));
                }
                
            }
        }

        var pick = best_cells[_rng.Next(best_cells.Count)];

        move.Row = pick.row;
        move.Col = pick.col;
        
        return move;
    }
}
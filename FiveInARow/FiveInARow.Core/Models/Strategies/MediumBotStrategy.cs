namespace FiveInARow.Core.Models.Strategies;

public class MediumBotStrategy : BotStrategy
{
    public override Move MakeMove(Board board)
    {
        Move move = new Move();
        
        

        int f_row = 1, f_col = 1;
        
        

        for (int row = 1; row <= board.Height; ++row)
        {
            for (int col = 1; col <= board.Width; ++col)
            {
                int mark = board.Get(row, col);

                if (mark == 0)
                {
                    foreach (var (dr, dc) in dirs)
                    {
                        // if (row, col) wins
                        int fwd = cntDir(board, row, col, dr, dc, Bot.Mark);
                        int back = cntDir(board, row, col, -dr, -dc, Bot.Mark);
                        if (fwd + back == 4)
                        {
                            move.Row = row;
                            move.Col = col;
                            return move;
                        }
                        
                        // if (row, col) blocks the victory of player
                        fwd = cntDir(board, row, col, dr, dc, Player.Mark);
                        back = cntDir(board, row, col, -dr, -dc, Player.Mark);
                        if (fwd + back >= 3)
                        {
                            move.Row = row;
                            move.Col = col;
                            return move;
                        }
                        
                    }
                }
            }
        }



        return new EasyBotStrategy().MakeMove(board);
    }
}
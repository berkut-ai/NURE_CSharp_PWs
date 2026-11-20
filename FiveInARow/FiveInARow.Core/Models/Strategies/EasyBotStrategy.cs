namespace FiveInARow.Core.Models.Strategies;

public class EasyBotStrategy : BotStrategy
{
    public Move MakeMove(Board board)
    {
        Move move = new Move();

        int f_row = 1, f_col = 1;
        
        

        for (int row = 1; row < board.Height; ++row)
        {
            for (int col = 1; col <= board.Width; ++col)
            {
                int mark = board.Get(row, col);
                
                if (mark == 0)
                {
                    f_row = row;
                    f_col = col;
                } else if (mark == 2)
                {
                    foreach (var (dr, dc) in dirs)
                    {
                        if (board.checkIndexes(row + dr, col + dc)
                            && board.checkIndexes(row - dr, col - dc)){
                            move.Row = row + dr;
                            move.Col = col + dc;
                            return move;
                        }
                    }
                }
                
            }
        }

        move.Row = f_row;
        move.Col = f_col;
        return move;
    }
}
namespace FiveInARow.Core.Models.Strategies;

public abstract class BotStrategyUtils
{
    public static readonly (int dr, int dc)[] dirs = { (0, 1), (1, 0), (1, 1), (1, -1) };
    
    public static int cntDir(Board board, int r, int c, int dr, int dc, int mark)
    {
        int n = 0;
        r += dr; c += dc;
        while (board.checkIndexes(r, c) && board.Get(r, c) == mark)
        {
            n++; 
            r += dr; 
            c += dc;
        }
        return n;
    }
    
    
    
}
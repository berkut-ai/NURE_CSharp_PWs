namespace FiveInARow.Core.Models.Strategies;

public abstract class BotStrategy : IBotStrategy
{
    protected static readonly (int dr, int dc)[] dirs = { (0, 1), (1, 0), (1, 1), (1, -1) };
    public virtual Move MakeMove(Board board)
    {
        throw new NotImplementedException();
    }
    
    protected static int cntDir(Board board, int r, int c, int dr, int dc, int mark)
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
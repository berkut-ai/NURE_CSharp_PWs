using FiveInARow.Core.Models.Memento;

namespace FiveInARow.Core.Models;

public class Board : IBoard
{
    private class BoardMemento : IBoardMemento
    {
        private int[,] _board_state;
        private int _free_cells_state;
        public BoardMemento(int[,] board, int free_cells)
        {
            _board_state = (int[,])board.Clone();
            _free_cells_state = free_cells;
        }
        

        public (int[,], int) GetState()
        {
            return ((int[,])_board_state.Clone(), _free_cells_state);
        }
    }
    
    
    private const int _width = 15;
    private const int _height = _width;
    private int _free_cells = _width * _height;

    private int[,] _board; // [i, j] 0 == empty, 1 == player, 2 == bot or player2 

    public int Height => _height;

    public int Width => _width;

    public int FreeCells => _free_cells;


    public Board()
    {
        _board = new int[_height, _width];
    }

    public bool checkIndexes(int i, int j)
    {
        if (!(1 <= i && i <= _height) || !(1 <= j && j <= _width))
            return false;
        return true;
    }
    

    public int Get(int row, int col)
    {
        if (!checkIndexes(row, col))
            throw new IndexOutOfRangeException();

        return _board[row-1, col-1];
    }

    public void Set(Move move)
    {
        int row = move.Row, col = move.Col, val = move.Val;
        
        if (!checkIndexes(row, col))
            throw new IndexOutOfRangeException();

        row--;
        col--;

        if (!(0 <= val && val <= 2))
        {
            throw new ArgumentException("value shold be in [0;2]");
        }

        if (_board[row, col] != val)
        {
            
            if (val == 0)
                _free_cells++;
            else if (_board[row, col] == 0)
                _free_cells--;
            _board[row, col] = move.Val;
        }
    }

    private int cntDir(int[,] board, int r, int c, int dr, int dc, int mark)
    {
        int n = 0;
        r += dr; c += dc;
        while (checkIndexes(r+1, c+1) && board[r, c] == mark)
        {
            n++; 
            r += dr; 
            c += dc;
        }
        return n;
    }

    private bool checkIf5InARow(int[,] board, int row, int col, int mark)
    {
        var  dirs = new (int dr, int dc)[]{ (0, 1), (1, 0), (1, 1), (1, -1) };
        foreach (var (dr, dc) in dirs)
        {
            int fwd = cntDir(board, row, col, dr, dc, mark);
            int back = cntDir(board, row, col, -dr, -dc, mark);
            int len = 1 + fwd + back;

            if (len >= 5)
                return true;
        }
        return false;
    }
    
    public int CheckWinner()
    {
        for (int row = 0; row < _height; ++row)
        {
            for (int col = 0; col < _width; ++col)
            {
                int mark = _board[row, col];
                
                if (mark == 0)
                    continue;

                if (checkIf5InARow(_board, row, col, mark))
                {
                    return mark;
                }
                
            }
        }

        return 0;
    }

    public IBoardMemento Save()
    {
        return new BoardMemento(_board, _free_cells);
    }

    public void Restore(IBoardMemento? memento)
    {
        if (memento is not BoardMemento b_memento)
            return;
        (_board, _free_cells) = b_memento.GetState();
    }

    public void Clear()
    {
        _board = new int[_height, _width];
        _free_cells = _height * _width;
    }
}
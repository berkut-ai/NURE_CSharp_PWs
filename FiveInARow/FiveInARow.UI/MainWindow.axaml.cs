using Avalonia.Controls;
using FiveInARow.Core.Models;
using FiveInARow.Core.Models.Strategies;
using FiveInARow.Core.Controllers;

namespace FiveInARow.UI;

public partial class MainWindow : Window
{
    private readonly GameController _controller =
        new(new Board(), new EasyBotStrategy());
    public MainWindow()
    {
        InitializeComponent();
        
        BoardView.setBoard(_controller.Board);
        UpdateStatus();

        BoardView.cell_clicked += (r, c) =>
        {
            _controller.HandleCLick(r, c);
            Refresh();
        };

        BtnNewGame.Click += (_, _) =>
        {
            _controller.newGame();
            Refresh();
        };

        BtnUndo.Click += (_, _) =>
        {
            _controller.Undo();
            Refresh();
        };

        CbDifficulty.SelectionChanged += (_, _) =>
        {
            _controller.setDifficulty((Difficulty)CbDifficulty.SelectedIndex);
        };
    }
    
    private void Refresh()
    {
        BoardView.Redraw();
        UpdateStatus();
    }
    
    private void UpdateStatus()
    {
        LblStatus.Text = _controller.Result switch
        {
            GameResult.PlayerWon => "You Won!",
            GameResult.BotWon => "Bot Won",
            GameResult.Draw => "Draw",
            _ => "Your Turn"
        };
    }
}
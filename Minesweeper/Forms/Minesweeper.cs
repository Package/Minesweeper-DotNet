using Minesweeper.Core;
using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Minesweeper : Form
    {
        public Board _board { get; private set; }

        public GameMode _mode { get; private set; }

        public int BOARD_WIDTH { get; private set; }
        public int BOARD_HEIGHT { get; private set; }
        public int NUM_MINES { get; private set; }

        public Random _random { get; private set; }

        public Minesweeper()
        {
            InitializeComponent();
            DoubleBuffered = true;

            _random = new Random();
            _mode = GameMode.Intermediate;
            GetBoardSize();

            _board = new Board(this, BOARD_WIDTH, BOARD_HEIGHT, NUM_MINES);
            _board.SetupBoard();
            _board.PlaceMines();
        }

        /// <summary>
        /// Size of the board is determined by the GameMode.
        /// </summary>
        /// <returns></returns>
        private void GetBoardSize()
        {
            switch (_mode)
            {
                case GameMode.Beginner:
                    BOARD_WIDTH = 8;
                    BOARD_HEIGHT = 8;
                    NUM_MINES = 10;
                    break;
                case GameMode.Intermediate:
                    BOARD_WIDTH = 16;
                    BOARD_HEIGHT = 16;
                    NUM_MINES = 40;
                    break;
                case GameMode.Expert:
                    BOARD_WIDTH = 16;
                    BOARD_HEIGHT = 30;
                    NUM_MINES = 99;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Reveals the locations of the mines.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowMines_Click(object sender, EventArgs e)
        {
            _board.ShowMines = !_board.ShowMines;
            Invalidate();
        }

        /// <summary>
        /// Reveals the percentages of each cell being a mine.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowPercent_Click(object sender, EventArgs e)
        {
            _board.ShowPercentage = !_board.ShowPercentage;
            _board.SetMinePercentages();
            Invalidate();
        }

        /// <summary>
        /// Handles click events on the form for opening/flagging cells.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Minesweeper_Click(object sender, EventArgs e)
        {
            var mouseArgs = (MouseEventArgs)e;

            var clickedX = mouseArgs.X - Board.CellSize;
            var clickedY = mouseArgs.Y - Board.CellSize;

            var cellX = clickedX / Board.CellSize;
            var cellY = clickedY / Board.CellSize;

            // Check for out of bounds:
            if (clickedX < 0 || clickedY < 0 || cellX < 0 || cellY < 0 || cellX >= _board.Width || cellY >= _board.Height)
            {
                return;
            }

            Cell cell = _board.Cells[cellX, cellY];

            switch (mouseArgs.Button)
            {
                case MouseButtons.Left:
                    // Left click opens the cell:
                    cell.OnClick();
                    AfterClick();
                    break;
                case MouseButtons.Right:
                    // Right click places a flag:
                    if (cell.Closed)
                    {
                        cell.OnFlag();
                        AfterClick();
                    }
                    break;
                default: 
                    break;
            }
        }

        /// <summary>
        /// Updates the UI after the user has clicked
        /// </summary>
        private void AfterClick()
        {
            if (_board.ShowPercentage)
            {
                _board.SetMinePercentages();
            }

            _board.CheckForWin();
            Invalidate();
        }

        /// <summary>
        /// Paints the game board.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Minesweeper_Paint(object sender, PaintEventArgs e)
        {
            if (_board != null)
            {
                _board.Paint(e.Graphics);
            }
        }
    }
}

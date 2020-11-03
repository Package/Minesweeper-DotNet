using Minesweeper.Core;
using Minesweeper.Core.Boards;
using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Minesweeper : Form
    {
        public Board _board { get; private set; }
        public GameMode _mode { get; private set; }
        public Random _random { get; private set; }
        public int BOARD_WIDTH { get; private set; }
        public int BOARD_HEIGHT { get; private set; }
        public int NUM_MINES { get; private set; }

        public Minesweeper()
        {
            InitializeComponent();
            DoubleBuffered = true;

            _random = new Random();

            _mode = GameMode.Intermediate;
            StartGame();
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        private void StartGame()
        {
            GetBoardSize();

            _board = new Board(this, BOARD_WIDTH, BOARD_HEIGHT, NUM_MINES);
            _board.SetupBoard();
            _board.PlaceMines();

            // Dynamically resize the board to fit
            Width = (BOARD_WIDTH * Board.CellSize) + (int)(Board.CellSize * 1.5);
            Height = (BOARD_HEIGHT * Board.CellSize) + Board.CellSize * 4;

            UpdateMinesRemaining();

            showMinesToolStripMenuItem.Checked = false;
            showCellLocationsToolStripMenuItem.Checked = false;
            showPercentagesToolStripMenuItem.Checked = false;
        }

        /// <summary>
        /// Updates the number of mines remaining on the UI.
        /// </summary>
        /// <param name="numberOfMines"></param>
        public void UpdateMinesRemaining()
        {
            lblMinesLeft.Text = $"Mines Left: {_board.NumMinesRemaining}";
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
                    BOARD_WIDTH = 30;
                    BOARD_HEIGHT = 16;
                    NUM_MINES = 99;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Returns the cell that was clicked on. Or null if the click was outside of the bounds of the 
        /// game board.
        /// </summary>
        /// <param name="mouseArgs"></param>
        /// <returns></returns>
        private Cell GetCellFromMouseEvent(MouseEventArgs mouseArgs)
        {
            // Game is in an over state - do not register clicks on the board.
            if (_board != null && _board.GameOver)
            {
                return null;
            }

            var clickedX = mouseArgs.X - (Board.CellSize / 2);
            var clickedY = mouseArgs.Y - (Board.CellSize * 2);

            var cellX = clickedX / Board.CellSize;
            var cellY = clickedY / Board.CellSize;

            // Check for out of bounds:
            if (clickedX < 0 || clickedY < 0 || cellX < 0 || cellY < 0 || cellX >= _board.Width || cellY >= _board.Height)
            {
                return null;
            }

            return _board.Cells[cellX, cellY];
        }

        /// <summary>
        /// Handles click events on the form for opening/flagging cells.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Minesweeper_Click(object sender, EventArgs e)
        {
            var mouseArgs = (MouseEventArgs)e;

            var cell = GetCellFromMouseEvent(mouseArgs);
            if (cell == null)
                return;

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
            _board.UpdateCells();
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
                _board.Painter?.Paint(e.Graphics);
            }
        }

        /// <summary>
        /// Handles the user wanting to quit the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var response = MessageBox.Show("Do you really want to quit the game?", "Quit?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (response == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Reveals (or hides) the locations of the mines.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showMinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _board.ShowMines = !_board.ShowMines;
            showMinesToolStripMenuItem.Checked = _board.ShowMines;

            Invalidate();
        }

        /// <summary>
        /// Reveals (or hides) the percentages of each cell being a mine.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showPercentagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _board.ShowPercentage = !_board.ShowPercentage;
            _board.UpdateCells();
            showPercentagesToolStripMenuItem.Checked = _board.ShowPercentage;

            Invalidate();
        }

        /// <summary>
        /// Reveals (or hides) the cell location co-ordinates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showCellLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _board.ShowLocation = !_board.ShowLocation;
            showCellLocationsToolStripMenuItem.Checked = _board.ShowLocation;

            Invalidate();
        }

        /// <summary>
        /// Starts a new beginner game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmNewGame(GameMode.Beginner);
        }

        /// <summary>
        /// Starts a new intermediate game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmNewGame(GameMode.Intermediate);
        }

        /// <summary>
        /// Starts a new expert game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmNewGame(GameMode.Expert);
        }

        /// <summary>
        /// Confirms that the user wants to start a new game.
        /// </summary>
        /// <returns></returns>
        private void ConfirmNewGame(GameMode mode)
        {
            var response = MessageBox.Show("Do you really want to start a new game?\nYou will lose any progress in your current game.", "Start New Game", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (response == DialogResult.Yes)
            {
                _mode = mode;
                StartGame();
            }
        }

        /// <summary>
        /// Event handler for when the mouse is moved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Minesweeper_MouseMove(object sender, MouseEventArgs e)
        {
            var cell = GetCellFromMouseEvent(e);
            _board.Painter.HoveredCell = cell;
            Cursor = cell != null && cell.Closed ? Cursors.Hand : Cursors.Default;

            if (cell != null)
            {
                _board.UpdateCells();
            }

            Invalidate();
        }
    }
}

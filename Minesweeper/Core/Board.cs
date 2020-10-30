using System;
using System.Linq;
using System.Windows.Forms;

namespace Minesweeper.Core
{
    public class Board
    {
        public Minesweeper Minesweeper { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NumMines { get; set; }
        public Cell[,] Cells { get; set; }
        public bool ShowMines { get; set; }

        public Board(Minesweeper minesweeper, int width, int height, int mines)
        {
            this.Minesweeper = minesweeper;
            this.Width = width;
            this.Height = height;
            this.NumMines = mines;
            this.Cells = new Cell[width, height];
        }

        /// <summary>
        /// Setup the cells on the board.
        /// </summary>
        public void SetupBoard()
        {
            for (var x = 1; x <= Width; x++)
            {
                for (var y = 1; y <= Height; y++)
                {
                    var c = new Cell
                    {
                        XLoc = x - 1,
                        YLoc = y - 1,
                        CellState = CellState.Closed,
                        CellType = CellType.Regular,
                        CellSize = 35,
                        MinePercentage = -1,
                        Board = this
                    };
                    c.SetupDesign();
                    c.MouseDown += Cell_MouseClick;
                    
                    
                    this.Cells[x-1, y-1] = c;
                    this.Minesweeper.Controls.Add(c);
                }
            }
        }

        /// <summary>
        /// Randomly distribute the mines across the game board.
        /// </summary>
        public void PlaceMines()
        {
            var minesPlaced = 0;
            var random = new Random();

            while (minesPlaced < this.NumMines)
            {
                int x = random.Next(0, this.Width);
                int y = random.Next(0, this.Height);

                if (!this.Cells[x, y].IsMine())
                {
                    this.Cells[x, y].CellType = CellType.Mine;
                    minesPlaced += 1;
                }
            }

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var c = this.Cells[x, y];
                    c.UpdateDisplay();
                    c.NumMines = c.GetNeighborCells().Where(n => n.IsMine()).Count();
                }
            }
        }

        private void Cell_MouseClick(object sender, MouseEventArgs e)
        {
            var cell = (Cell) sender;

            switch (e.Button)
            {
                // Left mouse button opens the cell
                case MouseButtons.Left:
                    cell.OnClick();
                    break;

                // Right mouse button flags the cell
                case MouseButtons.Right:
                    if (cell.CellState == CellState.Closed)
                    {
                        cell.OnFlag();
                    }
                    break;

                default:
                    break;
            }

            SetMinePercentages();
            CheckForWin();
        }

        private void CheckForWin()
        {
            var correctMines = 0;
            var incorrectMines = 0;

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var c = this.Cells[x, y];
                    if (c.CellType == CellType.Flagged)
                        incorrectMines += 1;
                    if (c.CellType == CellType.FlaggedMine)
                        correctMines += 1;
                }
            }

            // Correctly identified all mines
            if (correctMines == NumMines && incorrectMines == 0)
            {
                MessageBox.Show("Congratulations! You won.");
                RestartGame();
            }
        }

        public void RestartGame()
        {
            // Remove all cells from the board.
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var c = this.Cells[x, y];
                    this.Minesweeper.Controls.Remove(c);
                }
            }

            // Start the new game
            this.SetupBoard();
            this.PlaceMines();
        }

        public void SetMinePercentages(bool reset = false)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var cc = this.Cells[x, y];
                    cc.MinePercentage = reset ? -1 : cc.CalculateMinePercentage();
                    cc.UpdateDisplay();
                }
            }
        }
    }
}

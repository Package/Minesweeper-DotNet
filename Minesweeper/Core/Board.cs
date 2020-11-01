using System;
using System.Collections.Generic;
using System.Drawing;
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
        public bool ShowPercentage { get; set; }
        public bool ShowLocation { get; set; }
        public bool GameOver { get; set; }

        public const int CellSize = 32;
        public Dictionary<int, SolidBrush> _cellColours { get; set; }

        private readonly Font _textFont = new Font(FontFamily.GenericSansSerif, 16f);
        private readonly Font _percentFont = new Font(FontFamily.GenericSansSerif, 7f);
        private readonly Font _locationFont = new Font(FontFamily.GenericSansSerif, 6f);

        public Board(Minesweeper minesweeper, int width, int height, int mines)
        {
            Minesweeper = minesweeper;
            Width = width;
            Height = height;
            NumMines = mines;
            Cells = new Cell[width, height];
        }

        /// <summary>
        /// Setup the cells on the board.
        /// </summary>
        public void SetupBoard()
        {
            if (_cellColours == null)
            {
                _cellColours = new Dictionary<int, SolidBrush> {
                    { 0, new SolidBrush(ColorTranslator.FromHtml("0xffffff")) },
                    { 1, new SolidBrush(ColorTranslator.FromHtml("0x0000FE")) },
                    { 2, new SolidBrush(ColorTranslator.FromHtml("0x186900")) },
                    { 3, new SolidBrush(ColorTranslator.FromHtml("0xAE0107")) },
                    { 4, new SolidBrush(ColorTranslator.FromHtml("0x000177")) },
                    { 5, new SolidBrush(ColorTranslator.FromHtml("0x8D0107")) },
                    { 6, new SolidBrush(ColorTranslator.FromHtml("0x007A7C")) },
                    { 7, new SolidBrush(ColorTranslator.FromHtml("0x902E90")) },
                    { 8, new SolidBrush(ColorTranslator.FromHtml("0x000000")) }
                };
            }

            for (var x = 1; x <= Width; x++)
            {
                for (var y = 1; y <= Height; y++)
                {
                    Cells[x - 1, y - 1] = new Cell(x - 1, y - 1, this);
                }
            }

            GameOver = false;
        }

        /// <summary>
        /// Randomly distribute the mines across the game board.
        /// </summary>
        public void PlaceMines()
        {
            var minesPlaced = 0;
            var random = new Random();

            while (minesPlaced < NumMines)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);

                if (!Cells[x, y].IsMine)
                {
                    Cells[x, y].CellType = CellType.Mine;
                    minesPlaced += 1;
                }
            }

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var c = Cells[x, y];
                    c.NumMines = c.GetNeighborCells().Where(n => n.IsMine).Count();
                }
            }

            Minesweeper.Invalidate();
        }

        /// <summary>
        /// User opened a mine and lost. Reveal the locations of the remaining mines
        /// and then restart the game.
        /// </summary>
        public void RevealMines()
        {
            // Reveal where the mines where
            GameOver = true;
            Minesweeper.Invalidate();

            // Ask to play again
            HandleGameOver(gameWon: false);
        }

        /// <summary>
        /// Offer the user the option to restart the game.
        /// </summary>
        /// <param name="gameWon"></param>
        private void HandleGameOver(bool gameWon)
        {
            var message = gameWon ? "Congratulations... You won!" : "Unlucky... you opened a mine!";
            message += "\nWould you like to play again?";

            var response = MessageBox.Show(message, "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (response == DialogResult.Yes)
            {
                // Restart the game
                SetupBoard();
                PlaceMines();
            }
        }

        /// <summary>
        /// Determines whether the game has been won.
        /// This is when the user has correctly identified all the mines on the board.
        /// </summary>
        public void CheckForWin()
        {
            var correctMines = 0;
            var incorrectMines = 0;

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var c = Cells[x, y];
                    if (c.CellType == CellType.Flagged)
                    {
                        incorrectMines += 1;
                    }
                    if (c.CellType == CellType.FlaggedMine)
                    {
                        correctMines += 1;
                    }
                }
            }

            if (correctMines == NumMines && incorrectMines == 0)
            {
                HandleGameOver(gameWon: true);
            }
        }

        /// <summary>
        /// Calculates the percentage of each cell on the board being a mine.
        /// </summary>
        public void SetMinePercentages()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cells[x, y].CalculateMinePercentage();
                }
            }
        }

        /// <summary>
        /// Paints the game board.
        /// </summary>
        /// <param name="graphics"></param>
        public void Paint(Graphics graphics)
        {
            graphics.Clear(Color.White);
            graphics.TranslateTransform(CellSize, CellSize);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var cell = Cells[x, y];
                    DrawInsideCell(cell, graphics);
                    graphics.DrawRectangle(Pens.Gray, cell.Bounds);
                }
            }
        }

        /// <summary>
        /// Renders one cell on the game board.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="graphics"></param>
        private void DrawInsideCell(Cell cell, Graphics graphics)
        {
            // Closed Cell
            if (cell.Closed)
            {
                graphics.FillRectangle(Brushes.DarkGray, cell.Bounds);
                if (cell.MinePercentage != -1 && ShowPercentage)
                {
                    graphics.DrawString($"{cell.MinePercentage:#.##}%", _percentFont, Brushes.DarkRed, cell.TopLeftPos);
                }
            }

            // Opened Cell
            if (cell.Opened)
            {
                graphics.FillRectangle(Brushes.LightGray, cell.Bounds);
                if (cell.NumMines > 0)
                {
                    graphics.DrawString(cell.NumMines.ToString(), _textFont, GetCellColour(cell), cell.CenterPos);
                }
            }

            // Flagged Cell
            if (cell.Flagged)
            {
                graphics.DrawString("?", _textFont, Brushes.Black, cell.CenterPos);
            }

            // Mine Cell
            if (cell.IsMine && (ShowMines || GameOver))
            {
                // This cell was the one that ended the game
                if (cell.Opened)
                {
                    graphics.FillRectangle(Brushes.Red, cell.Bounds);
                }

                graphics.DrawString("M", _textFont, Brushes.DarkRed, cell.CenterPos);
            }

            // Draw (x,y) location
            if (ShowLocation)
            {
                graphics.DrawString($"{cell.XLoc},{cell.YLoc}", _locationFont, Brushes.Black, cell.BottomLeftPos);
            }
        }

        /// <summary>
        /// Return the colour code associated with the number of surrounding mines
        /// </summary>
        /// <returns></returns>
        private SolidBrush GetCellColour(Cell cell)
        {
            return _cellColours[cell.NumMines];
        }
    }
}

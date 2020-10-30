using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Minesweeper.Core
{
    public enum CellType
    {
        Regular, Mine, Flagged, FlaggedMine
    }

    public enum CellState
    {
        Opened, Closed
    }

    public class Cell : Button
    {
        private readonly Font _defaultFont = new Font("Verdana", 14f, FontStyle.Bold);
        private readonly Font _smallFont = new Font("Arial", 8f, FontStyle.Bold);

        public int XLoc { get; set; }
        public int YLoc { get; set; }
        public int CellSize { get; set; }
        public CellState CellState { get; set; }
        public CellType CellType { get; set; }
        public int NumMines { get; set; }
        public Board Board { get; set; }
        public double MinePercentage { get; set; }

        /// <summary>
        /// Setup the look and feel for the buttons.
        /// Should be called just once after initializing the cells.
        /// </summary>
        public void SetupDesign()
        {
            this.Location = new Point((XLoc * CellSize) + CellSize, (YLoc * CellSize) + CellSize);
            this.Size = new Size(CellSize, CellSize);
            this.UseVisualStyleBackColor = false;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.Font = _defaultFont;
            
            // Borders
            this.BackColor = Color.Silver;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.Gray;
            this.FlatAppearance.BorderSize = 1;
        }

        /// <summary>
        /// Return whether the type of this cell is a Mine.
        /// </summary>
        /// <returns></returns>
        public bool IsMine()
        {
            return this.CellType == CellType.Mine ||
                this.CellType == CellType.FlaggedMine;
        }

        /// <summary>
        /// Responds to user click event to flag this cell.
        /// </summary>
        public void OnFlag()
        {
            switch (this.CellType)
            {
                case CellType.Regular:
                    this.CellType = CellType.Flagged;
                    break;
                case CellType.Mine:
                    this.CellType = CellType.FlaggedMine;
                    break;
                case CellType.Flagged:
                    this.CellType = CellType.Regular;
                    break;
                case CellType.FlaggedMine:
                    this.CellType = CellType.Mine;
                    break;
                default:
                    throw new Exception($"Unknown cell type {this.CellType}");
            }

            this.UpdateDisplay();
        }

        /// <summary>
        /// Responds to user click event to open this cell.
        /// </summary>
        public void OnClick(bool recursiveCall = false)
        {
            // Recursive cell opening stops when it gets to a non-regular cell or a cell that's already open.
            if (recursiveCall)
            {
                if (this.CellType != CellType.Regular || this.CellState != CellState.Closed)
                    return;
            }

            // Cell was a mine
            if (this.CellType == CellType.Mine)
            {
                this.CellState = CellState.Opened;
                this.UpdateDisplay();

                // For each cell, reveal whether it was a bomb or not
                for (var x = 0; x < this.Board.Width; x++)
                {
                    for (var y = 0; y < this.Board.Height; y++)
                    {
                        this.Board.Cells[x, y].CellState = CellState.Opened;
                        this.Board.Cells[x, y].UpdateDisplay();
                    }
                }

                MessageBox.Show("You have opened a mine! Good game.");

                Board.RestartGame();
                return;
            }

            // Regular cell
            if (this.CellType == CellType.Regular)
            {
                this.CellState = CellState.Opened;
                this.UpdateDisplay();
            }

            // Recursively open surrounding cells.
            if (this.NumMines == 0)
            {
                var neighbors = this.GetNeighborCells();
                foreach (var n in neighbors)
                    n.OnClick(true);
            }
        }

        /// <summary>
        /// Get a list of cells that are directly neighboring a provided cell.
        /// </summary>
        /// <returns></returns>
        public List<Cell> GetNeighborCells()
        {
            var neighbors = new List<Cell>();

            for (var x = -1; x < 2; x++)
            {
                for (var y = -1; y < 2; y++)
                {
                    // Can't be your own neighbor!
                    if (x == 0 && y == 0)
                        continue;

                    // Cell would be out of bounds
                    if (XLoc + x < 0 || XLoc + x >= Board.Width || YLoc + y < 0 || YLoc + y >= Board.Height)
                        continue;

                    neighbors.Add(Board.Cells[XLoc + x, YLoc + y]);
                }
            }

            return neighbors;
        }

        /// <summary>
        /// Updates the display of the cell.
        /// </summary>
        public void UpdateDisplay()
        {
            this.Font = _defaultFont;
            this.TextAlign = ContentAlignment.MiddleCenter;

            // Cell is flagged
            if (this.CellType == CellType.Flagged ||
                this.CellType == CellType.FlaggedMine)
            {
                this.ForeColor = Color.Black;
                this.Text = "?";
                return;
            }

            // Cell is closed
            if (this.CellState == CellState.Closed)
            {
                this.BackColor = Color.DarkGray;
                this.Text = string.Empty;

                if (this.MinePercentage != -1)
                {
                    this.ForeColor = Color.Red;
                    this.Font = _smallFont;
                    this.Text = $"{this.MinePercentage}%";
                    this.TextAlign = ContentAlignment.TopLeft;
                }

            }

            // Open mine
            if (this.CellType == CellType.Mine && (this.CellState == CellState.Opened || Board.ShowMines))
            {
                this.BackColor = Color.DarkGray;
                this.ForeColor = Color.DarkRed;
                this.Text = "M";
            }

            // Open regular cell (show number of mines around it)
            if (this.CellType == CellType.Regular && this.CellState == CellState.Opened)
            {
                this.BackColor = Color.LightGray;
                this.ForeColor = this.GetCellColour();
                this.Text = this.NumMines > 0 ? $"{this.NumMines}" : string.Empty;
            }
        }

        /// <summary>
        /// Return the colour code associated with the number of surrounding mines
        /// </summary>
        /// <returns></returns>
        private Color GetCellColour()
        {
            switch (this.NumMines)
            {
                case 1:
                    return ColorTranslator.FromHtml("0x0000FE"); // 1
                case 2:
                    return ColorTranslator.FromHtml("0x186900"); // 2
                case 3:
                    return ColorTranslator.FromHtml("0xAE0107"); // 3
                case 4:
                    return ColorTranslator.FromHtml("0x000177"); // 4
                case 5:
                    return ColorTranslator.FromHtml("0x8D0107"); // 5
                case 6:
                    return ColorTranslator.FromHtml("0x007A7C"); // 6
                case 7:
                    return ColorTranslator.FromHtml("0x902E90"); // 7
                case 8:
                    return ColorTranslator.FromHtml("0x000000"); // 8
                default:
                    return ColorTranslator.FromHtml("0xffffff");
            }
        }

        /// <summary>
        /// Work out the percentage % of this cell being a mine.
        /// </summary>
        /// <returns></returns>
        public double CalculateMinePercentage()
        {
            double pct = 0d;
            int checkedCells = 0;

            foreach (var nc in GetNeighborCells())
            {
                int surroundingMines = nc.NumMines;

                if (surroundingMines < 1)
                    continue;

                if (nc.CellState == CellState.Closed)
                    continue;

                int availCells = nc.GetNeighborCells().Where(ncc =>
                    ncc.CellState == CellState.Closed &&
                    ncc.CellType != CellType.Flagged &&
                    ncc.CellType != CellType.FlaggedMine).ToList().Count;

                int flaggedCells = nc.GetNeighborCells().Where(ncc =>
                    ncc.CellType == CellType.Flagged || ncc.CellType == CellType.FlaggedMine).ToList().Count;

                // 0% chance of being a mine
                if (flaggedCells == surroundingMines)
                    return 0;

                // 100% of being a mine
                if (surroundingMines == (availCells + flaggedCells))
                    return 100;

                checkedCells += 1;
                pct += (surroundingMines * 1.0 / availCells) * 100;
            }

            // Unable to determine - did not consider any cells.
            if (checkedCells == 0)
            {
                return -1;
            }

            return Math.Round(pct / (checkedCells > 0 ? checkedCells : 1));
        }
    }
}

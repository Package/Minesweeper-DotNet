using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Minesweeper.Core
{
    public class Cell
    {
        public int XLoc { get; set; }
        public int YLoc { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public Point CenterPos { get; set; }
        public Point TopLeftPos { get; set; }
        public Point BottomLeftPos { get; set; }
        public int CellSize { get; set; }
        public CellState CellState { get; set; }
        public CellType CellType { get; set; }
        public int NumMines { get; set; }
        public Board Board { get; set; }
        public double MinePercentage { get; set; }
        public Rectangle Bounds { get; private set; }
        private List<Cell> Surrounding { get; set; }
        public bool Flagged => CellType == CellType.Flagged || CellType == CellType.FlaggedMine;

        public bool Closed => CellState == CellState.Closed;

        public bool Opened => CellState == CellState.Opened;

        /// <summary>
        /// Return whether the type of this cell is a Mine.
        /// </summary>
        /// <returns></returns>
        public bool IsMine => CellType == CellType.Mine || CellType == CellType.FlaggedMine;

        /// <summary>
        /// Constructs a new <see cref="Cell"/>
        /// </summary>
        public Cell(int x, int y, Board board)
        {
            XLoc = x;
            YLoc = y;
            CellSize = Board.CellSize;
            CellState = CellState.Closed;
            CellType = CellType.Regular;
            MinePercentage = -1;
            Bounds = new Rectangle(XLoc * CellSize, YLoc * CellSize, CellSize, CellSize);
            Board = board;
            XPos = XLoc * CellSize;
            YPos = YLoc * CellSize;
            CenterPos = new Point(XPos + (CellSize / 2 - 10), YPos + (CellSize / 2 - 10));
            TopLeftPos = new Point(XPos, YPos);
            BottomLeftPos = new Point(XPos, YPos + (CellSize - 10));
        }

        /// <summary>
        /// Responds to user click event to flag this cell.
        /// </summary>
        public void OnFlag()
        {
            switch (CellType)
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

            Board.Minesweeper.Invalidate();
        }

        /// <summary>
        /// Responds to user click event to open this cell.
        /// </summary>
        public void OnClick(bool recursiveCall = false)
        {
            // Recursive cell opening stops when it gets to a non-regular cell or a cell that's already open.
            if (recursiveCall)
            {
                if (CellType != CellType.Regular || CellState != CellState.Closed)
                    return;
            }

            // Cell was a mine
            if (CellType == CellType.Mine)
            {
                CellState = CellState.Opened;
                Board.RevealMines();
                return;
            }

            // Regular cell
            if (CellType == CellType.Regular)
            {
                CellState = CellState.Opened;
            }

            // Recursively open surrounding cells.
            if (NumMines == 0)
            {
                foreach (var n in GetNeighborCells())
                {
                    n.OnClick(true);
                }
            }
        }

        /// <summary>
        /// Get a list of cells that are directly neighboring a provided cell.
        /// </summary>
        /// <returns></returns>
        public List<Cell> GetNeighborCells()
        {
            if (Surrounding == null)
            {
                Surrounding = new List<Cell>();

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

                        Surrounding.Add(Board.Cells[XLoc + x, YLoc + y]);
                    }
                }
            }

            return Surrounding;
        }

        /// <summary>
        /// Work out the percentage % of this cell being a mine.
        /// </summary>
        /// <returns></returns>
        public void CalculateMinePercentage()
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
                {
                    MinePercentage = 0;
                    return;
                }

                // 100% of being a mine
                if (surroundingMines == (availCells + flaggedCells))
                {
                    MinePercentage = 100;
                    return;
                }

                checkedCells += 1;
                pct += (surroundingMines * 1.0 / availCells) * 100;
            }

            // Unable to determine - did not consider any cells.
            if (checkedCells == 0)
            {
                MinePercentage = -1;
                return;
            }

            MinePercentage = Math.Round(pct / (checkedCells > 0 ? checkedCells : 1));
        }
    }
}

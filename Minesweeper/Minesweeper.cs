using Minesweeper.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public enum GameMode
    {
        Beginner, Intermediate, Expert
    }

    public partial class Minesweeper : Form
    {
        public Board _board { get; set; }

        public GameMode _mode { get; set; }

        public int BOARD_WIDTH { get; set; }
        public int BOARD_HEIGHT { get; set; }
        public int NUM_MINES { get; set; }

        public Random _random { get; set; }

        public Minesweeper()
        {
            InitializeComponent();

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

        public void RunDebug(Cell clickedCell)
        {
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                for (int y = 0; y < BOARD_HEIGHT; y++)
                {
                    var cc = this._board.Cells[x, y];
                    cc.FlatStyle = FlatStyle.Standard;
                    cc.FlatAppearance.BorderColor = Color.Black;
                    cc.FlatAppearance.BorderSize = 1;
                }
            }

            Cell c = this._board.Cells[clickedCell.XLoc, clickedCell.YLoc];

            // Highlight neighbours
            //foreach (var nc in c.GetNeighborCells())
            //{
            //    nc.FlatStyle = FlatStyle.Flat;
            //    nc.FlatAppearance.BorderColor = Color.Aqua;
            //    nc.FlatAppearance.BorderSize = 1;
            //    nc.UpdateDisplay();
            //}

            clickedCell.OnClick();
        }

        /// <summary>
        /// Loops through all the cells in the game board and determines what to do with them,
        /// e.g. open them, or flag them etc.
        /// </summary>
        private void LoopCells()
        {
            bool cellProcessed = false;

            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                for (int y = 0; y < BOARD_HEIGHT; y++)
                {
                    Cell c = _board.Cells[x, y];
                    int surroundingBombs = c.NumMines;

                    // Skip any cells that have already been opened.
                    if (surroundingBombs < 1)
                        continue;

                    int flaggedCells = c.GetNeighborCells().Where(n => n.CellType == CellType.Flagged || n.CellType == CellType.FlaggedMine).ToList().Count;
                    List<Cell> availableCells = c.GetNeighborCells()
                        .Where(n =>
                        n.CellState == CellState.Closed &&
                        n.CellType != CellType.Flagged &&
                        n.CellType != CellType.FlaggedMine
                    ).ToList();

                    if (surroundingBombs == flaggedCells)
                    {
                        // Correct number of mines have already been flagged so we can open this.
                        if (c.CellState == CellState.Closed)
                        {
                            c.OnClick(true);
                            Task.Delay(500).Wait();
                            cellProcessed = true;
                        }

                        foreach (var cell in availableCells)
                        {
                            if (cell.CellState == CellState.Closed)
                            {
                                cell.OnClick();
                                Task.Delay(500).Wait();
                                cellProcessed = true;
                            }
                        }
                    }

                    // This must be a mine, so flag it.
                    if (availableCells.Count + flaggedCells == surroundingBombs)
                    {
                        foreach (var cell in availableCells)
                        {
                            cell.OnFlag();
                            Task.Delay(500).Wait();
                            cellProcessed = true;
                        }
                    }
                }
            }

            // Got here without processing a cell, probably stuck in a deadlock situation.
            // Open a random remaining closed cell.
            if (!cellProcessed)
            {
                while (true)
                {
                    var x = _random.Next(_board.Width);
                    var y = _random.Next(_board.Height);
                    if (_board.Cells[x, y].CellState == CellState.Closed)
                    {
                        Task.Delay(500).Wait();
                        _board.Cells[x, y].OnClick();
                        break;
                    }
                }
            }
        }

        private void btnShowMines_Click(object sender, EventArgs e)
        {
            _board.ShowMines = !_board.ShowMines;

            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                for (int y = 0; y < BOARD_HEIGHT; y++)
                {
                    var cc = this._board.Cells[x, y];
                    cc.UpdateDisplay();
                }
            }
        }

        private void btnShowPercent_Click(object sender, EventArgs e)
        {
            this._board.SetMinePercentages();
        }
    }
}

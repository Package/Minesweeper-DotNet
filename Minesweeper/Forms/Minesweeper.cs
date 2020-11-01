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


        /// <summary>
        /// Reveals the locations of the mines.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Reveals the percentages of each cell being a mine.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowPercent_Click(object sender, EventArgs e)
        {
            this._board.SetMinePercentages();
        }
    }
}

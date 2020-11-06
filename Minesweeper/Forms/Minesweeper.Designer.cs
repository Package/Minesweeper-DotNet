﻿namespace Minesweeper
{
    partial class Minesweeper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intermediateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hintsAndTipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHintsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPercentagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCellLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMinesLeft = new System.Windows.Forms.Label();
            this.showCellHighlightsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.newGameToolStripMenuItem,
            this.hintsAndTipsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beginnerToolStripMenuItem,
            this.intermediateToolStripMenuItem,
            this.expertToolStripMenuItem});
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.newGameToolStripMenuItem.Text = "New Game";
            // 
            // beginnerToolStripMenuItem
            // 
            this.beginnerToolStripMenuItem.Name = "beginnerToolStripMenuItem";
            this.beginnerToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.beginnerToolStripMenuItem.Text = "Beginner (8x8 - 10 mines)";
            this.beginnerToolStripMenuItem.Click += new System.EventHandler(this.beginnerToolStripMenuItem_Click);
            // 
            // intermediateToolStripMenuItem
            // 
            this.intermediateToolStripMenuItem.Name = "intermediateToolStripMenuItem";
            this.intermediateToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.intermediateToolStripMenuItem.Text = "Intermediate (16x16 - 40 mines)";
            this.intermediateToolStripMenuItem.Click += new System.EventHandler(this.intermediateToolStripMenuItem_Click);
            // 
            // expertToolStripMenuItem
            // 
            this.expertToolStripMenuItem.Name = "expertToolStripMenuItem";
            this.expertToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.expertToolStripMenuItem.Text = "Expert (30x16 - 99 mines)";
            this.expertToolStripMenuItem.Click += new System.EventHandler(this.expertToolStripMenuItem_Click);
            // 
            // hintsAndTipsToolStripMenuItem
            // 
            this.hintsAndTipsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMinesToolStripMenuItem,
            this.showHintsToolStripMenuItem,
            this.showPercentagesToolStripMenuItem,
            this.showCellLocationsToolStripMenuItem,
            this.showCellHighlightsToolStripMenuItem});
            this.hintsAndTipsToolStripMenuItem.Name = "hintsAndTipsToolStripMenuItem";
            this.hintsAndTipsToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.hintsAndTipsToolStripMenuItem.Text = "Hints and Tips";
            // 
            // showMinesToolStripMenuItem
            // 
            this.showMinesToolStripMenuItem.Name = "showMinesToolStripMenuItem";
            this.showMinesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showMinesToolStripMenuItem.Text = "Show Mines";
            this.showMinesToolStripMenuItem.Click += new System.EventHandler(this.showMinesToolStripMenuItem_Click);
            // 
            // showHintsToolStripMenuItem
            // 
            this.showHintsToolStripMenuItem.Name = "showHintsToolStripMenuItem";
            this.showHintsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showHintsToolStripMenuItem.Text = "Show Hints";
            this.showHintsToolStripMenuItem.Click += new System.EventHandler(this.showHintsToolStripMenuItem_Click);
            // 
            // showPercentagesToolStripMenuItem
            // 
            this.showPercentagesToolStripMenuItem.Name = "showPercentagesToolStripMenuItem";
            this.showPercentagesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showPercentagesToolStripMenuItem.Text = "Show Percentages %";
            this.showPercentagesToolStripMenuItem.Click += new System.EventHandler(this.showPercentagesToolStripMenuItem_Click);
            // 
            // showCellLocationsToolStripMenuItem
            // 
            this.showCellLocationsToolStripMenuItem.Name = "showCellLocationsToolStripMenuItem";
            this.showCellLocationsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showCellLocationsToolStripMenuItem.Text = "Show Cell Locations";
            this.showCellLocationsToolStripMenuItem.Click += new System.EventHandler(this.showCellLocationsToolStripMenuItem_Click);
            // 
            // lblMinesLeft
            // 
            this.lblMinesLeft.AutoSize = true;
            this.lblMinesLeft.Location = new System.Drawing.Point(16, 38);
            this.lblMinesLeft.Name = "lblMinesLeft";
            this.lblMinesLeft.Size = new System.Drawing.Size(59, 13);
            this.lblMinesLeft.TabIndex = 3;
            this.lblMinesLeft.Text = "Mines Left:";
            // 
            // showCellHighlightsToolStripMenuItem
            // 
            this.showCellHighlightsToolStripMenuItem.Name = "showCellHighlightsToolStripMenuItem";
            this.showCellHighlightsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.showCellHighlightsToolStripMenuItem.Text = "Highlight Surrounding Cells";
            this.showCellHighlightsToolStripMenuItem.Click += new System.EventHandler(this.showCellHighlightsToolStripMenuItem_Click);
            // 
            // Minesweeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 620);
            this.Controls.Add(this.lblMinesLeft);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Minesweeper";
            this.Text = "Minesweeper";
            this.Click += new System.EventHandler(this.Minesweeper_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Minesweeper_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Minesweeper_MouseMove);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hintsAndTipsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPercentagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCellLocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beginnerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intermediateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expertToolStripMenuItem;
        private System.Windows.Forms.Label lblMinesLeft;
        private System.Windows.Forms.ToolStripMenuItem showHintsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCellHighlightsToolStripMenuItem;
    }
}


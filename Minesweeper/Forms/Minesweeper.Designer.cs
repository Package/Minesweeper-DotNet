namespace Minesweeper
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
            this.btnSolve = new System.Windows.Forms.Button();
            this.btnShowPercent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSolve
            // 
            this.btnSolve.Location = new System.Drawing.Point(705, 585);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(75, 23);
            this.btnSolve.TabIndex = 0;
            this.btnSolve.Text = "Show Mines";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnShowMines_Click);
            // 
            // btnShowPercent
            // 
            this.btnShowPercent.Location = new System.Drawing.Point(705, 556);
            this.btnShowPercent.Name = "btnShowPercent";
            this.btnShowPercent.Size = new System.Drawing.Size(75, 23);
            this.btnShowPercent.TabIndex = 1;
            this.btnShowPercent.Text = "Show %";
            this.btnShowPercent.UseVisualStyleBackColor = true;
            this.btnShowPercent.Click += new System.EventHandler(this.btnShowPercent_Click);
            // 
            // Minesweeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 620);
            this.Controls.Add(this.btnShowPercent);
            this.Controls.Add(this.btnSolve);
            this.Name = "Minesweeper";
            this.Text = "Minesweeper";
            this.Click += new System.EventHandler(this.Minesweeper_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Minesweeper_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Button btnShowPercent;
    }
}


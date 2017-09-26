using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinMaxAlpha
{
    public partial class Alpha : Form
    {
        GameBoard gameb;


        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private static double totalTime = 0.0;

        public Alpha()
        {
            InitializeComponent();
            this.button1.Click += delegate { this.button_click(new Space(0, 0), new System.EventArgs()); };
            this.button2.Click += delegate { this.button_click(new Space(0, 1), new System.EventArgs()); };
            this.button3.Click += delegate { this.button_click(new Space(0, 2), new System.EventArgs()); };
            this.button4.Click += delegate { this.button_click(new Space(1, 0), new System.EventArgs()); };
            this.button5.Click += delegate { this.button_click(new Space(1, 1), new System.EventArgs()); };
            this.button6.Click += delegate { this.button_click(new Space(1, 2), new System.EventArgs()); };
            this.button7.Click += delegate { this.button_click(new Space(2, 0), new System.EventArgs()); };
            this.button8.Click += delegate { this.button_click(new Space(2, 1), new System.EventArgs()); };
            this.button9.Click += delegate { this.button_click(new Space(2, 2), new System.EventArgs()); };
        }

        private void Alpha_Load(object sender, EventArgs e)
        {
            gameb = new TicTacToeBoard();
            LoadBoard();
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;

        }

        private void LoadBoard()
        {
            if (gameb[0, 0] == Player.Open)
                button1.Text = "";
            else
            {
                button1.Text = gameb[0, 0].ToString();
                button1.Enabled = false;
            }

            if (gameb[0, 1] == Player.Open)
                button2.Text = "";
            else
            {
                button2.Text = gameb[0, 1].ToString();
                button2.Enabled = false;
            }

            if (gameb[0, 2] == Player.Open)
                button3.Text = "";
            else
            {
                button3.Text = gameb[0, 2].ToString();
                button3.Enabled = false;
            }

            if (gameb[1, 0] == Player.Open)
                button4.Text = "";
            else
            {
                button4.Text = gameb[1, 0].ToString();
                button4.Enabled = false;
            }

            if (gameb[1, 1] == Player.Open)
                button5.Text = "";
            else
            {
                button5.Text = gameb[1, 1].ToString();
                button5.Enabled = false;
            }

            if (gameb[1, 2] == Player.Open)
                button6.Text = "";
            else
            {
                button6.Text = gameb[1, 2].ToString();
                button6.Enabled = false;
            }

            if (gameb[2, 0] == Player.Open)
                button7.Text = "";
            else
            {
                button7.Text = gameb[2, 0].ToString();
                button7.Enabled = false;
            }

            if (gameb[2, 1] == Player.Open)
                button8.Text = "";
            else
            {
                button8.Text = gameb[2, 1].ToString();
                button8.Enabled = false;
            }

            if (gameb[2, 2] == Player.Open)
                button9.Text = "";
            else
            {
                button9.Text = gameb[2, 2].ToString();
                button9.Enabled = false;
            }
        }


        private void button_click(object sender, EventArgs e)
        {
            Space s = (Space)sender;

            gameb[s.X, s.Y] = Player.O;
            LoadBoard();
            if (WinnerCheck())
                Alpha_Load(null, new EventArgs());  //Winner was found
            DateTime startDate = DateTime.Now;

            if (gameb.SquaresAvailable.Count == gameb.BoardSize) //pick a random node
            {
                Random r = new Random();
                s = new Space(r.Next(0, 3), r.Next(0, 3));
            }
            else
                s = AI.BestMove(gameb, Player.X);

            DateTime endDate = DateTime.Now;
            TimeSpan diffTime = endDate.Subtract(startDate);
            double ms = diffTime.TotalMilliseconds;
            totalTime += ms;

            gameb[s.X, s.Y] = Player.X;
            LoadBoard();
            if (WinnerCheck())
                Alpha_Load(null, new EventArgs());  //Winner  found

        }

        
        public bool WinnerCheck()
        {
            Player? p = gameb.Winner;

            if (p == Player.X)
            {
                MessageBox.Show("Player X has won the game!\tTime taken to compute moves : " + (totalTime / 1000).ToString() + " s");
                totalTime = 0;
                return true;
            }
            else if (p == Player.O)
            {
                MessageBox.Show("Player O has won the game!\tTime taken to compute moves : " + (totalTime / 1000).ToString() + " s");
                totalTime = 0;
                return true;
            }
            else if (gameb.IsFull)
            {
                MessageBox.Show("The game was a tie!\tTime taken to compute moves : " + (totalTime / 1000).ToString() + " s");
                totalTime = 0;
                return true;
            }
            return false;
        }


        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 62);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;

            this.button2.Location = new System.Drawing.Point(93, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 62);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;

            this.button3.Location = new System.Drawing.Point(174, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 62);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;

            this.button4.Location = new System.Drawing.Point(12, 80);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 62);
            this.button4.TabIndex = 3;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;

            this.button5.Location = new System.Drawing.Point(93, 80);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 62);
            this.button5.TabIndex = 4;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;


            this.button6.Location = new System.Drawing.Point(174, 80);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 62);
            this.button6.TabIndex = 5;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;

            this.button7.Location = new System.Drawing.Point(12, 148);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 62);
            this.button7.TabIndex = 6;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;


            this.button8.Location = new System.Drawing.Point(93, 148);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 62);
            this.button8.TabIndex = 7;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;

            this.button9.Location = new System.Drawing.Point(174, 148);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 62);
            this.button9.TabIndex = 8;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "Alpha";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Alpha";
            this.Text = "Tic Tac Toe";
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Alpha_Load);
            this.ResumeLayout(false);

        }
    }
}

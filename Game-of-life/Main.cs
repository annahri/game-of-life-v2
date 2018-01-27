using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gameoflife {
    public partial class Main {
        public Main() {
            InitializeCanvas();
            Initialize();
        }

        bool begin;

        private void Canvas_Load(object sender, EventArgs e) {
            g = CreateGraphics();
            t.Interval = 50;
            t.Enabled = true;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e) {
            //g.FillRectangle(Brushes.Black,
            //new Rectangle(10,10,20,20));

            Draw(g);
        }

        private void Timer_tick(object sender, EventArgs e) {
			Text = $"Game of Life | Status : {begin.ToString()}";
			g.Clear(SystemColors.Control);
            Draw(g);
            CheckId(g);

            if (begin) {
                UpdateDraw();
            }
        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e) {
            Turn();
        }

		private void Canvas_MouseDown(object sender,MouseEventArgs e) {
		}

        private void Canvas_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Space) {
                if (begin) {
                    begin = false;
                } else {
                    begin = true;
                }
            }

            if (e.KeyCode == Keys.R) {
                if (!begin) {
                    Randomize();
                }
            }

            if (e.KeyCode == Keys.C) {
                if (!begin) {
                    Clear();
                }
            }

            if (e.KeyCode == Keys.Escape) {
                this.Close();
            }
        }
    }
}

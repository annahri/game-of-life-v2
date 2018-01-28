using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gameoflife {
    public partial class Main {
        public Main() {
            InitializeCanvas();
            Initialize(panel);
        }

        // Bools for conditions
        bool begin;
        bool ShowGrid = true;
        bool EnableDrag;

        void Canvas_Load(object sender, EventArgs e) {
            g = this.CreateGraphics();
            g1 = panel.CreateGraphics();
            t.Interval = 50;
            t.Enabled = true;
			t2.Interval = 100;
			t2.Enabled = true;
            panel.Focus();
        }

		void Panel_Paint(object sender,PaintEventArgs e) {

            if(ShowGrid) {
                DrawGrid(g1,this);
            }

            DrawOnlyTrue(g1);
			DrawBorder(g1,this);

			if (begin) {
				UpdateDraw();
			}
		}

        void Timer_tick(object sender, EventArgs e) {
            Text = "Conway's Game of Life | Status : " + ((begin) ? "Started" : "Stopped");
            CheckGrowth();

            populationStatusPanel.Text = $"{CountPopulation()}/{boxx.Length}";
            generationStatusPanel.Text = $"{generation}";
            gridStatusPanel.Text = (ShowGrid) ? "Grid" : "";
            statusStatusPanel.Text = (begin) ? $"[{growth}]" : "[STATUS]" ;
            setColor = (selectColor.Text == "") ? Color.DarkGreen : Color.FromName(selectColor.Text);

            panel.Invalidate();
        }

		void Timer2_tick(object sender,EventArgs e) {
            
		}

        void Panel_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                Turn(panel);
            }
            if (e.Button == MouseButtons.Right) {
                if(!EnableDrag) {
                    EnableDrag = true;
                } else {
                    EnableDrag = false;
                }
            }
        }

        void Panel_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Space) {
                if (begin) {
                    begin = false;
                } else {
                    begin |= CheckPopulation();
                }
            }

            if (e.KeyCode == Keys.R) {
                if (!begin) {
                    Randomize();
                    generation = 0;
                }
            }

            if (e.KeyCode == Keys.C) {
                if (!begin) {
                    Clear();
					generation = 0;
				}
            }

            if (e.KeyCode == Keys.G) {
                if (ShowGrid) {
                    ShowGrid = false;
                    offset = 0;
				} else { 
                    ShowGrid = true;
                    offset = 1;
                }
            } 

            if (e.KeyCode == Keys.Escape) {
                this.Close();
            }
        }

        void BtnHelp_Click(object sender, EventArgs e) {
            MessageBox.Show(
                "Hotkeys: \n" +
                "Press 'space bar' to begin the growth\n" +
                "Press 'R' to generate random cells\n" +
                "Press 'C' to clear the cells\n" + 
                "Press 'G' to show/hide grid\n" +
                "=====================================\n\n" +
                "Left click to a cell to change its state\n" + 
                "Right click to enable/disable drag",
                "Help me"
            );
            panel.Focus();
        }

		void Panel_MouseMove(object sender,MouseEventArgs e) {
            if (EnableDrag) {
                Turn(panel);
            }
        }
	}
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gameoflife {
    public partial class Main : Form{

		private System.ComponentModel.IContainer components = null;
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

        #region Declarations 
        public Graphics g;
        public Graphics g1;
        public Timer t;
        public Panel panel;
        #endregion

        private void InitializeCanvas () {
            this.components = new System.ComponentModel.Container();

            // Canvas size
            this.Width = 400;
            this.Height = 400;
            this.Text = "Conway's Game of life";

            // Canvas events
            this.Load += new EventHandler(this.Canvas_Load);
            this.Paint += new PaintEventHandler(this.Canvas_Paint);
            this.MouseClick += new MouseEventHandler(this.Canvas_MouseClick);
            this.KeyDown += new KeyEventHandler(this.Canvas_KeyDown);
            this.MouseDown += new MouseEventHandler(this.Canvas_MouseDown);

            // Panel size

            // Initialize objects
            this.t = new Timer(components);
            this.t.Tick += new EventHandler(this.Timer_tick);
        }
    }
}

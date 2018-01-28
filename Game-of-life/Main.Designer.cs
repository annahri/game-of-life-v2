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
        public Timer t2;
        public Panel panel;
        public Panel topPanel;
        public Panel botPanel;
        public StatusBar statusbar;
		public StatusBarPanel populationStatusPanel;
		public StatusBarPanel generationStatusPanel;
		public StatusBarPanel statusStatusPanel;
		public StatusBarPanel gridStatusPanel;
        public Label colorLabel;
        public ComboBox selectColor;
        public Button btnHelp;
        #endregion

        private void InitializeCanvas () {
            this.components = new System.ComponentModel.Container();
            this.panel = new Panel();
			this.topPanel = new Panel();
			this.botPanel = new Panel();
            this.colorLabel = new Label();
            this.selectColor = new ComboBox();
            this.statusbar = new StatusBar();
			this.populationStatusPanel = new StatusBarPanel();
			this.generationStatusPanel = new StatusBarPanel();
			this.statusStatusPanel = new StatusBarPanel();
			this.gridStatusPanel = new StatusBarPanel();
            this.btnHelp = new Button();

            // Canvas settings
            this.Width = 600;
            this.Height = 500;
            this.Text = "Conway's Game of life";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Canvas events
            this.Load += new EventHandler(this.Canvas_Load);

            // Panel settings
            this.panel.Dock = DockStyle.Bottom;
			this.panel.Height = 420;
			this.panel.Paint += new PaintEventHandler(this.Panel_Paint);
			this.panel.KeyDown += new KeyEventHandler(this.Panel_KeyDown);
			this.panel.MouseMove += new MouseEventHandler(this.Panel_MouseMove);
            this.panel.MouseClick += new MouseEventHandler(this.Panel_MouseClick);
            this.panel.LostFocus += (sender, e) => panel.Focus();

            // Top panel settings
            this.topPanel.Dock = DockStyle.Top;
            this.topPanel.Height = 30;
			this.topPanel.Controls.Add(btnHelp);
            this.topPanel.Controls.Add(colorLabel);
            this.topPanel.Controls.Add(selectColor);

            // colorLabel settings
            this.colorLabel.Location = new Point(10,8);
            this.colorLabel.Width = 70;
            this.colorLabel.Text = "Cells color: ";

            // Combo box select color settings
            this.selectColor.Location = new Point(colorLabel.Right, 5);
            foreach (System.Reflection.PropertyInfo prop in typeof(Color).GetProperties()) {
                if (prop.PropertyType.FullName == "System.Drawing.Color") {
                    selectColor.Items.Add(prop.Name);
                }
            }

            // Bottom panel settings
            this.botPanel.Dock = DockStyle.Bottom;
            this.botPanel.Height = 20;
			//this.botPanel.BackColor = Color.DimGray;
            this.botPanel.Controls.Add(statusbar);
			

            // btnHelp settings 
            this.btnHelp.Dock = DockStyle.None;
			this.btnHelp.Size = new Size(50,20);
			this.btnHelp.Location = new Point(this.Right - btnHelp.Width - 10,5);
			this.btnHelp.Text = "Help";
            this.btnHelp.TabStop = false;
            this.btnHelp.Click += new EventHandler(this.BtnHelp_Click);

            // statusbar settings
            this.statusbar.Dock = DockStyle.Fill;
            this.statusbar.SizingGrip = false;
            this.populationStatusPanel.BorderStyle = StatusBarPanelBorderStyle.None;
            this.populationStatusPanel.Text = "Population: 0";
            this.populationStatusPanel.ToolTipText = "Current population";
            this.statusbar.Panels.Add(populationStatusPanel);
            this.generationStatusPanel.BorderStyle = StatusBarPanelBorderStyle.None;
            this.generationStatusPanel.Text = "Generation";
            this.generationStatusPanel.ToolTipText = "Current generation";
			this.statusbar.Panels.Add(generationStatusPanel);
            this.gridStatusPanel.BorderStyle = StatusBarPanelBorderStyle.None;
            this.gridStatusPanel.Text = "Grid";
			this.statusbar.Panels.Add(gridStatusPanel);
            this.statusStatusPanel.BorderStyle = StatusBarPanelBorderStyle.None;
            this.statusStatusPanel.Text = "[STATUS]";
			this.statusbar.Panels.Add(statusStatusPanel);
            this.statusbar.ShowPanels = true;

			// Adds object control to the form
			this.Controls.Add(topPanel);
			this.Controls.Add(panel);
			this.Controls.Add(botPanel);

			// Initialize objects
			this.t = new Timer(components);
			this.t2 = new Timer(components);
			this.t.Tick += new EventHandler(this.Timer_tick);
			this.t2.Tick += new EventHandler(this.Timer2_tick);
		}
    }
}

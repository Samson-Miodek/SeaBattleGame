using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SeaBattleGame
{
    public partial class GameForm : Form
    {
	    public static int WindowWidth;
	    public static int WindowHeight;
	    private Timer timer;
        private bool IsMousePressed;
        private SolidBrush darkRed = new SolidBrush(Color.DarkRed);

        protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			TopMost = true;
			WindowState = FormWindowState.Maximized;
			
			WindowWidth = ClientSize.Width;
			WindowHeight = ClientSize.Height;
			
			DoubleBuffered = true;
			BackColor = Color.Black;

			timer = new Timer { Interval = 10 };
			timer.Tick += TimerOnTick;
			timer.Start();
			
			GameController.Initialization();
		}

		protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            Text = string.Format("Ходит игрок {0}", GameController.CurrentPlayerId+1);
            
            if(GameController.CurrentPlayerId == 0 && Mouse.position.X > WindowWidth/2)	
				GameView.DrawEllipse(g,darkRed,Mouse.position);
            if(GameController.CurrentPlayerId == 1 && Mouse.position.X < WindowWidth/2)	
	            GameView.DrawEllipse(g,darkRed,Mouse.position);
            
            GameView.DrawCurrentPlayer(g);
            GameView.DrawGrid(g);
        }
		
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			
			if(GameController.CurrentPlayerId == 0 && Mouse.position.X > WindowWidth/2)	
				GameController.CurrentPlayersAttack();
			if(GameController.CurrentPlayerId == 1 && Mouse.position.X < WindowWidth/2)	
				GameController.CurrentPlayersAttack();
		}
		
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			Mouse.UpdPosition(e.X,e.Y);
		}
		
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Button == MouseButtons.Right)
				IsMousePressed = true;
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Button == MouseButtons.Right)
				IsMousePressed = false;
		}
		private void TimerOnTick(object sender, EventArgs eventArgs)
		{
			Invalidate();
		}
		public GameForm()
		{
			InitializeComponent();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}
    }
}

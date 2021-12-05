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

        protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			TopMost = true;
			WindowState = FormWindowState.Maximized;
			
			WindowWidth = ClientSize.Width;
			WindowHeight = ClientSize.Height;
			
			GameView.leftPosition = new PointF(WindowWidth / 4 * 0.8f, WindowHeight * 0.01f);
			GameView.rightPosition = new PointF(WindowWidth / 2 * 1.4f, WindowHeight * 0.01f);
			
			DoubleBuffered = true;
			BackColor = Color.Black;

			timer = new Timer { Interval = 10 };
			timer.Tick += TimerOnTick;
			timer.Start();
			Text = "Sea Battle Game";

			GameController.Initialization();
		}

		protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            GameView.DrawGrid(g);
            GameView.DrawMousePosition(g);
            GameView.DrawCurrentPlayer(g);
            GameView.DrawTextInfo(g);
        }
		
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseMove(e);
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

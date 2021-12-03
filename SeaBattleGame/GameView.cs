using System;
using System.Drawing;
using System.Numerics;

namespace SeaBattleGame
{
    public static class GameView
    {
        private static SolidBrush redColor = new SolidBrush(Color.Red);
        public struct DividingLine
        {
            public static Pen pen = new Pen(Color.DimGray);
            public static Point p1 = new Point(GameForm.WindowWidth / 2, 0);
            public static Point p2 = new Point(GameForm.WindowWidth/ 2, GameForm.WindowHeight);
        }
        public static void DrawEllipse(Graphics g, SolidBrush color, Vector2 point)
        {
            g.FillEllipse(color,point.X-Ship.radius,point.Y-Ship.radius,2 * Ship.radius,2 * Ship.radius); 
        }
        public static void DrawCurrentPlayer(Graphics g)
        {
            var currentPlayer =  GameController.Players[GameController.CurrentPlayerId];

            foreach (var ship in currentPlayer.Ships)
                foreach (var position in ship.cellCoordinates)
                    DrawEllipse(g, ship.color, position);

            foreach (var ship in currentPlayer.WreckedShips)
                foreach (var position in ship.cellCoordinates)
                    DrawEllipse(g, ship.color, position);
        }

        public static void DrawGrid(Graphics g)
        {
            // Vertical
            var w = Math.Round(GameForm.WindowWidth / Ship.diameter)*Ship.diameter;
            for (var x = Ship.radius; x < w; x+=(int)Ship.diameter)
                g.DrawLine(DividingLine.pen,new Point(x,0),new Point(x,GameForm.WindowHeight));
            
            //Horizontal
            var h = Math.Round(GameForm.WindowHeight / Ship.diameter)*Ship.diameter;
            for (var y = Ship.radius; y < h; y+=(int)Ship.diameter)
                g.DrawLine(DividingLine.pen,new Point(0,y),new Point(GameForm.WindowWidth,y));
        }
    }
}
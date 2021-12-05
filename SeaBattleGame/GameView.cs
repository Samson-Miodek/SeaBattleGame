using System;
using System.Drawing;
using System.Numerics;

namespace SeaBattleGame
{
    public static class GameView
    {
        private static SolidBrush darkRed = new SolidBrush(Color.DarkRed);
        private static SolidBrush whiteColor = new SolidBrush(Color.White);
        private static Font font = new Font("Arial", 24);
        public static PointF leftPosition;
        public static PointF rightPosition;
        
        public struct DividingLine
        {
            public static Pen pen = new Pen(Color.DimGray);
            public static Point p1 = new Point(GameForm.WindowWidth / 2, 0);
            public static Point p2 = new Point(GameForm.WindowWidth/ 2, GameForm.WindowHeight);
        }
        public static void DrawEllipse(Graphics g, SolidBrush color, Vector2 point)
        {
            g.FillEllipse(color,point.X-Ship.radius,point.Y-Ship.radius,Ship.diameter,Ship.diameter); 
        }

        public static void DrawRectangle(Graphics g,SolidBrush color, Vector2 point)
        {
            g.FillRectangle(color,point.X-Ship.radius,point.Y-Ship.radius,Ship.diameter,Ship.diameter);
            g.DrawRectangle(Pens.Black, point.X-Ship.radius,point.Y-Ship.radius,Ship.diameter,Ship.diameter);
        }
        
        public static void DrawCurrentPlayer(Graphics g)
        {
            var currentPlayer =  GameController.Players[GameController.CurrentPlayerId];

            foreach (var ship in currentPlayer.Ships)
                foreach (var position in ship.cellCoordinates)
                    DrawRectangle(g, ship.color, position);

            foreach (var ship in currentPlayer.WreckedShips)
                foreach (var position in ship.cellCoordinates)
                    DrawRectangle(g, ship.color, position);
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

        public static void DrawMousePosition(Graphics g)
        {
            if (GameController.CurrentPlayerId == 0 && Mouse.position.X > GameForm.WindowWidth / 2)
                DrawEllipse(g,darkRed,Mouse.position);
            else if (GameController.CurrentPlayerId == 1 && Mouse.position.X < GameForm.WindowWidth/2)
                DrawEllipse(g,darkRed,Mouse.position);
        }
        
        public static void DrawTextInfo(Graphics g)
        {
            var countShips1 = GameController.Players[GameController.CurrentPlayerId].Ships.Count;
            var countShips2 = GameController.Players[(GameController.CurrentPlayerId+1)%2].Ships.Count;

            if (GameController.CurrentPlayerId == 0)
            {
                g.DrawString(string.Format("Моё поле {0}",countShips1), font,whiteColor,leftPosition);
                g.DrawString(string.Format("Поле противника {0}",countShips2), font,whiteColor,rightPosition);
            }else if(GameController.CurrentPlayerId == 1){
                g.DrawString(string.Format("Поле противника {0}",countShips2), font,whiteColor,leftPosition);
                g.DrawString(string.Format("Моё поле {0}",countShips1), font,whiteColor,rightPosition);
            }
        }
    }
}
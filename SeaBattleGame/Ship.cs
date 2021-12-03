using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace SeaBattleGame
{
    public class Ship
    {
        public static int radius = 10;
        public static float diameter = radius*2;
        
        public List<Vector2> position;
        public SolidBrush color;
        public Ship(Vector2 startPosition, int length, double angle, SolidBrush color)
        {
            this.color = color;
            this.position = new List<Vector2>(length);

            var delta = new Vector2(diameter * (float)Math.Cos(angle), diameter * (float)Math.Sin(angle));

            position.Add(startPosition);

            for (int i = 1; i < length; i++)
                position.Add(position[i - 1] + delta);
        }
    }
}
using System;
using System.Numerics;

namespace SeaBattleGame
{
    public static class Mouse
    {
        public static Vector2 position;

        public static void UpdPosition(int X, int Y)
        {
            var x = Math.Round(X/Ship.diameter)*Ship.diameter;
            var y = Math.Round(Y/Ship.diameter)*Ship.diameter;
            position = new Vector2((float)x, (float)y);
        }
    }
}
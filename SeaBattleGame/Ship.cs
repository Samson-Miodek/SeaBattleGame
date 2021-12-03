﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace SeaBattleGame
{
    public class Ship
    {
        public static int radius = 10;
        public static float diameter = radius*2;

        public SolidBrush color;
        public List<Vector2> cellCoordinates;

        public Ship()
        {
            this.cellCoordinates = new List<Vector2>();
        }

        public Ship(Vector2 startPosition, int numberCells, double angle, SolidBrush color)
        {
            this.color = color;
            this.cellCoordinates = new List<Vector2>(numberCells);

            var delta = new Vector2(diameter * (float)Math.Cos(angle), diameter * (float)Math.Sin(angle));

            cellCoordinates.Add(startPosition);

            for (int i = 1; i < numberCells; i++)
                cellCoordinates.Add(cellCoordinates[i - 1] + delta);
        }
    }
}
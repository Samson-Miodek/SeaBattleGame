using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace SeaBattleGame
{
    public static  class GameController
    {
        private static int numberShips = 15;
        private static int currentPlayerId = 0;
        private static Dictionary<int, Player> players;
        
        public static Dictionary<int, Player> Players
        {
            get { return players; }
        }
        public static int CurrentPlayerId
        {
            get { return currentPlayerId; }
        }

        public static void Initialization()
        {
            currentPlayerId = 0;
            players = new Dictionary<int, Player>();
            
            players.Add(0,new Player());
            players.Add(1,new Player());

            var color1 = new SolidBrush(Color.LightGreen);
            var color2 = new SolidBrush(Color.Blue);
;            
            players[0].Ships = GetShips(0,color1);
            players[1].Ships = GetShips( GameForm.WindowWidth / 2,color2);

            players[0].WreckedShips = new List<Ship>();
            players[1].WreckedShips = new List<Ship>();
        }

        private static List<Ship> GetShips(float offsetX, SolidBrush color)
        {
            var ships = new List<Ship>(numberShips);
            var random = new Random();
            var w = GameForm.WindowWidth / 2;
            var h = GameForm.WindowHeight;
            
            for (var i = 0; i < numberShips; i++)
            {
                var angle = (random.Next(5))*Math.PI /2;
                var numberCells = random.Next(4) + 3;
                
                var posX = w * random.NextDouble();
                var posY = h * random.NextDouble();
                
                var x = Math.Round(posX/Ship.diameter)*Ship.diameter;
                var y = Math.Round(posY/Ship.diameter)*Ship.diameter;
                
                var startPosition = new Vector2((float)x, (float)y);
                startPosition.X += offsetX;
                
                ships.Add(new Ship(startPosition,numberCells,angle,color));
            }

            return ships;
        }

        private static bool CanCurrentPlayerAttack()
        {
            return CurrentPlayerId == 1 && Mouse.position.X < GameForm.WindowWidth / 2
                || CurrentPlayerId == 0 && Mouse.position.X > GameForm.WindowWidth / 2;
        }

        public static void CurrentPlayersAttack()
        {
            if(!CanCurrentPlayerAttack())	
                return;

            var anotherPlayer = players[(currentPlayerId + 1) % 2];
            var isHit = false;
            
            for(var shipIndex = 0; shipIndex < anotherPlayer.Ships.Count; shipIndex++)
            {
                var ship = anotherPlayer.Ships[shipIndex];

                for (var coordinateIndex = 0; coordinateIndex < ship.cellCoordinates.Count; coordinateIndex++)
                {
                    var distance = (ship.cellCoordinates[coordinateIndex] - Mouse.position).Length();
                    if (distance < Ship.diameter)
                    {
                        AddWreckedShip(ship.cellCoordinates[coordinateIndex], new SolidBrush(Color.Red));
                        ship.cellCoordinates.RemoveAt(coordinateIndex);
                        if (ship.cellCoordinates.Count == 0)
                            anotherPlayer.Ships.Remove(ship);
                        isHit = true;
                    }
                }
                if (ship.cellCoordinates.Count == 0)
                    anotherPlayer.Ships.Remove(ship);
            }

            if (!isHit)
            {
                AddWreckedShip(Mouse.position,new SolidBrush(Color.Gray));
                currentPlayerId++;
                currentPlayerId %= players.Count;
            }
        }

        private static void AddWreckedShip(Vector2 position, SolidBrush color)
        {
            players[currentPlayerId].WreckedShips.Add(new Ship(position,color));
        }
    }
}
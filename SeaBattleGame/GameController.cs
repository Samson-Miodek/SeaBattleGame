using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace SeaBattleGame
{
    public static  class GameController
    {
        private static int numberShips = 45;
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

        public static void Init()
        {
            currentPlayerId = 0;
            players = new Dictionary<int, Player>();
            
            players.Add(0,new Player());
            players.Add(1,new Player());

            players[0].Ships = GetShips(0, 150);
            players[1].Ships = GetShips( GameForm.WindowWidth / 2,250);

            players[0].CoordinatesWreckedShips = new List<Vector2>();
            players[1].CoordinatesWreckedShips = new List<Vector2>();
        }

        private static List<Ship> GetShips(float offsetX,int clr)
        {
            var ships = new List<Ship>(numberShips);
            var random = new Random();
            var color = new SolidBrush(Color.FromArgb(255,clr,255,clr));
            for (int i = 0; i < numberShips; i++)
            {
                var w = GameForm.WindowWidth / 2;
                var h = GameForm.WindowHeight;
                
                var X = w * random.NextDouble();
                var Y = h * random.NextDouble();
                
                var x = Math.Round(X/Ship.diameter)*Ship.diameter;
                var y = Math.Round(Y/Ship.diameter)*Ship.diameter;
                
                var startPosition = new Vector2((float)x, (float)y);
                startPosition.X += offsetX;
                
                var angle = (random.Next(5))*Math.PI /2;
                ships.Add(new Ship(startPosition,random.Next(4)+3,angle,color));
            }

            return ships;
        }

        public static void CurrentPlayersAttack()
        {
            var currentPlayer = players[CurrentPlayerId];

            foreach (var playerId in players.Keys)
            {
                if(playerId == CurrentPlayerId)
                    continue;
                
                foreach (var ship in players[playerId].Ships)
                    for (int i = 0; i < ship.position.Count; i++)
                    {
                        var distance = (ship.position[i] - Mouse.position).Length();
                        if (distance < Ship.diameter)
                        {
                            currentPlayer.CoordinatesWreckedShips.Add(ship.position[i]);
                            ship.position.RemoveAt(i);
                            return;
                        }
                    }
            }
            currentPlayerId++;
            currentPlayerId %= players.Count;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Player : IGameObject
    {
        public IPosition Position { get; set; }

        public Player(IPosition pos)
        {
            Position = pos;
        }
        
        public void GoUp() { Position.Coords = new Point(Position.Coords.X, Position.Coords.Y - 5);}
        public void GoDown() { Position.Coords = new Point(Position.Coords.X, Position.Coords.Y + 5); }
        public void GoL() { Position.Coords = new Point(Position.Coords.X - 5, Position.Coords.Y); }
        public void GoR() { Position.Coords = new Point(Position.Coords.X + 5, Position.Coords.Y); }
    }
}

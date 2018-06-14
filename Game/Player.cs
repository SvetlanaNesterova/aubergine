using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Player : MovingObject
    {
        public int Health { get; internal set; }

        public Player(Position pos)
        {
            Position = pos;
        }
    }

}

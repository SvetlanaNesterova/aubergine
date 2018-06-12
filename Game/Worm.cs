using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Worm : IGameObject
    {
        public Worm(Position position)
        {
            Position = position;
        }

        public Position Position { get; set; }
    }
}

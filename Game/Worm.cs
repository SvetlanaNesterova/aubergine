using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Worm : GameObject
    {
        public Worm(Position position)
        {
            Position = position;
        }

        internal void Die()
        {
            throw new NotImplementedException();
        }
    }
}

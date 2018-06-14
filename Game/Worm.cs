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
        public bool IsDead { get; private set; }

        public Worm(Position position)
        {
            Position = position;
            IsOnMap = true;
            IsDead = false;
        }

        public void Die()
        {
            IsOnMap = false;
            IsDead = true;
        }
    }
}

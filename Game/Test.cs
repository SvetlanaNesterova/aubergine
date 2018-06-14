using Aubergine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Eat : CollideInteraction<Player, Worm>
    {
        public override void Happen(Player subject, Worm obj)
        {
            subject.Health++;
            obj.Die();
        }
    }

    class Test
    {
    }
}

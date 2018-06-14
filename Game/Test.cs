using Aubergine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Eat : ICollideInteraction<Player, Worm>
    {
        public void Do(Player subject, Worm obj)
        {
            subject.Health++;
            obj.Die();
        }

        public bool IsAvailiable(Player subject, Worm obj)
        {
            throw new NotImplementedException();
        }
    }

    class Test
    {
    }
}

using Aubergine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Eat : IConditionalEvent<Player, Worm>
    {
        public void Happen(Player subject, Worm obj)
        {
            subject.Health++;
            obj.Die();
        }

        public bool ShouldHappenNow(Player subject, Worm obj)
        {
            return subject.Position.IsIntersectedWith(obj.Position);
        }
    }

    class Attack : IInteraction<Player, Worm>
    {
        public void Do(Player subject, Worm obj)
        {
            subject.Health--;
            obj.Die();
        }

        public bool IsAvailiable(Player subject, Worm obj)
        {
            return subject.Health > 10;
        }
    }

    class Test
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine.realisation
{
    class Hero : IGameObject
    {
        public Position Position { get; set; }

    }

    class Hit : Interaction<Hero, Hero>
    {
        public override void Do()
        {
            throw new NotImplementedException();
        }

        public override bool IsAvailiable()
        {
            throw new NotImplementedException();
        }
    }

}

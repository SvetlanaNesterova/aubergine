using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine.realisation
{
    class Hero : IGameObject
    {
        public IPosition Position { get; set; }

    }




    class Kill : Interaction<Hero, Hero>
    {
        public Kill(Hero first, Hero second)
        {

        }

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

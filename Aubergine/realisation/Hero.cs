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

        public void Do()
        {
            throw new NotImplementedException();
        }

        public bool IsAvailiable()
        {
            throw new NotImplementedException();
        }

        public override void Do(Hero first, Hero second)
        {
            throw new NotImplementedException();
        }

        public override bool IsAvailiable(Hero first, Hero second)
        {
            throw new NotImplementedException();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine.realisation
{
    public class MainLoop
    {
        private ISpace space;

        public MainLoop(ISpace space)
        {
            this.space = space;
        }

        public void Start()
        {
            while (space.Exist)
            {
                var hero1 = new Hero();
                var hero2 = new Hero();
                //space.Happen(new Kill(hero1, hero2));

                space.Tick();

                // отрисовать изменения
            }
        }
    }
}

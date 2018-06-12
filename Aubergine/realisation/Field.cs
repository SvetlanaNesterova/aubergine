using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    class Field : ISpace
    {
        List<IGameObject> Objects;
        public Field()
        {
        }

        public bool Exist => throw new NotImplementedException();

        public void Tick()
        {
            foreach(var obj in Objects)
            {
                obj.
            }
        }

        public bool TryMove(IGameObject gObject, IPosition position)
        {
            
        }


    }
}

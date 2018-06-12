using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine.realisation;

namespace Aubergine
{
    class Field : ISpace
    {
        List<IGameObject> Objects = new List<IGameObject>();
        public Field()
        {
        }

        public bool Exist => throw new NotImplementedException();

        public void Happen(Interaction<IGameObject, IGameObject> event_)
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            foreach(var obj in Objects)
            {
                obj.Position = new Position();
            }
        }

        public bool TryMove(IGameObject gObject, Position position)
        {
            return true;
        }


    }
}

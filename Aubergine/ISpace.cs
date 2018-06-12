using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aubergine
{
    public interface ISpace
    {
        bool Exist { get; }

        void Happen(Interaction<IGameObject, IGameObject> event_);

        //bool TryMove(IGameObject<IPosition> gObject, IPosition position);
        void Tick();
    }
}

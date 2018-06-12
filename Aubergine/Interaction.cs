using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public abstract class Interaction<TSubject, TObject> where TSubject : IGameObject
    {
        public abstract void Do(TSubject first, TObject second);
        public abstract bool IsAvailiable(TSubject first, TObject second);

    }

}
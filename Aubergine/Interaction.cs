using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public interface IInteraction<TSubject, TObject>
        where TSubject : GameObject
        where TObject : GameObject
    {
        void Do();
        bool IsAvailiable();
    }

    public interface ICollideInteraction<TSubject, TObject> : IInteraction<TSubject, TObject>
        where TSubject : GameObject
        where TObject : GameObject
    { }
}
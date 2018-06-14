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
        void Do(TSubject subject, TObject obj);
        bool IsAvailiable(TSubject subject, TObject obj);
    }


    public interface IConditionalEvent
    {
        void Happen();
        bool ShouldHappenNow();
    }

    public interface ICollideInteraction<TSubject, TObject> : IInteraction<TSubject, TObject>
        where TSubject : GameObject
        where TObject : GameObject
    { }


}
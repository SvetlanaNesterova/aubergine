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

    public abstract class CollideInteraction<TSubject, TObject> : IInteraction<TSubject, TObject>
        where TSubject : GameObject
        where TObject : GameObject
    {
        public abstract void Do(TSubject subject, TObject obj);

        public bool IsAvailiable(TSubject subject, TObject obj)
        {
            return subject.Position.IsIntersectedWith(obj.Position);
        }

        bool IInteraction<TSubject, TObject>.IsAvailiable(TSubject subject, TObject obj)
        {
            throw new NotImplementedException();
        }
    }
}
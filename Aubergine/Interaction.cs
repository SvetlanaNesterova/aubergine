using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public interface IInteraction<in TSubject, in TObject> 
        where TSubject : GameObject
        where TObject : GameObject
    {
        void Do(TSubject subject, TObject obj);
        bool IsAvailiable(TSubject subject, TObject obj);
    }

    public interface IConditionalEvent<in TObject1, in TObject2>
       where TObject1 : GameObject
       where TObject2 : GameObject
    {
        void Happen(TObject1 subject, TObject2 obj);
        bool ShouldHappenNow(TObject1 subject, TObject2 obj);
    }
    
    public abstract class CollideInteraction<TObject1, TObject2> : IConditionalEvent<TObject1, TObject2>
        where TObject1 : GameObject
        where TObject2 : GameObject
    {
        public abstract void Happen(TObject1 subject, TObject2 obj);

        public bool ShouldHappenNow(TObject1 subject, TObject2 obj)
        {
            return subject.Position.IsIntersectedWith(obj.Position);
        }
    }
    
    // private
    class StarndardCollideInteraction<TObject1, TObject2> : CollideInteraction<TObject1, TObject2>
        where TObject1 : GameObject
        where TObject2 : GameObject
    {
        private Action<TObject1, TObject2> action;

        public StarndardCollideInteraction(Action<TObject1,TObject2> action)
        {
            this.action = action;
        }

        public override void Happen(TObject1 subject, TObject2 obj)
        {
            action.Invoke(subject, obj);
        }
    }
}
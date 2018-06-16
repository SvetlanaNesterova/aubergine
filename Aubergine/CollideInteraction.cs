using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public abstract class CollideInteraction<TObject1, TObject2> : IConditionalEvent<TObject1, TObject2>
        where TObject1 : GameObject
        where TObject2 : GameObject
    {
        public abstract void Happen(TObject1 subject, TObject2 obj);

        public virtual bool ShouldHappenNow(TObject1 subject, TObject2 obj)
        {
            return subject.Position.IsTouchedWith(obj.Position);
        }
    }

    class SimpleCollideInteraction<TObject1, TObject2> : CollideInteraction<TObject1, TObject2>
        where TObject1 : GameObject
        where TObject2 : GameObject
    {
        private Action<TObject1, TObject2> action;

        public SimpleCollideInteraction(Action<TObject1, TObject2> action)
        {
            this.action = action;
        }

        public override void Happen(TObject1 subject, TObject2 obj)
        {
            action(subject, obj);
        }
    }
}

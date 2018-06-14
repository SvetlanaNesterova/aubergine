using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    public interface IConditionalEvent<in TObject1, in TObject2>
       where TObject1 : GameObject
       where TObject2 : GameObject
    {
        void Happen(TObject1 subject, TObject2 obj);
        bool ShouldHappenNow(TObject1 subject, TObject2 obj);
    }

    public class ConditionalEventWrapper
    {
        public static ConditionalEventWrapper CreateWrapper<T1, T2>(
            IConditionalEvent<T1, T2> iConditional)
            where T1 : GameObject
            where T2 : GameObject
        {
            var instance = new ConditionalEventWrapper(iConditional, typeof(T1), typeof(T2));
            return instance;
        }

        Func<GameObject, GameObject, bool> shouldHappen;
        Action<GameObject, GameObject> happen;
        private Type firstArgType;
        private Type secondArgType;

        private ConditionalEventWrapper() { }

        private ConditionalEventWrapper(object iConditional, Type firstType, Type secondType)
        {
            firstArgType = firstType;
            secondArgType = secondType;
            var type = iConditional.GetType();

            var happenMethod = type.GetMethod("Happen");
            happen = (first, second) =>
            {
                happenMethod.Invoke(iConditional, new[] { first, second });
            };

            var isAvailiableMethod = type.GetMethod("ShouldHappenNow");
            shouldHappen = (first, second) =>
            {
                return (bool)isAvailiableMethod.Invoke(iConditional, new[] { first, second });
            };
        }

        private void ValidateArgsTypes(GameObject first, GameObject second)
        {
            if (firstArgType != first.GetType())
                throw new ArgumentException(
                    $"Incorrect argument type {first.GetType()} for 'first'. " +
                    $"Expected {firstArgType}.");
            if (secondArgType != second.GetType())
                throw new ArgumentException(
                    $"Incorrect argument type {second.GetType()} for 'second'. " +
                    $"Expected {secondArgType}.");
        }

        public void Happen(GameObject first, GameObject second)
        {
            ValidateArgsTypes(first, second);
            happen(first, second);
        }

        public bool ShouldHappenNow(GameObject first, GameObject second)
        {
            ValidateArgsTypes(first, second);
            return shouldHappen(first, second);
        }
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
            action.Invoke(subject, obj);
        }
    }
}
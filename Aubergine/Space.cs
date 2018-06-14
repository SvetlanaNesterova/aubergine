using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Aubergine
{
    public class Space
    {
        public List<GameObject> objects;
        private Dictionary<Type, Dictionary<Type, ConditionalEventWrapper>> conditionalEvents;

        public virtual bool Exist { get; }

        public virtual void Happen(IConditionalEvent<GameObject, GameObject> event_) { }

        public Space() : this(new GameObject[] { }) { }

        public Space(GameObject[] objects) : 
            this(objects, new CollideInteraction<GameObject, GameObject>[] { }) { }

        public Space(GameObject[] objects,
            IConditionalEvent<GameObject, GameObject>[] conditionalEvents)
        {
            this.conditionalEvents = new Dictionary<Type, Dictionary<Type, ConditionalEventWrapper>>();
            foreach (var action in conditionalEvents)
            {
                action.GetType();

                //AddToDictionary()
            }
            //this.collideInteractions;
            this.objects = objects.ToList();
            var enumerable = objects
                .OfType<ParametrizedGameObject>();

            foreach (var obj in enumerable)
                foreach (var dict in obj.ConditionalEvents)
                    AddToDictionary(obj.GetType(), dict.Key, dict.Value);

        }

        public Space(GameObject[] objects,
            CollideInteraction<GameObject, GameObject>[] collideInteractions,
            IInteraction<GameObject, GameObject>[] interactions)
        {

        }

         public void Tick()
        {
            foreach (var first in objects)
            {
                foreach (var second in objects)
                {
                    if (second == first)
                        continue;
                    foreach (var conditionalEvent in GetPossibleEventsFor(first, second))
                    {
                        if (conditionalEvent.ShouldHappenNow(first, second))
                            conditionalEvent.Happen(first, second);
                    }
                }
            }
            // проверить все пересечения, произвести действия
            objects = objects.Where(obj => obj.IsOnMap).ToList();
        }

        public void AddIConditionalEvent<TFirst, TSecond>(IConditionalEvent<TFirst, TSecond> action)
           where TFirst : GameObject
           where TSecond : GameObject
        {
            AddToDictionary(typeof(TFirst), typeof(TSecond), ConditionalEventWrapper.CreateWrapper(action));
        }

        public void AddCollideInteraction<TFirst, TSecond>(CollideInteraction<TFirst, TSecond> action)
           where TFirst : GameObject
           where TSecond : GameObject
        {
            AddIConditionalEvent(action);
        }

        public void AddCollideInteraction<TFirst, TSecond>(Action<TFirst, TSecond> action)
                    where TFirst : GameObject
                    where TSecond : GameObject
        {
            AddCollideInteraction(new SimpleCollideInteraction<TFirst, TSecond>(action));
        }

        private IEnumerable<ConditionalEventWrapper> GetPossibleEventsFor<TFirst, TSecond>(TFirst first, TSecond second)
            where TFirst : GameObject
            where TSecond : GameObject
        {
            var firstType = first.GetType();
            var secondType = second.GetType();
            var res = GetFromDictionary(firstType, secondType);
            if (res != null)
                yield return res;
        }

        private void AddToDictionary(Type first, Type second, ConditionalEventWrapper action)
        {
            if (!conditionalEvents.ContainsKey(first))
                conditionalEvents[first] = new Dictionary<Type, ConditionalEventWrapper>();
            conditionalEvents[first][second] = action;
        }

        private ConditionalEventWrapper GetFromDictionary(Type first, Type second)
        {
            if (conditionalEvents.ContainsKey(first))
                if (conditionalEvents[first].ContainsKey(second))
                    return conditionalEvents[first][second];
            return null;
        }

        public void MoveCameraView(Point vector)
        {
            MoveCameraView(Direction.Right, vector.X);
            MoveCameraView(Direction.Up, vector.Y);
        }

        public void MoveCameraView(Direction direction, int distance)
        {
            foreach (var obj in objects)
            {
                obj.Position.MoveDirection(direction, distance);
            }
        }

    }
    /*
    public interface Action<in T1, in T2>
    {

    }*/
}

/*
 действия при пересечении

    if (пересекается)
     выполни действие

 проверка пересечения

     * 
     */
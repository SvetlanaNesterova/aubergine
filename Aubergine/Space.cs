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
            CollideInteraction<GameObject, GameObject>[] collideInteractions)
        {
            this.conditionalEvents = new Dictionary<Type, Dictionary<Type, ConditionalEventWrapper>>();
            foreach (var interaction in collideInteractions)
            {
                interaction.GetType();

                //AddToDictionary()
            }
            //this.collideInteractions;
            this.objects = objects.ToList();
            var enumerable = objects
                .Where(obj => obj is ParametrizedGameObject)
                .OfType<ParametrizedGameObject>();

            foreach (var obj in enumerable)
                foreach (var dict in obj.CollideInteractions)
                    AddToDictionary(obj.GetType(), dict.Key, dict.Value);

        }

        IConditionalEvent<GameObject, GameObject>[] Events;
        IInteraction<GameObject, GameObject>[] Interactions;

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
                    var res = GetPossibleEventsFor(first, second);
                    //if (res == null)
                    //    res = GetIntersection(objType, subjType);
                    if (res == null)
                        continue;
                    foreach (var conditionalEvent in res)
                    {
                        if (conditionalEvent.ShouldHappenNow(first, second))
                            conditionalEvent.Happen(first, second);
                    }
                    //var type = res.GetType();
                    //bool isAvailiable = (bool)type.GetMethod("ShouldHappenNow").Invoke(res, new[] { first, second });
                    //if (isAvailiable)
                    //    type.GetMethod("Happen").Invoke(res, new[] { first, second });
                }
            }
            // проверить все пересечения, произвести действия

            //throw new NotImplementedException();
            objects = objects.Where(obj => obj.IsOnMap).ToList();
        }

        public void AddCollideAction<TFirst, TSecond>(Action<TFirst, TSecond> action)
            where TFirst : GameObject
            where TSecond : GameObject
        {
            AddToDictionary(typeof(TFirst), typeof(TSecond),
                new SimpleCollideInteraction<TFirst, TSecond>(action));
        }

        public void AddIConditionalEvent<TFirst, TSecond>(IConditionalEvent<TFirst, TSecond> action)
           where TFirst : GameObject
           where TSecond : GameObject
        {
            AddToDictionary(typeof(TFirst), typeof(TSecond), action);
        }

        public void AddCollideInteraction<TFirst, TSecond>(CollideInteraction<TFirst, TSecond> action)
           where TFirst : GameObject
           where TSecond : GameObject
        {
            AddToDictionary(typeof(TFirst), typeof(TSecond), action);
        }

        private void AddToDictionary<TFirst, TSecond>(
            Type first, Type second,
            CollideInteraction<TFirst, TSecond> action)
            where TFirst : GameObject
            where TSecond : GameObject
        {
            if (!conditionalEvents.ContainsKey(first))
                conditionalEvents[first] = new Dictionary<Type, ConditionalEventWrapper>();
            conditionalEvents[first][second] = new ConditionalEventWrapper(action);
        }

        private void AddToDictionary(Type first, Type second, object action)
        {
            if (!conditionalEvents.ContainsKey(first))
                conditionalEvents[first] = new Dictionary<Type, ConditionalEventWrapper>();
            conditionalEvents[first][second] = new ConditionalEventWrapper(action);
        }

        private IEnumerable<ConditionalEventWrapper> GetPossibleEventsFor<TFirst, TSecond>(TFirst first, TSecond second)
            where TFirst : GameObject
            where TSecond : GameObject
        {
            var firstType = first.GetType();
            var secondType = second.GetType();
            var res = GetIntersection(firstType, secondType);
            if (res != null)
                return new List<ConditionalEventWrapper>() { res };
            return new List<ConditionalEventWrapper>() { };
        }
        private void AddToDictionary(
            Type first, Type second,
            object action)
        {
            if (!collideInteractions.ContainsKey(first))
                collideInteractions[first] = new Dictionary<Type, object>();
            // м.б. проблема
            collideInteractions[first][second] = action;
        }


        public Action<TFirst, TSecond> GetIntersection<TFirst, TSecond>()
            where TFirst : GameObject
            where TSecond : GameObject
        {
            var first = typeof(TFirst);
            var second = typeof(TSecond);
            if (conditionalEvents.ContainsKey(first))
                if (conditionalEvents[first].ContainsKey(second))
                    return ((IConditionalEvent<TFirst, TSecond>)conditionalEvents[first][second]).Happen;
            // проблемы с тем, что CollideInteraction имеется только в одну сторону
            
            //if (collideInteractions.ContainsKey(second))
            //    if (collideInteractions[second].ContainsKey(first))
            //        return ((CollideInteraction<TFirst, TSecond>)collideInteractions[second][first]).Do;
            return null;
        }

        private ConditionalEventWrapper GetIntersection(Type first, Type second)
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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Collections.Immutable;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Aubergine
{
    public class World
    {
        public ImmutableList<GameObject> Objects { get; private set; }
        public Physics Physics { get; }

        private Dictionary<Type, Dictionary<Type, List<ConditionalEventWrapper>>> conditionalEvents = 
            new Dictionary<Type, Dictionary<Type, List<ConditionalEventWrapper>>>();

        public World(Physics physics) : this(physics, new GameObject[] { }) { }

        public World(Physics physics, GameObject[] objects) :
            this(physics, objects, new ConditionalEventWrapper[] { })
        { }

        public World(Physics physics, GameObject[] objects,
            ConditionalEventWrapper[] conditionalEvents)
        {
            Physics = physics;
            Physics.AddWorld(this);
            Position.AddPhysics(physics);

            this.Objects = objects.ToImmutableList();
            var enumerable = objects
                .OfType<ParametrizedGameObject>();
            foreach (var obj in enumerable)
                AddParametrizedObject(obj);
             
            foreach (var action in conditionalEvents)
            {
                AddToDictionary(action.FirstArgType, action.SecondArgType, action);
            }

        }

        public World(Physics physics, GameObject[] objects,
            CollideInteraction<GameObject, GameObject>[] collideInteractions,
            IInteraction<GameObject, GameObject>[] interactions)
        {

        }

        public void Tick()
        {
            foreach (var first in Objects)
            {
                foreach (var second in Objects)
                {
                    foreach (var conditionalEvent in GetPossibleEventsFor(first, second))
                    {
                        if (conditionalEvent.ShouldHappenNow(first, second))
                            conditionalEvent.Happen(first, second);
                    }
                }
            }
            // проверить все пересечения, произвести действия
            Objects = Objects.Where(obj => obj.IsOnMap).ToImmutableList();
        }
        
        /*private void InjectPhysics(GameObject obj)
        {
            var movable = obj as IMovingObject;
            if (movable == null)
                return;
            var type = obj.GetType();
            var old = type.GetMethod("MoveOnVector");
            type.
            Func<Point, bool> newM = (vector) =>
            {
                if (Physics.MoveOnVector(obj, vector))
                    return ((IMovingObject)obj).MoveOnVector(vector);
                return false;
            };
            Injector.Inject(type, old, newM.Method);
        }*/
        
        public bool AddObject(GameObject obj)
        {
            // if не помещается на карте - return false;
            Objects = Objects.Add(obj);
            if (obj is ParametrizedGameObject)
                AddParametrizedObject((ParametrizedGameObject)obj);
            return true;
        }

        private void AddParametrizedObject(ParametrizedGameObject obj)
        {
            foreach (var dict in obj.ConditionalEvents)
                foreach (var item in dict.Value)
                    AddToDictionary(obj.GetType(), dict.Key, item);
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
            foreach (var event_ in res)
                yield return event_;
        }

        private void AddToDictionary(Type first, Type second, ConditionalEventWrapper action)
        {
            if (!conditionalEvents.ContainsKey(first))
                conditionalEvents[first] = new Dictionary<Type, List<ConditionalEventWrapper>>();
            if (!conditionalEvents[first].ContainsKey(second))
                conditionalEvents[first][second] = new List<ConditionalEventWrapper>();
            conditionalEvents[first][second].Add(action);
        }

        private List<ConditionalEventWrapper> GetFromDictionary(Type first, Type second)
        {
            if (conditionalEvents.ContainsKey(first))
                if (conditionalEvents[first].ContainsKey(second))
                    return conditionalEvents[first][second];
            return new List<ConditionalEventWrapper>();
        }

        public void MoveCameraView(Point vector)
        {
            MoveCameraView(Direction.Right, vector.X);
            MoveCameraView(Direction.Up, vector.Y);
        }

        public void MoveCameraView(Direction direction, int distance)
        {
            foreach (var obj in Objects)
                obj.Position.MoveDirection(direction, distance);
        }
    }
}

/*
 действия при пересечении

    if (пересекается)
     выполни действие

 проверка пересечения

     * 
     */
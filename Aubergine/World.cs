﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Aubergine
{
    public class World
    {
        public List<GameObject> objects;
        private Dictionary<Type, Dictionary<Type, List<ConditionalEventWrapper>>> conditionalEvents;

        public virtual bool Exist { get; }

        public virtual void Happen(IConditionalEvent<GameObject, GameObject> event_) { }

        public World() : this(new GameObject[] { }) { }

        public World(GameObject[] objects) :
            this(objects, new ConditionalEventWrapper[] { })
        { }

        public World(GameObject[] objects,
            ConditionalEventWrapper[] conditionalEvents)
        {
            this.conditionalEvents = new Dictionary<Type, Dictionary<Type, List<ConditionalEventWrapper>>>();
            foreach (var action in conditionalEvents)
            {
                AddToDictionary(action.FirstArgType, action.SecondArgType, action);
            }
            this.objects = objects.ToList();
            var enumerable = objects
                .OfType<ParametrizedGameObject>();

            foreach (var obj in enumerable)
                foreach (var dict in obj.ConditionalEvents)
                    foreach (var item in dict.Value)
                        AddToDictionary(obj.GetType(), dict.Key, item);

        }

        public World(GameObject[] objects,
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

        public IEnumerable<GameObject> GetObjectsInRectangle(Rectangle region)
        {
            return objects
                .Where(obj => region.IntersectsWith(obj.Position.Body));
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
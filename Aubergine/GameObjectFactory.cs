using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public abstract class ParametrizedGameObject : GameObject
    {
        private Dictionary<Type, object> parameters;
        internal Dictionary<Type, List<ConditionalEventWrapper>> ConditionalEvents =
            new Dictionary<Type, List<ConditionalEventWrapper>>();

        public TValue Get<TValue, TName>()
            where TName : Parameter<TValue>
            where TValue : IComparable
        {
            // if contains
            var name = typeof(TName);
            if (parameters.ContainsKey(name))
                return ((TName)parameters[name]).Value;
            else
                throw new Exception($"IParameter {name.Name} (FullName: {name.FullName}) was not found.");
        }

        public void Set<TValue, TName>(TValue value)
            where TName : Parameter<TValue>
            where TValue : IComparable
        {
            // if contains
            // if min<value<max
            ((TName)parameters[typeof(TName)]).Value = value;
        }

        internal void SetParameters(Dictionary<Type, object> dictionary)
        {
            parameters = dictionary;
        }
    }

    public abstract class Parameter<T> where T : IComparable
    {
        public T Value { get; set; }
        public T Min { get; set; }
        public T Max { get; set; }
    }

    public class ParametrizedCharacter<TCharacter>
        where TCharacter : ParametrizedGameObject, new()
    {
        internal Dictionary<Type, object> parameters { get; } = new Dictionary<Type, object>();
        private Dictionary<Type, List<ConditionalEventWrapper>> conditionalEvents =
            new Dictionary<Type, List<ConditionalEventWrapper>>();

        public ParametrizedCharacter<TCharacter> WithParameter<TValue, TName>(
            TValue current, TValue min, TValue max)
            where TName : Parameter<TValue>, new()
            where TValue : IComparable
        {
            Func<TName> parameterCreator = () => new TName() { Value = current, Min = min, Max = max };
            // может бфть ошибка
            parameters[typeof(TName)] = parameterCreator;
            return this;
        }

        public TCharacter CreateOnPosition(Position position)
        {
            var obj = new TCharacter { Position = position, ConditionalEvents = conditionalEvents };
            var d = new Dictionary<Type, object>();

            foreach (var parameter in parameters)
            {
                d[parameter.Key] = ((Func<object>)parameter.Value)();
            }
            obj.SetParameters(d);
            return obj;
        }
        
        public ParametrizedCharacter<TCharacter> AddCollideInteraction<T>(Action<TCharacter, T> action)
            where T : ParametrizedGameObject
        {
            if (!conditionalEvents.ContainsKey(typeof(T)))
                conditionalEvents[typeof(T)] = new List<ConditionalEventWrapper>();
            conditionalEvents[typeof(T)].Add(ConditionalEventWrapper.CreateWrapper(
                new SimpleCollideInteraction<TCharacter, T>((a, b) =>
                {
                    if (a != b) action(a, b);
                })));
            return this;
        }

        public ConditionalEventCreator If(Func<TCharacter, bool> condition)
        {
            return new ConditionalEventCreator(condition, this);
        }

        public class ConditionalEventCreator
        {
            private Func<TCharacter, bool> condition;
            private readonly ParametrizedCharacter<TCharacter> parametrizedCharacter;


            public ConditionalEventCreator(Func<TCharacter, bool> c, ParametrizedCharacter<TCharacter> paramCharacter)
            {
                condition = c;
                parametrizedCharacter = paramCharacter;
            }

            public ParametrizedCharacter<TCharacter> Then(Action<TCharacter> action)
            {
                var cond = new SimpleConditionalEvent<TCharacter, TCharacter>(
                    (firstObj, secondObj) => firstObj == secondObj && condition(firstObj),
                    (firstObj, secondObj) =>
                    {
                        if (firstObj == secondObj)
                            action(firstObj);
                    });

                if (!parametrizedCharacter.conditionalEvents.ContainsKey(typeof(TCharacter)))
                    parametrizedCharacter.conditionalEvents[typeof(TCharacter)] = new List<ConditionalEventWrapper>();

                parametrizedCharacter.conditionalEvents[typeof(TCharacter)].Add(cond.Wrap());
                return parametrizedCharacter;
            }
        }
    }

    public static class GameObjectFactory
    {
        public static ParametrizedCharacter<T> GetParametrizedCharacter<T>()
            where T : ParametrizedGameObject, new()
        {
            return new ParametrizedCharacter<T>();
        }
    }
}
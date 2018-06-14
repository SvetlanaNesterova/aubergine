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

        public TValue Get<TValue, TName>()
            where TName : Parameter<TValue>
            where TValue : IComparable
        {
            // if contains
            var name = typeof(TName);
            if (parameters.ContainsKey(name))
                return ((TName)parameters[name]).Value;
            else
                throw new Exception($"Parameter {name.Name} (FullName: {name.FullName}) was not found.");
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

    public class Hero : ParametrizedGameObject { }

    public class Health : Parameter<int> { }

    public abstract class Parameter<T> where T: IComparable
    {
        public T Value { get; set; }
        public T Min { get; set; }
        public T Max { get; set; }
    }


    public class ParametrizedCharacter<TCharacter> 
        where TCharacter : ParametrizedGameObject , new()
    {
        private Dictionary<Type, object> Parameters = new Dictionary<Type, object>();
        
        public ParametrizedCharacter<TCharacter> WithParameter<TValue, TName>(
            TValue current, TValue min, TValue max)
            where TName : Parameter<TValue>, new()
            where TValue : IComparable
        {
            Func<TName> parameterCreator = () => new TName() {Value = current, Min = min, Max = max};
            // может бфть ошибка
            Parameters[typeof(TName)] = parameterCreator;
            return this;
        }

        public TCharacter Create()
        {
            var obj = new TCharacter();
            var d = new Dictionary<Type, object>();
            
            foreach (var parameter in Parameters)
            {
                d[parameter.Key] = ((Func<object>)parameter.Value)();
            }
            obj.SetParameters(d);
            return obj;
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

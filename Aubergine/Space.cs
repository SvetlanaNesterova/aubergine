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
        private Dictionary<Type, Dictionary<Type, object>> collideInteractions;

        public virtual bool Exist { get; }

        public virtual void Happen(IConditionalEvent<GameObject, GameObject> event_) { }

        public Space() : this(new GameObject[] { }) { }

        public Space(GameObject[] objects) : 
            this(objects, new CollideInteraction<GameObject, GameObject>[] { }) { }

        public Space(GameObject[] objects,
            CollideInteraction<GameObject, GameObject>[] collideInteractions)
        {
            this.collideInteractions = new Dictionary<Type, Dictionary<Type, object>>();
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

        public void Tick()
        {
            foreach (var subj in objects)
            {
                foreach (var obj_ in objects)
                {
                    if (obj_ == subj)
                        continue;
                    var subjType = subj.GetType();
                    var objType = obj_.GetType();
                    var res = GetIntersection(subjType, objType);
                    //if (res == null)
                    //    res = GetIntersection(objType, subjType);
                    if (res == null)
                        continue;
                    var type = res.GetType();
                    bool isAvailiable = (bool)type.GetMethod("ShouldHappenNow").Invoke(res, new[] { subj, obj_ });
                    if (isAvailiable)
                        type.GetMethod("Happen").Invoke(res, new[] { subj, obj_ });
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
                new StarndardCollideInteraction<TFirst, TSecond>(action));
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
            if (!collideInteractions.ContainsKey(first))
                collideInteractions[first] = new Dictionary<Type, object>();
            collideInteractions[first][second] = action;
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
            if (collideInteractions.ContainsKey(first))
                if (collideInteractions[first].ContainsKey(second))
                    return ((CollideInteraction<TFirst, TSecond>)collideInteractions[first][second]).Happen;
            // проблемы с тем, что CollideInteraction имеется только в одну сторону
            
            //if (collideInteractions.ContainsKey(second))
            //    if (collideInteractions[second].ContainsKey(first))
            //        return ((CollideInteraction<TFirst, TSecond>)collideInteractions[second][first]).Do;
            return null;
        }

        private object GetIntersection(Type first, Type second)
        {
            if (collideInteractions.ContainsKey(first))
                if (collideInteractions[first].ContainsKey(second))
                    return collideInteractions[first][second];
            // проблемы с тем, что CollideInteraction имеется только в одну сторону

            //if (collideInteractions.ContainsKey(second))
            //    if (collideInteractions[second].ContainsKey(first))
            //        return ((CollideInteraction<TFirst, TSecond>)collideInteractions[second][first]).Do;
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
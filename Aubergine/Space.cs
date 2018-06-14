using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Aubergine
{
    public class Space
    {
        public virtual bool Exist { get; }

        public virtual void Happen(IInteraction<GameObject, GameObject> event_) { }

        public Space() : this(new GameObject[] { }) { }


        public Space(GameObject[] objects)
            
        {

        }

        public Space(GameObject[] objects,
            ICollideInteraction<GameObject, GameObject>[] collideInteractions)
        {

        }


        public void Tick()
        {
            // проверить все пересечения, произвести действия

            //throw new NotImplementedException();
            objects = objects.Where(obj => obj.IsOnMap).ToList();
        }

        public List<GameObject> objects;

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

        private Dictionary<Type, Dictionary<Type, Action<GameObject, GameObject>>> intersectionActions =
            new Dictionary<Type, Dictionary<Type, Action<GameObject, GameObject>>>();

        public void SetIntersection<TFirst, TSecond>(Action<TFirst, TSecond> action)
            where TFirst : GameObject
            where TSecond : GameObject
        {
            AddToDictionary(typeof(TFirst), typeof(TSecond), (Action<GameObject, GameObject>)action);
            AddToDictionary(typeof(TSecond), typeof(TFirst), (Action<GameObject, GameObject>)action);
        }

        private void AddToDictionary(Type first, Type second, Action<GameObject, GameObject> action)
        {
            if (!intersectionActions.ContainsKey(first))
                intersectionActions[first] = new Dictionary<Type, Action<GameObject, GameObject>>();
            intersectionActions[first][second] = action;
        }

        public Action<TFirst, TSecond> GetIntersection<TFirst, TSecond>()
            where TFirst : GameObject
            where TSecond : GameObject
        {
            var first = typeof(TFirst);
            var second = typeof(TSecond);
            if (intersectionActions.ContainsKey(first))
                if (intersectionActions[first].ContainsKey(second))
                    return intersectionActions[first][second];
            if (intersectionActions.ContainsKey(second))
                if (intersectionActions[second].ContainsKey(first))
                    return intersectionActions[second][first];
            return null;
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
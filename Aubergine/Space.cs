using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Aubergine
{
    public abstract class Space
    {
        public abstract bool Exist { get; }

        public abstract void Happen(Interaction<GameObject, GameObject> event_);

        public abstract void Tick();

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
    }
}

/*
 действия при пересечении

    if (пересекается)
     выполни действие

 проверка пересечения

     * 
     */
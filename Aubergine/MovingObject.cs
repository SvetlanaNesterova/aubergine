using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public abstract class MovingObject : GameObject
    {
        public bool MoveOnVector(Point vector)
        {
            // проверка корректности
            Position.MoveRight(vector.X);
            Position.MoveUp(vector.Y);
            return true;
        }

        public bool MoveInDirection(Direction direction, int distance)
        {
            // проверка корректности
            Position.MoveDirection(direction, distance);
            return true;
        }
    }
}

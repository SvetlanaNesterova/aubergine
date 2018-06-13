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
        public bool MoveUp(int distance)
        {
            // проверка корректности
            Position.MoveUp(distance);
            return true;
        }

        public bool MoveDown(int distance)
        {
            // проверка корректности
            Position.MoveDown(distance);
            return true;
        }

        public bool MoveRight(int distance)
        {
            // проверка корректности
            Position.MoveRight(distance);
            return true;
        }

        public bool MoveLeft(int distance)
        {
            // проверка корректности
            Position.MoveLeft(distance);
            return true;
        }

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

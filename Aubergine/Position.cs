using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public class Position
    {
        public static Physics Physics { get; private set; }
        public static void AddPhysics(Physics physics)
        {
            // if (Physics != null)
            Physics = physics;
        }

        public Point Coords { get; private set; }
        public Size Size { get; private set; }
        public Rectangle Body => new Rectangle(Coords, Size);

        /// <summary>
        /// Является ли объект проницаемым для других непроницаемых объектов
        /// </summary>
        public bool IsPermeable { get; set; } = false;

        public Position(Point point, Size size)
        {
            Coords = point;
            Size = size;
        }

        public bool IsIntersectedWith(Position other)
        {
            return Body.IntersectsWith(other.Body);
        }

        public bool IsTouchedWith(Position other)
        {
            var intersection = Rectangle.Intersect(Body, other.Body);
            return !intersection.IsEmpty;
        }

        public bool TryMoveOn(Point vector)
        {
            if (Physics == null || Physics.AllowMoveOnVector(this, vector))
            {
                TeleportateOn(vector);
                return true;
            }
            return false;
        }

        public void TeleportateOn(Point vector)
        {
            Coords = Point.Add(Coords, new Size(vector));
        }

        // to delete
        public void MoveDirection(Direction direction, int distance)
        {
            var vector = direction.ToVector();
            vector.X = distance * vector.X;
            vector.Y = distance * vector.Y;
            TryMoveOn(vector);
        }
    }
}

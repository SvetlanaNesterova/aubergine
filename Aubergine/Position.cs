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
        public Point Coords { get; set; }
        public Size Size { get; set; }
        public Rectangle Body { get => new Rectangle(Coords, Size); }

        public bool IsIntersectedWith(Position other)
        {
            return Body.IntersectsWith(other.Body);
        }

        internal void MoveUp(int distance)
        {
            Coords.Offset(0, distance);
        }

        internal void MoveDown(int distance)
        {
            Coords.Offset(0, -distance);
        }

        internal void MoveRight(int distance)
        {
            Coords.Offset(distance, 0);
        }

        internal void MoveLeft(int distance)
        {
            Coords.Offset(-distance, 0);
        }

        internal void MoveDirection(Direction direction, int distance)
        {
            var vector = direction.ToVector();
            vector.X = distance * vector.X;
            vector.Y = distance * vector.Y;
            var newCoords = Coords;
            newCoords.Offset(vector);
            Coords = newCoords;
        }
    }
}

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
        public Rectangle Body => new Rectangle(Coords, Size);

        public Position(Point point, Size size)
        {
            Coords = point;
            Size = size;
        }

        public bool IsIntersectedWith(Position other)
        {
            return Body.IntersectsWith(other.Body);
        }
        
        public void MoveDirection(Direction direction, int distance)
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

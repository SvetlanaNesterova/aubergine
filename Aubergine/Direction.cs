using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public enum Direction
    {
        None = 0,
        Up,
        Down,
        Right,
        Left
    }

    public static class DirectionExtensions
    {
        public static Point ToVector(this Direction direction)
        {
            var vector = new Point(0, 0);
            if (direction == Direction.Up)
                vector = new Point(0, 1);
            if (direction == Direction.Down)
                vector = new Point(0, -1);
            if (direction == Direction.Right)
                vector = new Point(1, 0);
            if (direction == Direction.Left)
                vector = new Point(-1, 0);
            return vector;
        }
    }
}

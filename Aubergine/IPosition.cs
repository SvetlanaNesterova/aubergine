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
        public Point Coords;

        public bool IsIntersectedWith(Position a)
        {
            var p = a as Position;
            if (p != null)
                return Coords == p.Coords;
            return false; 
        }
    }
}

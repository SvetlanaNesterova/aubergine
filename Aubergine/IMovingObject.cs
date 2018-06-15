using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public interface IMovingObject
    {
        int Speed { get; set; }
        bool MoveInDirection(Direction direction, int distance);
    }
}

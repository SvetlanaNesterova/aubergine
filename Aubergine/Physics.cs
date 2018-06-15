using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public abstract class Physics
    {
        public abstract void AddWorld(World world);

        public abstract bool AllowMoveOnVector(Position obj, Point vector);
    }

    public class FourWaysPhysics : Physics
    {
        private World world;

        public override void AddWorld(World world) => this.world = world;

        public override bool AllowMoveOnVector(Position position, Point vector)
        {
            if (position.IsPermeable)
                return true;

            var newPoint = Point.Add(position.Coords, new Size(vector));
            var newPosition = new Position(newPoint, position.Size);

            return world.Objects
                .Select(o => o.Position)
                .Where(p => !p.IsPermeable && p != position)
                .All(other => !other.IsIntersectedWith(newPosition));
        }
    }
}

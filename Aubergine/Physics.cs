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

        public abstract bool MoveOnVector(GameObject obj, Point vector);
    }

    public class ForWaysUnpermeablePhysics : Physics
    {
        private World world;

        public override void AddWorld(World world) => this.world = world;

        public override bool MoveOnVector(GameObject obj, Point vector)
        {
            Console.WriteLine("Physics exsists for " + obj.ToString());
            if (obj.IsPermeable)
                return true;
           
            var newPosition = new Position(
                Point.Add(obj.Position.Coords, new Size(vector)),
                obj.Position.Size);
            foreach (var otherObj in world.Objects.Where(o => o.IsPermeable))
            {
                if (otherObj.Position.IsIntersectedWith(obj.Position))
                    return false;
            }
            return true;
        }
    }
}

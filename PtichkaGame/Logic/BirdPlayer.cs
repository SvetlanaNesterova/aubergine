using System;
using Aubergine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PtichkaGame.Logic
{
    public class BirdPlayer : GameObject
    {
        static int a = 0;
        static List<BirdPlayer> pl = new List<BirdPlayer>();

        public BirdPlayer(Position position) : base(position)
        {
            a++;
            pl.Add(this);
        }

        public void MoveInDirection(Direction direction, int distance)
        {
            var vector = direction.ToVector();
            var newPoint = new Point(vector.X * distance, vector.Y * distance);
            Position.TryMoveOn(newPoint);
        }
    }
}

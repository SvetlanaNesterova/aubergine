using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Game : Space
    {
        public Game()
        {
            objects = new List<GameObject>
            {
                new Player(new Position() {Coords = new Point(450,200)}),
                new Worm(new Position() {Coords = new Point(100, 100)}),
                new Worm(new Position() {Coords = new Point(900, 100)}),
                new Worm(new Position() {Coords = new Point(100, 300)}),
                new Worm(new Position() {Coords = new Point(400, 500)})
            };
        }

        public override bool Exist { get; }
        public override void Happen(Interaction<GameObject, GameObject> event_)
        {
            throw new NotImplementedException();
        }

        public void MoveObjects(Direction direction)
        {
            foreach (var obj in objects)
            {
                if (obj is Player) continue;
                obj.MoveInDirection(direction, 5);
            }
        }

    }
}

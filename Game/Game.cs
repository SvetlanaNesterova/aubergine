using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Game : ISpace
    {
        public List<IGameObject> objects;
        public Game()
        {
            objects = new List<IGameObject>
            {
                new Player(new Position() {Coords = new Point(450,200)}),
                new Worm(new Position() {Coords = new Point(100, 100)}),
                new Worm(new Position() {Coords = new Point(900, 100)}),
                new Worm(new Position() {Coords = new Point(100, 300)}),
                new Worm(new Position() {Coords = new Point(400, 500)})
            };
        }

        public bool Exist { get; }
        public void Happen(Interaction<IGameObject, IGameObject> event_)
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            //throw new NotImplementedException();
        }

        public void moveObjects(Direction direction)
        {
            foreach (var obj in objects)
            {
                if (obj is Player) continue;
                if (direction == Direction.Up)
                    obj.Position.Coords = new Point(obj.Position.Coords.X, obj.Position.Coords.Y + 5);
                if (direction == Direction.Down)
                    obj.Position.Coords = new Point(obj.Position.Coords.X, obj.Position.Coords.Y - 5);
                if (direction == Direction.Right)
                    obj.Position.Coords = new Point(obj.Position.Coords.X - 5, obj.Position.Coords.Y);
                if (direction == Direction.Left)
                    obj.Position.Coords = new Point(obj.Position.Coords.X + 5, obj.Position.Coords.Y);
            }
        }
    }
}

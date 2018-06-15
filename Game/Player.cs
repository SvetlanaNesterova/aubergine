using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Player : ParametrizedGameObject
    {
        public Player()
        { }

        public Player(Position position)
        {
            Position = position;
        }

        public int Health { get; internal set; }
        
        public bool MoveOnVector(Point vector)
        {
            // проверка корректности
            //Position.MoveRight(vector.X);
            //Position.MoveUp(vector.Y);
            throw new NotImplementedException("Current method not implimented");
            //return true;
        }

        public bool MoveInDirection(Direction direction, int distance)
        {
            // проверка корректности
            Position.MoveDirection(direction, distance);
            return true;
        }
    }

}

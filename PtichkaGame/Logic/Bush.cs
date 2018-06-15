using Aubergine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtichkaGame.Logic
{
    class NutritionalValue : Parameter<int> { }

    public class Bush : GameObject
    {
        public Bush(Position position) : base(position) { }

        public int MaxBerriesCount { get; private set; } = 10;
        public int BerriesCount { get; private set; } = 10;

        public void BeCollected()
        {
            BerriesCount = 0;
            IsOnMap = false;
        }

        public void Grow()
        {
            if (BerriesCount < MaxBerriesCount)
                BerriesCount++;
        }
    }
}

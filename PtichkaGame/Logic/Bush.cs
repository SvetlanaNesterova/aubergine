using Aubergine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtichkaGame.Logic
{
    //  КУСТ
    public class Bush : GameObject
    {
        public Bush(Position position) : base(position) { }

        public int MaxBerriesCount { get; private set; } = 10;
        public int BerriesCount { get; private set; } = 10;

        public void BeCollected()
        {
            BerriesCount = 0;
        }

        internal void GrowBerry()
        {
            BerriesCount = Math.Max(BerriesCount + 1, MaxBerriesCount);
        }
    }

    public class Grow : IConditionalEvent<Bush, Bush>
    {
        private int calls = 0;
        private int enougthCalls = 100;

        public void Happen(Bush object1, Bush object2)
        {
            if (calls == enougthCalls)
            {
                object1.GrowBerry();
                calls = 0;
            }
            else
                calls++;
        }

        public bool ShouldHappenNow(Bush object1, Bush object2)
        {
            return object1 == object2 &&
                object1.BerriesCount < object1.MaxBerriesCount;
        }
    }
}

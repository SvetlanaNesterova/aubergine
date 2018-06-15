using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace PtichkaGame.Logic
{
    public class Collect : IConditionalEvent<BirdPlayer, Bush>
    {
        public void Happen(BirdPlayer bird, Bush bush)
        {
            bush.BeCollected();
        }
        
        public bool ShouldHappenNow(BirdPlayer bird, Bush bush)
        {
            //return bird.WantToCollect == bush && bush.BerriesCount > 0;
            return bird.Position.IsTouchedWith(bush.Position) && bush.BerriesCount > 0;
        }
    }
}

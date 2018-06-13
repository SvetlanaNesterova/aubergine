using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public abstract class Interaction<TSubject, TObject> 
        where TSubject : GameObject 
    {
        public abstract void Do();
        public abstract bool IsAvailiable();

    }

}
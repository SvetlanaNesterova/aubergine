using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public abstract class GameObject
    {
        public virtual Position Position { get; set; }
        public bool IsOnMap { get; protected set; }

    }
}
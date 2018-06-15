﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Worm : ParametrizedGameObject
    {
        public bool IsDead { get; private set; }
        
        public void Die()
        {
            IsOnMap = false;
            IsDead = true;
        }
    }
}

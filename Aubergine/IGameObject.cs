﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine
{
    public interface IGameObject
    {
        IPosition Position { get; set; }
    }
}

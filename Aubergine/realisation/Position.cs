﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aubergine.realisation
{
    class Position : IPosition
    {
        int x;
        int y;
        int radius;
        public Point Coords { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Summit
    {
        public const int INFINITE = 1000000;
        public int Potential;
        public bool Mark;
        public Coordinate Coordinate;
        public Summit Previous;

        public Summit(Coordinate coord)
        {
            Potential = INFINITE;
            Mark = false;
            Coordinate = coord;
            Previous = null;
        }

    }
}

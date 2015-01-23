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
        public Coordinates Coordinate;
        public Summit Previous;

        public Summit(Coordinates coord)
        {
            Potential = INFINITE;
            Mark = false;
            Coordinate = coord;
            Previous = null;
        }

    }
}

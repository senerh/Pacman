using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Coordinate
    {
        public int X, Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinate(Vector2 pos)
        {
            X = (int)pos.X / 16;
            Y = (int)pos.Y / 16;
        }

        // on surcharge l’opérateur == pour l’égalité entre les coordonnées 
        public static Boolean operator ==(Coordinate c1, Coordinate c2)
        {
            return ((c1.X == c2.X) && (c1.Y == c2.Y));
        }
        // on surcharge l’opérateur == pour la différence entre les  coordonnées 
        public static Boolean operator !=(Coordinate c1, Coordinate c2)
        {
            return ((c1.X != c2.X) || (c1.Y != c2.Y));
        }

    }
}

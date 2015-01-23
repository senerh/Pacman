﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Coordinates
    {
        public int X, Y;

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinates(Vector2 pos)
        {
            X = (int)pos.X / 16;
            Y = (int)pos.Y / 16;
        }

        // on surcharge l’opérateur == pour l’égalité entre les coordonnées 
        public static Boolean operator ==(Coordinates c1, Coordinates c2)
        {
            return ((c1.X == c2.X) && (c1.Y == c2.Y));
        }
        // on surcharge l’opérateur == pour la différence entre les  coordonnées 
        public static Boolean operator !=(Coordinates c1, Coordinates c2)
        {
            return ((c1.X != c2.X) || (c1.Y != c2.Y));
        }

    }
}

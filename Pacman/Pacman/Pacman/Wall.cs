using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Wall : Tile
    {
        //FIELDS

        //CONSTRUCTOR
        public Wall(int x, int y) : base(x, y, Resources.wall)
        {

        }
        
        //METHODS

        //UPDATE & DRAW
    }
}

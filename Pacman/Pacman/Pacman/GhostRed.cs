using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pacman
{
    class GhostRed : Ghost
    {
        //CONSTANTS
        const int SPEED = 2;
        const int ANIMATION_SPEED = 5;
        const int TIME_TO_WAIT = 7 * 60;
        const int TIME_VULNERABLE = 5 * 60;

        //CONSTRUCTOR
        public GhostRed(int x, int y, Engine engine) : base(x, y, engine, Resources.ghostRed, SPEED, ANIMATION_SPEED, TIME_TO_WAIT, TIME_VULNERABLE)
        {
            
        }

        //METHODS
        protected override Direction follow(Coordinates pacmanCoordinates)
        {
            return dijkstra.getDirection(getGridPosition(), pacmanCoordinates);
        }
    }
}

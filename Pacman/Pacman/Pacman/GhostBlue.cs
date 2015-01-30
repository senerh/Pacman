using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class GhostBlue : Ghost
    {
        //CONSTANTS
        const int SPEED = 2;
        const int ANIMATION_SPEED = 5;
        const int TIME_TO_WAIT = 4 * 60;
        const int TIME_VULNERABLE = 5 * 60;

        const int FOLLOW = 10;
        int i;
        Coordinates targetedCoordinates;
        //CONSTRUCTOR
        public GhostBlue(int x, int y, Engine engine) : base(x, y, engine, Resources.ghostBlue, SPEED, ANIMATION_SPEED, TIME_TO_WAIT, TIME_VULNERABLE)
        {
            i = FOLLOW;
        }

        //METHODS
        protected override Direction follow(Coordinates pacmanCoordinates)
        {
            if (i < FOLLOW)
            {
                i++;
            }
            else
            {
                i = 0;
                targetedCoordinates = engine.getRandomBeanCoordinates();
            }
            return dijkstra.getDirection(getGridPosition(), targetedCoordinates);
        }
    }
}

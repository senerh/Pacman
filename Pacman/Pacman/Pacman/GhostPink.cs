using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class GhostPink : Ghost
    {
        //CONSTANTS
        const int SPEED = 2;
        const int ANIMATION_SPEED = 5;
        const int TIME_TO_WAIT = 6 * 60;
        const int TIME_VULNERABLE = 5 * 60;

        const int FOLLOW = 5;
        int i;
        Coordinates targetedCoordinates;
        //CONSTRUCTOR
        public GhostPink(int x, int y, Engine engine) : base(x, y, engine, Resources.ghostPink, SPEED, ANIMATION_SPEED, TIME_TO_WAIT, TIME_VULNERABLE)
        {
            i = FOLLOW;
        }

        //METHODS
        protected override Direction follow(Coordinates pacmanCoordinates)
        {
            if (pacmanCoordinates.X > Grid.GRID_WIDTH / 2 && pacmanCoordinates.Y > Grid.GRID_HEIGHT / 2)
            {
                return dijkstra.getDirection(getGridPosition(), pacmanCoordinates);
            }
            else
            {
                if (i < FOLLOW)
                {
                    i++;
                }
                else
                {
                    i = 0;
                    targetedCoordinates = engine.getRandomEmptyCoordinates();
                }
                return dijkstra.getDirection(getGridPosition(), targetedCoordinates);
            }
        }
    }
}

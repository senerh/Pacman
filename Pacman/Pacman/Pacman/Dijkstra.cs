using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Dijkstra
    {
        private Summit[,] summits;
        private Engine engine;
        private Coordinates topLeft, topRight, botLeft, botRight;
        public Dijkstra(Engine engine)
        {
            this.engine = engine;
            summits = new Summit[Grid.GRID_WIDTH, Grid.GRID_HEIGHT];
            for (int x = 0; x < Grid.GRID_WIDTH; x++)
            {
                for (int y = 0; y < Grid.GRID_HEIGHT; y++)
                {
                    if (!engine.isWallOnMap(x, y))
                        summits[x, y] = new Summit(new Coordinates(x, y));
                }
            }

            int i, j;

            i = 1; j = 1;
            while (summits[i, j] == null)
            {
                i++;
                if (i > Grid.GRID_WIDTH / 2)
                {
                    i = 1;
                    j++;
                }
            }
            topLeft = new Coordinates(i, j);

            i = Grid.GRID_WIDTH - 2; j = 1;
            while (summits[i, j] == null)
            {
                i--;
                if (i < Grid.GRID_WIDTH / 2)
                {
                    i = Grid.GRID_WIDTH - 2;
                    j++;
                }
            }
            topRight = new Coordinates(i, j);

            i = 1; j = Grid.GRID_HEIGHT - 2;
            while (summits[i, j] == null)
            {
                i++;
                if (i > Grid.GRID_WIDTH / 2)
                {
                    i = 1;
                    j--;
                }
            }
            botLeft = new Coordinates(i, j);

            i = Grid.GRID_WIDTH - 2; j = Grid.GRID_HEIGHT - 2;
            while (summits[i, j] == null)
            {
                i--;
                if (i < Grid.GRID_WIDTH / 2)
                {
                    i = Grid.GRID_WIDTH - 2;
                    j--;
                }
            }
            botRight = new Coordinates(i, j);
        }
        public Direction getDirection(Coordinates currentCoordinates, Coordinates targetedCoordinates)
        {

            if (currentCoordinates.X == 0)
                return Direction.Right;
            else if (currentCoordinates.X == Grid.GRID_WIDTH - 1)
                return Direction.Left;
            else if (currentCoordinates.Y == 0)
                return Direction.Down;
            else if (currentCoordinates.Y == Grid.GRID_HEIGHT - 1)
                return Direction.Up;

            //Initialisation des sommets
            for (int x = 0; x < Grid.GRID_WIDTH; x++)
            {
                for (int y = 0; y < Grid.GRID_HEIGHT; y++)
                {
                    if (summits[x, y] != null)
                    {
                        summits[x, y].Mark = false;
                        summits[x, y].Potential = Summit.INFINITE;
                        summits[x, y].Previous = null;
                    }
                }
            }

            Summit currentSummit = summits[currentCoordinates.X, currentCoordinates.Y];
            currentSummit.Potential = 0;
            currentSummit.Mark = true;
            Summit nextSummit = currentSummit;
            Summit targetedSummit = summits[targetedCoordinates.X, targetedCoordinates.Y];

            while (nextSummit.Coordinate != targetedSummit.Coordinate)
            {
                nextSummit.Mark = true;

                if (nextSummit.Coordinate.X > 0 && nextSummit.Coordinate.Y > 0
                    && nextSummit.Coordinate.X < Grid.GRID_WIDTH - 1 && nextSummit.Coordinate.Y < Grid.GRID_HEIGHT - 1)
                {
                    //Case du haut
                    if (!engine.isWallOnMap(nextSummit.Coordinate.X, nextSummit.Coordinate.Y - 1))
                    {
                        Summit s = summits[nextSummit.Coordinate.X, nextSummit.Coordinate.Y - 1];
                        if (s.Potential > nextSummit.Potential + 1)
                        {
                            summits[(int)nextSummit.Coordinate.X, nextSummit.Coordinate.Y - 1].Previous = nextSummit;
                            summits[(int)nextSummit.Coordinate.X, nextSummit.Coordinate.Y - 1].Potential = nextSummit.Potential + 1;
                        }
                    }
                    //Case du bas
                    if (!engine.isWallOnMap(nextSummit.Coordinate.X, nextSummit.Coordinate.Y + 1))
                    {
                        Summit s = summits[(int)nextSummit.Coordinate.X, (int)nextSummit.Coordinate.Y + 1];
                        if (s.Potential > nextSummit.Potential + 1)
                        {
                            summits[(int)nextSummit.Coordinate.X, (int)nextSummit.Coordinate.Y + 1].Previous = nextSummit;
                            summits[(int)nextSummit.Coordinate.X, (int)nextSummit.Coordinate.Y + 1].Potential = nextSummit.Potential + 1;
                        }
                    }
                    //Case de gauche
                    if (!engine.isWallOnMap(nextSummit.Coordinate.X - 1, nextSummit.Coordinate.Y))
                    {
                        Summit s = summits[(int)nextSummit.Coordinate.X - 1, (int)nextSummit.Coordinate.Y];
                        if (s.Potential > nextSummit.Potential + 1)
                        {
                            summits[(int)nextSummit.Coordinate.X - 1, (int)nextSummit.Coordinate.Y].Previous = nextSummit;
                            summits[(int)nextSummit.Coordinate.X - 1, (int)nextSummit.Coordinate.Y].Potential = nextSummit.Potential + 1;
                        }
                    }
                    //Case de droite
                    if (!engine.isWallOnMap(nextSummit.Coordinate.X + 1, nextSummit.Coordinate.Y))
                    {
                        Summit s = summits[(int)nextSummit.Coordinate.X + 1, (int)nextSummit.Coordinate.Y];
                        if (s.Potential > nextSummit.Potential + 1)
                        {
                            summits[(int)nextSummit.Coordinate.X + 1, (int)nextSummit.Coordinate.Y].Previous = nextSummit;
                            summits[(int)nextSummit.Coordinate.X + 1, (int)nextSummit.Coordinate.Y].Potential = nextSummit.Potential + 1;
                        }
                    }
                }

                int minimum = Summit.INFINITE;

                for (int x = 0; x < Grid.GRID_WIDTH; x++)
                {
                    for (int y = 0; y < Grid.GRID_HEIGHT; y++)
                    {
                        if (summits[x, y] != null && !summits[x, y].Mark && summits[x, y].Potential < minimum)
                        {
                            minimum = summits[x, y].Potential;
                            nextSummit = summits[x, y];
                        }
                    }
                }
            }

            Summit caseSuivante = summits[(int)nextSummit.Coordinate.X, (int)nextSummit.Coordinate.Y];

            while (caseSuivante.Previous != null && caseSuivante.Previous.Coordinate != currentSummit.Coordinate)
            {
                caseSuivante = caseSuivante.Previous;
            }

            if (currentCoordinates.Y - caseSuivante.Coordinate.Y == -1)
                return Direction.Down;
            else if (currentCoordinates.Y - caseSuivante.Coordinate.Y == 1)
                return Direction.Up;
            else if (currentCoordinates.X - caseSuivante.Coordinate.X == 1)
                return Direction.Left;
            else if (currentCoordinates.X - caseSuivante.Coordinate.X == -1)
                return Direction.Right;
            else
                return Direction.None;
        }

        public Direction getDirectionTopLeft(Coordinates currentCoordinates)
        {
            return getDirection(currentCoordinates, topLeft);
        }

        public Direction getDirectionTopRight(Coordinates currentCoordinates)
        {
            return getDirection(currentCoordinates, topRight);
        }

        public Direction getDirectionBotLeft(Coordinates currentCoordinates)
        {
            return getDirection(currentCoordinates, botLeft);
        }

        public Direction getDirectionBotRight(Coordinates currentCoordinates)
        {
            return getDirection(currentCoordinates, botRight);
        }
    }
}

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

            i = 0; j = 0;
            while (summits[i, j] == null)
            {
                i++;
                if (i > Grid.GRID_WIDTH / 2)
                {
                    i = 0;
                    j++;
                }
            }
            topLeft = new Coordinates(i, j);

            i = Grid.GRID_WIDTH - 1; j = 0;
            while (summits[i, j] == null)
            {
                i--;
                if (i < Grid.GRID_WIDTH / 2)
                {
                    i = Grid.GRID_WIDTH - 1;
                    j++;
                }
            }
            topRight = new Coordinates(i, j);

            i = 0; j = Grid.GRID_HEIGHT - 1;
            while (summits[i, j] == null)
            {
                i++;
                if (i > Grid.GRID_WIDTH / 2)
                {
                    i = 0;
                    j--;
                }
            }
            botLeft = new Coordinates(i, j);

            i = Grid.GRID_WIDTH - 1; j = Grid.GRID_HEIGHT - 1;
            while (summits[i, j] == null)
            {
                i--;
                if (i < Grid.GRID_WIDTH / 2)
                {
                    i = Grid.GRID_WIDTH - 1;
                    j--;
                }
            }
            botRight = new Coordinates(i, j);
        }
        public Direction getDirection(Coordinates currentCoordinates, Coordinates targetedCoordinates)
        {
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

            Summit sommetCourant = summits[currentCoordinates.X, currentCoordinates.Y];
            sommetCourant.Potential = 0;
            sommetCourant.Mark = true;
            Summit sommetSuivant = sommetCourant;
            Summit sommetVise = summits[targetedCoordinates.X, targetedCoordinates.Y];

            while (sommetSuivant.Coordinate != sommetVise.Coordinate)
            {
                sommetSuivant.Mark = true;

                if (sommetSuivant.Coordinate.X != 0 && sommetSuivant.Coordinate.Y != 0
                    && sommetSuivant.Coordinate.X != Grid.GRID_WIDTH - 1 && sommetSuivant.Coordinate.Y != Grid.GRID_HEIGHT - 1)
                {
                    //Case du haut
                    if (!engine.isWallOnMap(sommetSuivant.Coordinate.X, sommetSuivant.Coordinate.Y - 1))
                    {
                        Summit s = summits[sommetSuivant.Coordinate.X, sommetSuivant.Coordinate.Y - 1];
                        if (s.Potential > sommetSuivant.Potential + 1)
                        {
                            summits[(int)sommetSuivant.Coordinate.X, sommetSuivant.Coordinate.Y - 1].Previous = sommetSuivant;
                            summits[(int)sommetSuivant.Coordinate.X, sommetSuivant.Coordinate.Y - 1].Potential = sommetSuivant.Potential + 1;
                        }
                    }
                    //Case du bas
                    if (!engine.isWallOnMap(sommetSuivant.Coordinate.X, sommetSuivant.Coordinate.Y + 1))
                    {
                        Summit s = summits[(int)sommetSuivant.Coordinate.X, (int)sommetSuivant.Coordinate.Y + 1];
                        if (s.Potential > sommetSuivant.Potential + 1)
                        {
                            summits[(int)sommetSuivant.Coordinate.X, (int)sommetSuivant.Coordinate.Y + 1].Previous = sommetSuivant;
                            summits[(int)sommetSuivant.Coordinate.X, (int)sommetSuivant.Coordinate.Y + 1].Potential = sommetSuivant.Potential + 1;
                        }
                    }
                    //Case de gauche
                    if (!engine.isWallOnMap(sommetSuivant.Coordinate.X - 1, sommetSuivant.Coordinate.Y))
                    {
                        Summit s = summits[(int)sommetSuivant.Coordinate.X - 1, (int)sommetSuivant.Coordinate.Y];
                        if (s.Potential > sommetSuivant.Potential + 1)
                        {
                            summits[(int)sommetSuivant.Coordinate.X - 1, (int)sommetSuivant.Coordinate.Y].Previous = sommetSuivant;
                            summits[(int)sommetSuivant.Coordinate.X - 1, (int)sommetSuivant.Coordinate.Y].Potential = sommetSuivant.Potential + 1;
                        }
                    }
                    //Case de droite
                    if (!engine.isWallOnMap(sommetSuivant.Coordinate.X + 1, sommetSuivant.Coordinate.Y))
                    {
                        Summit s = summits[(int)sommetSuivant.Coordinate.X + 1, (int)sommetSuivant.Coordinate.Y];
                        if (s.Potential > sommetSuivant.Potential + 1)
                        {
                            summits[(int)sommetSuivant.Coordinate.X + 1, (int)sommetSuivant.Coordinate.Y].Previous = sommetSuivant;
                            summits[(int)sommetSuivant.Coordinate.X + 1, (int)sommetSuivant.Coordinate.Y].Potential = sommetSuivant.Potential + 1;
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
                            sommetSuivant = summits[x, y];
                        }
                    }
                }
            }

            Summit caseSuivante = summits[(int)sommetSuivant.Coordinate.X, (int)sommetSuivant.Coordinate.Y];

            while (caseSuivante.Previous != null && caseSuivante.Previous.Coordinate != sommetCourant.Coordinate)
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

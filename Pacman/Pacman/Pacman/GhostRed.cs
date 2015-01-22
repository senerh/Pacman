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
    class GhostRed
    {
        //FIELDS
        Summit[,] summits;
        Engine engine;

        Rectangle hitbox;

        Grid grid;

        Direction direction;
        int frameLine;
        int frameColumn;
        int timer;

        const int SPEED = 2;
        const int ANIMATION_SPEED = 5;
        //CONSTRUCTOR
        public GhostRed(int x, int y, Engine engine, Summit[,] s)
        {
            hitbox = new Rectangle(x, y, Tile.TILE_WITDH, Tile.TILE_HEIGHT);
            direction = Direction.Right;
            timer = 0;
            this.engine = engine;
            this.summits = new Summit[Grid.GRID_HEIGHT, Grid.GRID_WIDTH];
            summits = s;
        }

        //METHODS
        private void Animate()
        {
            if (timer >= ANIMATION_SPEED)
            {
                timer = 0;
                frameColumn++;
                if (frameColumn == 3)
                    frameColumn = 1;
            }
            else
            {
                timer++;
            }
        }

        public Coordinate getGridPosition()
        {
            return new Coordinate(hitbox.X / Tile.TILE_WITDH, hitbox.Y / Tile.TILE_HEIGHT);
        }

        public virtual void changerDirection(Coordinate pacmanCoordinate)
        {
            Summit sommetCourant = summits[getGridPosition().X, getGridPosition().Y];
            sommetCourant.Potential = 0;
            sommetCourant.Mark = true;
            Summit sommetSuivant = sommetCourant;
            Summit sommetPacman = summits[pacmanCoordinate.X, pacmanCoordinate.Y];

            Summit sommetVise = summits[pacmanCoordinate.X, pacmanCoordinate.Y];

            while (sommetSuivant.Coordinate != sommetVise.Coordinate)
            {
                sommetSuivant.Mark = true;

                if (sommetSuivant.Coordinate.X != 0 && sommetSuivant.Coordinate.Y != 0
                    && sommetSuivant.Coordinate.X != Grid.GRID_WIDTH - 1 && sommetSuivant.Coordinate.Y != Grid.GRID_HEIGHT - 1)
                {
                    //Case du haut
                    if (!engine.isCollisionWall(new Rectangle(sommetSuivant.Coordinate.X * Tile.TILE_WITDH, (sommetSuivant.Coordinate.Y - 1) * Tile.TILE_HEIGHT, hitbox.Width, hitbox.Height)))
                    {
                        Summit s = summits[(int)sommetSuivant.Coordinate.X, (int)sommetSuivant.Coordinate.Y - 1];
                        if (s.Potential > sommetSuivant.Potential + 1)
                        {
                            summits[(int)sommetSuivant.Coordinate.X, (int)sommetSuivant.Coordinate.Y - 1].Previous = sommetSuivant;
                            summits[(int)sommetSuivant.Coordinate.X, (int)sommetSuivant.Coordinate.Y - 1].Potential = sommetSuivant.Potential + 1;
                        }
                    }
                    //Case du bas
                    if (!engine.isCollisionWall(new Rectangle(sommetSuivant.Coordinate.X * Tile.TILE_WITDH, (sommetSuivant.Coordinate.Y + 1) * Tile.TILE_HEIGHT, hitbox.Width, hitbox.Height)))
                    {
                        Summit s = summits[(int)sommetSuivant.Coordinate.X, (int)sommetSuivant.Coordinate.Y + 1];
                        if (s.Potential > sommetSuivant.Potential + 1)
                        {
                            summits[(int)sommetSuivant.Coordinate.X, (int)sommetSuivant.Coordinate.Y + 1].Previous = sommetSuivant;
                            summits[(int)sommetSuivant.Coordinate.X, (int)sommetSuivant.Coordinate.Y + 1].Potential = sommetSuivant.Potential + 1;
                        }
                    }
                    //Case de gauche
                    if (!engine.isCollisionWall(new Rectangle((sommetSuivant.Coordinate.X - 1) * Tile.TILE_WITDH, sommetSuivant.Coordinate.Y * Tile.TILE_HEIGHT, hitbox.Width, hitbox.Height)))
                    {
                        Summit s = summits[(int)sommetSuivant.Coordinate.X - 1, (int)sommetSuivant.Coordinate.Y];
                        if (s.Potential > sommetSuivant.Potential + 1)
                        {
                            summits[(int)sommetSuivant.Coordinate.X - 1, (int)sommetSuivant.Coordinate.Y].Previous = sommetSuivant;
                            summits[(int)sommetSuivant.Coordinate.X - 1, (int)sommetSuivant.Coordinate.Y].Potential = sommetSuivant.Potential + 1;
                        }
                    }
                    //Case de droite
                    if (!engine.isCollisionWall(new Rectangle((sommetSuivant.Coordinate.X + 1) * Tile.TILE_WITDH, sommetSuivant.Coordinate.Y * Tile.TILE_HEIGHT, hitbox.Width, hitbox.Height)))
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

            if (getGridPosition().Y - caseSuivante.Coordinate.Y == -1)
                direction = Direction.Down;
            else if (getGridPosition().Y - caseSuivante.Coordinate.Y == 1)
                direction = Direction.Up;
            else if (getGridPosition().X - caseSuivante.Coordinate.X == 1)
                direction = Direction.Left;
            else if (getGridPosition().X - caseSuivante.Coordinate.X == -1)
                direction = Direction.Right;
            else
                direction = Direction.None;

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
        }

        private void moveOnUp()
        {
            hitbox = engine.translateY(hitbox, -SPEED);
            direction = Direction.Up;
            Animate();
        }
        private void moveOnDown()
        {
            hitbox = engine.translateY(hitbox, SPEED);
            direction = Direction.Down;
            Animate();
        }
        private void moveOnRight()
        {
            hitbox = engine.translateX(hitbox, SPEED);
            direction = Direction.Right;
            Animate();
        }
        private void moveOnLeft()
        {
            hitbox = engine.translateX(hitbox, -SPEED);
            direction = Direction.Left;
            Animate();
        }

        //UPDATE & DRAW
        public void Update(Coordinate pacmanCoordinate)
        {
            if (hitbox.X % Tile.TILE_WITDH != 0)
            {
                if (direction == Direction.Left)
                    moveOnLeft();
                else
                    moveOnRight();
            }
            else if (hitbox.Y % Tile.TILE_HEIGHT != 0)
            {
                if (direction == Direction.Up)
                    moveOnUp();
                else
                    moveOnDown();
            }
            else
            {
                changerDirection(pacmanCoordinate);
                switch (direction)
                {
                    case Direction.Down:
                        moveOnDown();
                        break;
                    case Direction.Up:
                        moveOnUp();
                        break;
                    case Direction.Left:
                        moveOnLeft();
                        break;
                    case Direction.Right:
                        moveOnRight();
                        break;
                    case Direction.None:
                        break;
                }
            }

            switch (direction)
            {
                case Direction.Down:
                    frameLine = 1;
                    break;
                case Direction.Up:
                    frameLine = 2;
                    break;
                case Direction.Left:
                    frameLine = 3;
                    break;
                case Direction.Right:
                    frameLine = 4;
                    break;
                case Direction.None:
                    break;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Resources.ghostRed, hitbox,
                new Rectangle((frameColumn-1)*hitbox.Width, (frameLine-1)*hitbox.Height, hitbox.Width, hitbox.Height),
                Color.White);
        }
    }
}

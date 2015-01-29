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
        Dijkstra dijkstra;
        Engine engine;

        Rectangle hitbox;

        Direction direction;
        int frameLine;
        int frameColumn;
        int timer;
        int t_wait;

        const int SPEED = 2;
        const int ANIMATION_SPEED = 5;
        const int TIME_TO_WAIT = 5*60;

        //CONSTRUCTOR
        public GhostRed(int x, int y, Engine engine)
        {
            this.engine = engine;
            hitbox = new Rectangle(x, y, Tile.TILE_WITDH, Tile.TILE_HEIGHT);
            dijkstra = new Dijkstra(engine);

            timer = 0;
            t_wait = 0;
            direction = Direction.Right;
            frameLine = 1;
            frameColumn = 2;
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

        public Coordinates getGridPosition()
        {
            return new Coordinates(hitbox.X / Tile.TILE_WITDH, hitbox.Y / Tile.TILE_HEIGHT);
        }

        public Rectangle getHitbox()
        {
            return hitbox;
        }

        private void moveOnUp()
        {
            hitbox = engine.translateY(hitbox, -SPEED, true, false);
            direction = Direction.Up;
            Animate();
        }
        private void moveOnDown()
        {
            hitbox = engine.translateY(hitbox, SPEED, true, false);
            direction = Direction.Down;
            Animate();
        }
        private void moveOnRight()
        {
            hitbox = engine.translateX(hitbox, SPEED, true, false);
            direction = Direction.Right;
            Animate();
        }
        private void moveOnLeft()
        {
            hitbox = engine.translateX(hitbox, -SPEED, true, false);
            direction = Direction.Left;
            Animate();
        }

        //UPDATE & DRAW
        public void Update(Coordinates pacmanCoordinates)
        {
            if (t_wait < TIME_TO_WAIT)
            {
                t_wait++;
            }
            else
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
                    direction = dijkstra.getDirection(getGridPosition(), pacmanCoordinates);
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Resources.ghostRed, hitbox,
                new Rectangle((frameColumn-1)*hitbox.Width, (frameLine-1)*hitbox.Height, hitbox.Width, hitbox.Height),
                Color.White);
        }
    }
}

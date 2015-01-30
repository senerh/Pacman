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
    abstract class Ghost
    {
        //CONSTANTS
        private int SPEED;
        private int ANIMATION_SPEED;
        private int TIME_TO_WAIT;
        private int TIME_VULNERABLE;
        private Texture2D ghost;

        //FIELDS
        protected Dijkstra dijkstra;
        protected Engine engine;

        protected Rectangle hitbox;
        protected Coordinates initialCoordinates;

        protected Direction direction;
        protected int frameLine;
        protected int frameColumn;
        protected int timer;
        protected int t_wait;
        protected int t_vulnerable;

        protected bool dead;

        //CONSTRUCTOR
        public Ghost(int x, int y, Engine engine, Texture2D texture, int speed, int animation_speed, int time_to_wait, int time_vulnerable)
        {
            this.engine = engine;
            hitbox = new Rectangle(x, y, Tile.TILE_WITDH, Tile.TILE_HEIGHT);
            dijkstra = new Dijkstra(engine);
            initialCoordinates = new Coordinates(x / Tile.TILE_WITDH, y / Tile.TILE_HEIGHT);

            timer = 0;
            t_wait = 0;
            t_vulnerable = 0;
            direction = Direction.Up;
            frameLine = 1;
            frameColumn = 2;
            dead = false;

            ghost = texture;
            SPEED = speed;
            ANIMATION_SPEED = animation_speed;
            TIME_TO_WAIT = time_to_wait;
            TIME_VULNERABLE = time_vulnerable;
        }

        //METHODS
        public void init()
        {
            hitbox.X = initialCoordinates.X * Tile.TILE_WITDH;
            hitbox.Y = initialCoordinates.Y * Tile.TILE_HEIGHT;
            timer = 0;
            t_wait = 0;
            t_vulnerable = 0;
            direction = Direction.Up;
            frameLine = 1;
            frameColumn = 2;
            dead = false;
        }
        public Coordinates getGridPosition()
        {
            return new Coordinates(hitbox.X / Tile.TILE_WITDH, hitbox.Y / Tile.TILE_HEIGHT);
        }

        public Rectangle getHitbox()
        {
            return hitbox;
        }

        public bool isDead()
        {
            return dead;
        }

        public bool isVulnerable()
        {
            return (t_vulnerable > 0);
        }

        public void die()
        {
            dead = true;
        }

        public void setVulnerable()
        {
            t_vulnerable = TIME_VULNERABLE;
        }

        protected void Animate()
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

        protected void moveOnUp()
        {
            hitbox = engine.translateY(hitbox, -SPEED, true, false);
            direction = Direction.Up;
            Animate();
        }
        protected void moveOnDown()
        {
            hitbox = engine.translateY(hitbox, SPEED, true, false);
            direction = Direction.Down;
            Animate();
        }
        protected void moveOnRight()
        {
            hitbox = engine.translateX(hitbox, SPEED, true, false);
            direction = Direction.Right;
            Animate();
        }
        protected void moveOnLeft()
        {
            hitbox = engine.translateX(hitbox, -SPEED, true, false);
            direction = Direction.Left;
            Animate();
        }

        //CONDUCT
        protected void waiting()
        {
            t_wait++;
            if (hitbox.Y % Tile.TILE_HEIGHT != 0)
            {
                if (direction == Direction.Up)
                    moveOnUp();
                else
                    moveOnDown();
            }
            else
            {
                if (direction == Direction.Up)
                {
                    if (getGridPosition().Y > initialCoordinates.Y - 1)
                    {
                        moveOnUp();
                    }
                    else
                    {
                        moveOnDown();
                    }
                }
                else
                {
                    if (getGridPosition().Y < initialCoordinates.Y + 1)
                    {
                        moveOnDown();
                    }
                    else
                    {
                        moveOnUp();
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

        protected void dying()
        {
            if (getGridPosition() == initialCoordinates)
            {
                dead = false;
                t_vulnerable = 0;
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
                    direction = dijkstra.getDirection(getGridPosition(), initialCoordinates);
                    switch (direction)
                    {
                        case Direction.Down:
                            frameLine = 1;
                            moveOnDown();
                            break;
                        case Direction.Up:
                            frameLine = 2;
                            moveOnUp();
                            break;
                        case Direction.Left:
                            frameLine = 3;
                            moveOnLeft();
                            break;
                        case Direction.Right:
                            frameLine = 4;
                            moveOnRight();
                            break;
                        case Direction.None:
                            break;
                    }
                }
            }
        }

        protected void fleeting(Coordinates pacmanCoordinates)
        {
            t_vulnerable--;
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
                if (pacmanCoordinates.X < Grid.GRID_WIDTH / 2)
                {
                    if (pacmanCoordinates.Y < Grid.GRID_HEIGHT / 2)
                    {
                        direction = dijkstra.getDirectionBotRight(getGridPosition());
                    }
                    else
                    {
                        direction = dijkstra.getDirectionTopRight(getGridPosition());
                    }
                }
                else
                {
                    if (pacmanCoordinates.Y < Grid.GRID_HEIGHT / 2)
                    {
                        direction = dijkstra.getDirectionBotLeft(getGridPosition());
                    }
                    else
                    {
                        direction = dijkstra.getDirectionTopLeft(getGridPosition());
                    }
                }

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

            if (t_vulnerable <= 60)
            {
                if (t_vulnerable % 10 == 0)
                    if (frameLine == 2)
                        frameLine = 1;
                    else
                        frameLine = 2;
            }
            else
            {
                frameLine = 1;
            }
        }

        protected void hunting(Coordinates pacmanCoordinates)
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
                direction = follow(pacmanCoordinates);
                switch (direction)
                {
                    case Direction.Down:
                        frameLine = 1;
                        moveOnDown();
                        break;
                    case Direction.Up:
                        frameLine = 2;
                        moveOnUp();
                        break;
                    case Direction.Left:
                        frameLine = 3;
                        moveOnLeft();
                        break;
                    case Direction.Right:
                        frameLine = 4;
                        moveOnRight();
                        break;
                    case Direction.None:
                        break;
                }
            }
        }

        protected abstract Direction follow(Coordinates pacmanCoordinates);

        //UPDATE & DRAW
        public void Update(Coordinates pacmanCoordinates)
        {
            if (t_wait < TIME_TO_WAIT)
            {
                waiting();
            }
            else if (dead)
            {
                dying();
            }
            else if (isVulnerable())
            {
                fleeting(pacmanCoordinates);
            }
            else
            {
                hunting(pacmanCoordinates);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isDead())
            {
                spriteBatch.Draw(Resources.ghostDead, hitbox,
                new Rectangle(0, (frameLine - 1) * hitbox.Height, hitbox.Width, hitbox.Height),
                Color.White);
            }
            else if (isVulnerable())
            {
                spriteBatch.Draw(Resources.ghostVulnerable, hitbox,
                new Rectangle((frameColumn - 1) * hitbox.Width, (frameLine - 1) * hitbox.Height, hitbox.Width, hitbox.Height),
                Color.White);
            }
            else
            {
                spriteBatch.Draw(ghost, hitbox,
                new Rectangle((frameColumn - 1) * hitbox.Width, (frameLine - 1) * hitbox.Height, hitbox.Width, hitbox.Height),
                Color.White);
            }
        }
    }
}

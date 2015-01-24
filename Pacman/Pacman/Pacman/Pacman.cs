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
    public enum Direction
    {
        Up, Down, Left, Right, None
    };
    class Pacman
    {
        //FIELDS
        bool vulnerable;
        bool dead;

        Rectangle hitbox;

        Engine engine;

        Direction direction;
        int frameLine;
        int frameColumn;
        int timer;
        int t_invulnerable;

        const int SPEED = 2;
        const int ANIMATION_SPEED = 3;
        const int T_INVULNERABLE = 5 * 60;
        const int NB_FRAMES_DEATH = 12;
        //CONSTRUCTOR
        public Pacman(int x, int y, Engine engine)
        {
            this.engine = engine;
            hitbox = new Rectangle(x, y, Tile.TILE_WITDH, Tile.TILE_HEIGHT);
            direction = Direction.Right;
            timer = 0;
            vulnerable = true;
            dead = false;
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

        private void AnimateDeath()
        {
            if (timer >= ANIMATION_SPEED)
            {
                timer = 0;
                if (frameColumn < NB_FRAMES_DEATH)
                    frameColumn++;
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
        public bool isVulnerable()
        {
            return vulnerable;
        }
        public void setInvulnerable()
        {
            t_invulnerable = T_INVULNERABLE;
            vulnerable = false;
        }
        public bool isDying()
        {
            if (dead)
            {
                if (frameColumn == NB_FRAMES_DEATH)
                {
                    if (Resources.pacmanDeathSound.State == SoundState.Stopped)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool isDead()
        {
            return dead;
        }
        public void die()
        {
            dead = true;
            frameColumn = 1;
            Resources.pacmanDeathSound.Play();
        }
        public void eat()
        {
            if (Resources.eatBean.State == SoundState.Stopped)
                Resources.eatBean.Play();
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
        public void Update(MouseState mouse, KeyboardState keyboard)
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
            else if (keyboard.IsKeyDown(Keys.Up))
            {
                moveOnUp();
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                moveOnDown();
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                moveOnRight();
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                moveOnLeft();
            }
            else
            {
                    frameColumn = 2;
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

            if (!vulnerable)
            {
                t_invulnerable--;
                if (t_invulnerable == 0)
                    vulnerable = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (dead)
            {
                spriteBatch.Draw(Resources.pacmanDeath, hitbox,
                new Rectangle((frameColumn - 1) * hitbox.Width, 0, hitbox.Width, hitbox.Height),
                Color.White);
                AnimateDeath();
            }
            else
            {
                spriteBatch.Draw(Resources.pacman, hitbox,
                new Rectangle((frameColumn - 1) * hitbox.Width, (frameLine - 1) * hitbox.Height, hitbox.Width, hitbox.Height),
                Color.White);
            }
        }
    }
}

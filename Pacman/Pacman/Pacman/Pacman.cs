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
        Rectangle hitbox;

        Engine engine;

        Direction direction;
        int frameLine;
        int frameColumn;
        int timer;

        const int SPEED = 2;
        const int ANIMATION_SPEED = 3;
        //CONSTRUCTOR
        public Pacman(int x, int y, Engine engine)
        {
            hitbox = new Rectangle(x, y, Tile.TILE_WITDH, Tile.TILE_HEIGHT);
            direction = Direction.Right;
            timer = 0;
            this.engine = engine;
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
        private void moveOnUp()
        {
            hitbox = engine.translateY(hitbox, -SPEED);
            direction = Direction.Up;
            Animate();
            if (engine.isCollisionBean(hitbox))
            {
                if (Resources.eatBean.State == SoundState.Stopped)
                    Resources.eatBean.Play();
            }
        }
        private void moveOnDown()
        {
            hitbox = engine.translateY(hitbox, SPEED);
            direction = Direction.Down;
            Animate();
            if (engine.isCollisionBean(hitbox))
            {
                if (Resources.eatBean.State == SoundState.Stopped)
                    Resources.eatBean.Play();
            }
        }
        private void moveOnRight()
        {
            hitbox = engine.translateX(hitbox, SPEED);
            direction = Direction.Right;
            Animate();
            if (engine.isCollisionBean(hitbox))
            {
                if (Resources.eatBean.State == SoundState.Stopped)
                    Resources.eatBean.Play();
            }
        }
        private void moveOnLeft()
        {
            hitbox = engine.translateX(hitbox, -SPEED);
            direction = Direction.Left;
            Animate();
            if (engine.isCollisionBean(hitbox))
            {
                if (Resources.eatBean.State == SoundState.Stopped)
                    Resources.eatBean.Play();
            }
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

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Resources.pacman, hitbox,
                new Rectangle((frameColumn-1)*hitbox.Width, (frameLine-1)*hitbox.Height, hitbox.Width, hitbox.Height),
                Color.White);
        }
    }
}

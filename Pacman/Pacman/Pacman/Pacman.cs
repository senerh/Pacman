using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public enum Direction
    {
        Up, Down, Left, Right
    };
    class Pacman
    {
        //FIELDS
        Rectangle hitbox;

        Direction direction;
        int frameLine;
        int frameColumn;
        int timer;

        const int SPEED = 2;
        const int ANIMATION_SPEED = 5;
        //CONSTRUCTOR
        public Pacman(int x, int y)
        {
            hitbox = new Rectangle(x, y, Tile.TILE_WITDH, Tile.TILE_HEIGHT);
            direction = Direction.Right;
            timer = 0;
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
        private void moveOnUp(Grid grid)
        {
            hitbox.Y = hitbox.Y - SPEED;
            direction = Direction.Up;
            Animate();
            while (grid.isCollisionWall(hitbox) != null)
            {
                hitbox.Y++;
            }
            Bean b = grid.isCollisionBean(hitbox);
            if (b != null)
            {
                grid.removeBean(b);
            }
        }
        private void moveOnDown(Grid grid)
        {
            hitbox.Y = hitbox.Y + SPEED;
            direction = Direction.Down;
            Animate();
            while (grid.isCollisionWall(hitbox) != null)
            {
                hitbox.Y--;
            }
            Bean b = grid.isCollisionBean(hitbox);
            if (b != null)
            {
                grid.removeBean(b);
            }
        }
        private void moveOnRight(Grid grid)
        {
            hitbox.X = hitbox.X + SPEED;
            direction = Direction.Right;
            Animate();
            while (grid.isCollisionWall(hitbox) != null)
            {
                hitbox.X--;
            }
            Bean b = grid.isCollisionBean(hitbox);
            if (b != null)
            {
                grid.removeBean(b);
            }
        }
        private void moveOnLeft(Grid grid)
        {
            hitbox.X = hitbox.X - SPEED;
            direction = Direction.Left;
            Animate();
            while (grid.isCollisionWall(hitbox) != null)
            {
                hitbox.X++;
            }
            Bean b = grid.isCollisionBean(hitbox);
            if (b != null)
            {
                grid.removeBean(b);
            }
        }

        //UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard, Grid grid)
        {
            if (hitbox.X % Tile.TILE_WITDH != 0)
            {
                if (direction == Direction.Left)
                    moveOnLeft(grid);
                else
                    moveOnRight(grid);
            }
            else if (hitbox.Y % Tile.TILE_HEIGHT != 0)
            {
                if (direction == Direction.Up)
                    moveOnUp(grid);
                else
                    moveOnDown(grid);
            }
            else if (keyboard.IsKeyDown(Keys.Up))
            {
                moveOnUp(grid);
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                moveOnDown(grid);
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                moveOnRight(grid);
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                moveOnLeft(grid);
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

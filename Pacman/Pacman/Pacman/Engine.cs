using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Engine
    {
        Grid grid;

        public Engine(Grid grid)
        {
            this.grid = grid;
        }

        public bool isCollision(Rectangle hitbox1, Rectangle hitbox2)
        {
            return hitbox1.Intersects(hitbox2);
        }

        public Rectangle translateX(Rectangle hitbox, int v, bool checkWall, bool checkHouse)
        {
            hitbox.X = hitbox.X + v;

            if (checkWall || checkHouse)
            {
                int n;

                if (v < 0)
                    n = 1;
                else
                    n = -1;

                if (checkWall)
                {
                    while (isCollisionWall(hitbox))
                        hitbox.X = hitbox.X + n;
                }

                if (checkHouse)
                {
                    while (isCollisionHouse(hitbox))
                        hitbox.X = hitbox.X + n;
                }
            }

            if (hitbox.X >= Grid.GRID_WIDTH * Tile.TILE_WITDH)
            {
                hitbox.X = 0;
            }
            else if (hitbox.X + hitbox.Width <= 0)
            {
                hitbox.X = (Grid.GRID_WIDTH - 1) * Tile.TILE_WITDH;
            }

            return hitbox;
        }

        public Rectangle translateY(Rectangle hitbox, int v, bool checkWall, bool checkHouse)
        {
            hitbox.Y = hitbox.Y + v;

            if (checkWall || checkHouse)
            {
                int n;

                if (v < 0)
                    n = 1;
                else
                    n = -1;

                if (checkWall)
                {
                    while (isCollisionWall(hitbox))
                        hitbox.Y = hitbox.Y + n;
                }

                if (checkHouse)
                {
                    while (isCollisionHouse(hitbox))
                        hitbox.Y = hitbox.Y + n;
                }
            }

            if (hitbox.Y >= Grid.GRID_HEIGHT * Tile.TILE_HEIGHT)
            {
                hitbox.Y = 0;
            }
            else if (hitbox.Y + hitbox.Height <= 0)
            {
                hitbox.Y = (Grid.GRID_HEIGHT - 1) * Tile.TILE_HEIGHT;
            }
            return hitbox;
        }

        public bool isCollisionWall(Rectangle hitbox)
        {
            foreach(Wall w in grid.getListWall())
            {
                if (isCollision(w.getHitbox(), hitbox))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isCollisionHouse(Rectangle hitbox)
        {
            foreach (House h in grid.getListHouse())
            {
                if (isCollision(h.getHitbox(), hitbox))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isWallOnMap(int x, int y)
        {
            byte[,] map = grid.getMap();
            return (map[x, y] == Grid.WALL);
        }

        public bool isCollisionBean(Rectangle hitbox)
        {
            foreach(Bean b in grid.getListBean())
            {
                if (isCollision(b.getHitbox(), hitbox))
                {
                    grid.removeBean(b);
                    return true;
                }
            }
            return false;
        }

        public bool isCollisionSuperBean(Rectangle hitbox)
        {
            foreach (SuperBean b in grid.getListSuperBean())
            {
                if (isCollision(b.getHitbox(), hitbox))
                {
                    grid.removeSuperBean(b);
                    return true;
                }
            }
            return false;
        }

        public Coordinates getRandomBeanCoordinates()
        {
            int i = new Random().Next(grid.getListBean().Count());
            return grid.getListBean()[i].getCoordinatesOnMap();
        }
    }
}

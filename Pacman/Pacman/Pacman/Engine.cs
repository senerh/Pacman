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
        List<Wall> listWall;
        List<Bean> listBean;
        public Engine(Grid grid)
        {
            this.grid = grid;
            this.listWall = grid.getListWall();
            this.listBean = grid.getListBean();
        }

        public bool isCollision(Rectangle hitbox1, Rectangle hitbox2)
        {
            return hitbox1.Intersects(hitbox2);
        }

        public Rectangle translateX(Rectangle hitbox, int v)
        {
            int n;

            hitbox.X = hitbox.X + v;

            if (v < 0)
                n = 1;
            else
                n = -1;
            while (isCollisionWall(hitbox))
                hitbox.X = hitbox.X + n;

            return hitbox;
        }

        public Rectangle translateY(Rectangle hitbox, int v)
        {
            int n;

            hitbox.Y = hitbox.Y + v;

            if (v < 0)
                n = 1;
            else
                n = -1;
            while (isCollisionWall(hitbox))
                hitbox.Y = hitbox.Y + n;

            return hitbox;
        }

        public bool isCollisionWall(Rectangle hitbox)
        {
            foreach(Wall w in listWall)
            {
                if (isCollision(w.getHitbox(), hitbox))
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
            foreach(Bean b in listBean)
            {
                if (isCollision(b.getHitbox(), hitbox))
                {
                    grid.removeBean(b);
                    return true;
                }
            }
            return false;
        }
    }
}

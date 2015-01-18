using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Grid
    {
        //CONSTANTS
        public const byte WALL = 0;
        public const byte BEAN = 1;
        public const byte EMPTY = 2;
        public const byte PACMAN = 3;
        public const int GRID_WIDTH = 28;
        public const int GRID_HEIGHT = 31;

        //FIELDS
        List<Bean> listBean;
        List<Wall> listWall;

        //CONSTRUCTOR
        public Grid(byte[,] map)
        {
            listBean = new List<Bean>();
            listWall = new List<Wall>();

            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                for (int x = 0; x < GRID_WIDTH; x++)
                {
                    switch(map[y, x])
                    {
                        case WALL:
                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT));
                            break;
                        case BEAN:
                            listBean.Add(new Bean(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        //METHODS
        public void removeBean(Bean b)
        {
            listBean.Remove(b);
        }

        public Wall isCollisionWall(Rectangle hitbox)
        {
            foreach (Wall w in listWall)
            {
                if (w.getHitbox().Intersects(hitbox))
                    return w;
            }
            return null;
        }

        public Bean isCollisionBean(Rectangle hitbox)
        {
            foreach (Bean b in listBean)
            {
                if (b.getHitbox().Intersects(hitbox))
                    return b;
            }
            return null;
        }

        public bool isFinished()
        {
            return (listBean.Count == 0);
        }
        //UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Wall w in listWall)
            {
                w.Draw(spriteBatch);
            }
            foreach(Bean b in listBean)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}

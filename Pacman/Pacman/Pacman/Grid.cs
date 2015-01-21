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
        public const byte GHOST_RED = 4;


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
                            if (y>0 && map[y-1, x] == WALL)//mur en haut
                            {
                                if (y+1<GRID_HEIGHT && map[y+1, x] == WALL)//mur en bas
                                {
                                    if (x>0 && map[y, x-1] == WALL)//mur a gauche
                                    {
                                        if (x+1<GRID_WIDTH && map[y, x+1] == WALL)//mur a droite
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallMid));
                                        }
                                        else
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallRight));
                                        }
                                    }
                                    else
                                    {
                                        if (x+1 < GRID_WIDTH && map[y, x + 1] == WALL)//mur a droite
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallLeft));
                                        }
                                        else
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallMidXV));
                                        }
                                    }
                                }
                                else
                                {
                                    if (x > 0 && map[y, x - 1] == WALL)//mur a gauche
                                    {
                                        if (x+1 < GRID_WIDTH && map[y, x + 1] == WALL)//mur a droite
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallBot));
                                        }
                                        else
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallBotRight));
                                        }
                                    }
                                    else
                                    {
                                        if (x+1 < GRID_WIDTH && map[y, x + 1] == WALL)//mur a droite
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallBotLeft));
                                        }
                                        else
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallBotX));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (y+1 < GRID_HEIGHT && map[y + 1, x] == WALL)//mur en bas
                                {
                                    if (x > 0 && map[y, x - 1] == WALL)//mur a gauche
                                    {
                                        if (x+1 < GRID_WIDTH && map[y, x + 1] == WALL)//mur a droite
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallTop));
                                        }
                                        else
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallTopRight));
                                        }
                                    }
                                    else
                                    {
                                        if (x+1 < GRID_WIDTH && map[y, x + 1] == WALL)//mur a droite
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallTopLeft));
                                        }
                                        else
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallTopX));
                                        }
                                    }
                                }
                                else
                                {
                                    if (x > 0 && map[y, x - 1] == WALL)//mur a gauche
                                    {
                                        if (x+1 < GRID_WIDTH && map[y, x + 1] == WALL)//mur a droite
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallMidXH));
                                        }
                                        else
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallRightX));
                                        }
                                    }
                                    else
                                    {
                                        if (x+1 < GRID_WIDTH && map[y, x + 1] == WALL)//mur a droite
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallLeftX));
                                        }
                                        else
                                        {
                                            listWall.Add(new Wall(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT, Resources.wallMidX));
                                        }
                                    }
                                }
                            }
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

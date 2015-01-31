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
        public const byte GHOST_BLUE = 4;
        public const byte GHOST_ORANGE = 5;
        public const byte GHOST_PINK = 6;
        public const byte GHOST_RED = 7;
        public const byte HOUSE = 8;
        public const byte SUPER_BEAN = 9;
        public const byte BONUS = 10;

        public const int GRID_WIDTH = 28;
        public const int GRID_HEIGHT = 31;

        public const int TIME_BONUS_SPAWN = 15 * 60;

        //FIELDS
        List<Bean> listBean;
        List<Wall> listWall;
        List<House> listHouse;
        List<SuperBean> listSuperBean;
        Bonus bonus;
        byte[,] map;

        int timeBonusSpawn;

        //CONSTRUCTOR
        public Grid(byte[,] map)
        {
            this.map = map;
            listBean = new List<Bean>();
            listWall = new List<Wall>();
            listHouse = new List<House>();
            listSuperBean = new List<SuperBean>();
            timeBonusSpawn = TIME_BONUS_SPAWN;

            for (int x = 0; x < GRID_WIDTH; x++)
            {
                for (int y = 0; y < GRID_HEIGHT; y++)
                {
                    switch(map[x, y])
                    {
                        case WALL:
                            if (y > 0 && map[x, y - 1] == WALL)//mur en haut
                            {
                                if (y < GRID_HEIGHT-1 && map[x, y + 1] == WALL)//mur en bas
                                {
                                    if (x > 0 && map[x-1, y] == WALL)//mur a gauche
                                    {
                                        if (x + 1 < GRID_WIDTH && map[x + 1, y] == WALL)//mur a droite
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
                                        if (x + 1 < GRID_WIDTH && map[x + 1, y] == WALL)//mur a droite
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
                                    if (x > 0 && map[x - 1, y] == WALL)//mur a gauche
                                    {
                                        if (x + 1 < GRID_WIDTH && map[x + 1, y] == WALL)//mur a droite
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
                                        if (x + 1 < GRID_WIDTH && map[x + 1, y] == WALL)//mur a droite
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
                                if (y + 1 < GRID_HEIGHT && map[x, y + 1] == WALL)//mur en bas
                                {
                                    if (x > 0 && map[x - 1, y] == WALL)//mur a gauche
                                    {
                                        if (x + 1 < GRID_WIDTH && map[x + 1, y] == WALL)//mur a droite
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
                                        if (x + 1 < GRID_WIDTH && map[x + 1, y] == WALL)//mur a droite
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
                                    if (x > 0 && map[x - 1, y] == WALL)//mur a gauche
                                    {
                                        if (x + 1 < GRID_WIDTH && map[x + 1, y] == WALL)//mur a droite
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
                                        if (x + 1 < GRID_WIDTH && map[x + 1, y] == WALL)//mur a droite
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
                        case SUPER_BEAN:
                            listSuperBean.Add(new SuperBean(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT));
                            break;
                        case HOUSE:
                            listHouse.Add(new House(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT));
                            break;
                        case BONUS:
                            bonus = new Bonus(x * Tile.TILE_WITDH, y * Tile.TILE_HEIGHT);
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

        public void removeSuperBean(SuperBean b)
        {
            listSuperBean.Remove(b);
        }

        public List<Wall> getListWall()
        {
            return listWall;
        }
        public List<Bean> getListBean()
        {
            return listBean;
        }

        public List<House> getListHouse()
        {
            return listHouse;
        }

        public List<SuperBean> getListSuperBean()
        {
            return listSuperBean;
        }

        public byte[,] getMap()
        {
            return map;
        }

        public Bonus getBonus()
        {
            return bonus;
        }

        public bool isFinished()
        {
            return (listBean.Count == 0 && listSuperBean.Count == 0);
        }

        public Coordinates getCoordinatesOnMap(byte element)
        {
            for (int x = 0; x < Grid.GRID_WIDTH; x++)
            {
                for (int y = 0; y < Grid.GRID_HEIGHT; y++)
                {
                    if (map[x, y] == element)
                        return new Coordinates(x, y);
                }
            }
            return null;
        }

        //UPDATE & DRAW

        public void Update()
        {
            timeBonusSpawn--;
            if (timeBonusSpawn < 0)
            {
                timeBonusSpawn = TIME_BONUS_SPAWN;
                bonus.addBonus();
            }
            bonus.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Wall w in listWall)
            {
                w.Draw(spriteBatch);
            }
            foreach (House h in listHouse)
            {
                h.Draw(spriteBatch);
            }
            foreach(Bean b in listBean)
            {
                b.Draw(spriteBatch);
            }
            foreach(SuperBean b in listSuperBean)
            {
                b.Draw(spriteBatch);
            }
            bonus.Draw(spriteBatch);
        }
    }
}

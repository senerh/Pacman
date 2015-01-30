using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pacman
{
    class GameMain
    {
        //FIELDS
        Pacman pacman;
        Grid grid;
        Engine engine;
        Life lives;
        Score score;
        GameOver gameOver;
        List<Ghost> listGhost;
        Ghost ghost;
        bool startNewLevel;
        bool finished;

        //CONSTRUCTOR
        public GameMain()
        {
            lives = new Life();
            score = new Score();
            gameOver = null;
            finished = false;
            loadNextLevel();
        }

        //METHODS
        private void loadNextLevel()
        {
            byte[,] m = new byte[Grid.GRID_HEIGHT, Grid.GRID_WIDTH]
            {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 9, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 8, 8, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 4, 5, 6, 7, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 9, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            };

            byte[,] map = new byte[Grid.GRID_WIDTH, Grid.GRID_HEIGHT];

            for (int j = 0; j < Grid.GRID_HEIGHT; j++)
            {
                for (int i = 0; i< Grid.GRID_WIDTH; i++)
                {
                    map[i, j] = m[j, i];
                }
            }
            
            grid = new Grid(map);

            engine = new Engine(grid);

            Coordinates p = grid.getPositionOnMap(Grid.PACMAN);
            pacman = new Pacman(p.X * Tile.TILE_WITDH, p.Y * Tile.TILE_HEIGHT, engine);

            listGhost = new List<Ghost>();

            p = grid.getPositionOnMap(Grid.GHOST_RED);
            listGhost.Add(new GhostRed(p.X * Tile.TILE_WITDH, p.Y * Tile.TILE_HEIGHT, engine));
            p = grid.getPositionOnMap(Grid.GHOST_ORANGE);
            listGhost.Add(new GhostOrange(p.X * Tile.TILE_WITDH, p.Y * Tile.TILE_HEIGHT, engine));
            p = grid.getPositionOnMap(Grid.GHOST_PINK);
            listGhost.Add(new GhostPink(p.X * Tile.TILE_WITDH, p.Y * Tile.TILE_HEIGHT, engine));
            p = grid.getPositionOnMap(Grid.GHOST_BLUE);
            listGhost.Add(new GhostBlue(p.X * Tile.TILE_WITDH, p.Y * Tile.TILE_HEIGHT, engine));

            startNewLevel = true;
        }

        public bool isFinished()
        {
            return finished;
        }

        private Ghost collidedGhost()
        {
            foreach(Ghost g in listGhost)
            {
                if (engine.isCollision(g.getHitbox(), pacman.getHitbox()))
                {
                    return g;
                }
            }
            return null;
        }

        //UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            if (gameOver != null)
            {
                gameOver.Update(mouse, keyboard);
                if (gameOver.isFinished())
                {
                    finished = true;
                }
            }
            else if (Resources.beginningSound.State == SoundState.Stopped)
            {
                if (grid.isFinished())
                {
                    //niveau terminé
                    loadNextLevel();
                }
                else if ((ghost = collidedGhost()) != null)//s'il y a une collision entre fantome et pacman
                {
                    if (ghost.isVulnerable() && !ghost.isDead())//si le fantôme est vulnérable
                    {
                        ghost.die();
                    }
                    else if (!ghost.isVulnerable() && !ghost.isDead())//si le fantôme n'est pas vulnérable
                    {
                        if (pacman.isDead())
                        {
                            if (!pacman.isDying())//si le pacman a fini de mourrir
                            {
                                lives.lose();
                                if (lives.remainingLives() == 0)//si le pacman n'a plus de vies
                                {
                                    if (gameOver == null)
                                        gameOver = new GameOver(score);
                                }
                                else//s'il reste des vies au pacman
                                {
                                    Coordinates p;

                                    p = grid.getPositionOnMap(Grid.PACMAN);
                                    pacman = new Pacman(p.X * Tile.TILE_WITDH, p.Y * Tile.TILE_HEIGHT, engine);
                                    p = grid.getPositionOnMap(Grid.GHOST_RED);
                                    //TODO : MODIFIER
                                    foreach(Ghost g in listGhost)
                                    {
                                        g.init();
                                    }
                                }
                            }
                        }
                        else
                        {
                            pacman.die();
                        }
                    }
                }
                if (!pacman.isDead())
                {
                    pacman.Update(mouse, keyboard);
                    foreach(Ghost g in listGhost)
                    {
                        g.Update(pacman.getGridPosition());
                    }
                    if (engine.isCollisionBean(pacman.getHitbox()))
                    {
                        pacman.eat();
                        score.eatBean();
                    }
                    else if (engine.isCollisionSuperBean(pacman.getHitbox()))
                    {
                        pacman.eat();
                        score.eatSuperBean();
                        foreach (Ghost g in listGhost)
                        {
                            g.setVulnerable();
                        }
                    }
                }
            }
            else
            {
                foreach (Ghost g in listGhost)
                {
                    g.Update(pacman.getGridPosition());
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (gameOver != null)
            {
                gameOver.Draw(spriteBatch);
            }
            else if (startNewLevel)
            {
                startNewLevel = false;
                grid.Draw(spriteBatch);
                pacman.Draw(spriteBatch);
                foreach (Ghost g in listGhost)
                {
                    g.Draw(spriteBatch);
                }
                lives.Draw(spriteBatch);
                score.Draw(spriteBatch);
                Resources.beginningSound.Play();
            }
            else
            {
                grid.Draw(spriteBatch);
                pacman.Draw(spriteBatch);
                foreach (Ghost g in listGhost)
                {
                    g.Draw(spriteBatch);
                }
                lives.Draw(spriteBatch);
                score.Draw(spriteBatch);
            }
        }
    }
}

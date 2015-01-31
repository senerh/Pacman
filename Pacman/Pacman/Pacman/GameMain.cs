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
        Level level;
        bool startNewLevel;
        bool finished;

        //CONSTRUCTOR
        public GameMain()
        {
            lives = new Life();
            score = new Score();
            level = new Level();
            gameOver = null;
            finished = false;
            loadNextLevel();
        }

        //METHODS
        private void loadNextLevel()
        {
            level.loadNextLevel();
            
            grid = new Grid(level.getLevel());

            engine = new Engine(grid);

            Coordinates p = grid.getCoordinatesOnMap(Grid.PACMAN);
            pacman = new Pacman(p.X * Tile.TILE_WITDH, p.Y * Tile.TILE_HEIGHT, engine);

            listGhost = new List<Ghost>();

            p = grid.getCoordinatesOnMap(Grid.GHOST_RED);
            listGhost.Add(new GhostRed(p.X * Tile.TILE_WITDH, p.Y * Tile.TILE_HEIGHT, engine));
            p = grid.getCoordinatesOnMap(Grid.GHOST_ORANGE);
            listGhost.Add(new GhostOrange(p.X * Tile.TILE_WITDH, p.Y * Tile.TILE_HEIGHT, engine));
            p = grid.getCoordinatesOnMap(Grid.GHOST_PINK);
            listGhost.Add(new GhostPink(p.X * Tile.TILE_WITDH, p.Y * Tile.TILE_HEIGHT, engine));
            p = grid.getCoordinatesOnMap(Grid.GHOST_BLUE);
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
        public void Update(KeyboardState keyboard)
        {
            if (gameOver != null)//si le joueur a perdu
            {
                gameOver.Update(keyboard);
                if (gameOver.isFinished())
                {
                    finished = true;
                }
            }
            else if (Resources.beginningSound.State == SoundState.Stopped)//si la partie a commencé
            {
                if (grid.isFinished())//si le joueur a gagné
                {
                    loadNextLevel();
                }
                else if ((ghost = collidedGhost()) != null)//s'il y a une collision entre fantome et pacman
                {
                    if (ghost.isVulnerable() && !ghost.isDead())//si le fantôme est vulnérable
                    {
                        score.eatGhost();
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

                                    p = grid.getCoordinatesOnMap(Grid.PACMAN);
                                    pacman = new Pacman(p.X * Tile.TILE_WITDH, p.Y * Tile.TILE_HEIGHT, engine);
                                    p = grid.getCoordinatesOnMap(Grid.GHOST_RED);
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
                if (!pacman.isDead())//si la partie est en cours de jeu
                {
                    pacman.Update(keyboard);
                    foreach(Ghost g in listGhost)
                    {
                        g.Update(pacman.getGridPosition());
                    }
                    grid.Update();

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
                    else if (engine.isCollisionBonus(pacman.getHitbox()))
                    {
                        pacman.eat();
                        score.eatBonus();
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

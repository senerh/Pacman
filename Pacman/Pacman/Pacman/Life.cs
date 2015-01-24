using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Life
    {
        //CONSTANTS
        const int NB_LIFES_INIT = 3;

        //FIELDS
        Rectangle[] hitbox;
        int nbLives;

        //CONSTRUCTOR
        public Life()
        {
            hitbox = new Rectangle[NB_LIFES_INIT];
            for (int i = 0; i < NB_LIFES_INIT; i++)
            {
                hitbox[NB_LIFES_INIT - 1 - i] = new Rectangle((i + Grid.GRID_WIDTH - NB_LIFES_INIT) * Tile.TILE_WITDH, Grid.GRID_HEIGHT * Tile.TILE_HEIGHT, Tile.TILE_WITDH, Tile.TILE_HEIGHT);
            }

            nbLives = NB_LIFES_INIT;
        }

        //METHODS
        public void lose()
        {
            if (nbLives > 0)
                nbLives--;
        }

        public int remainingLives()
        {
            return nbLives;
        }

        //UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < nbLives; i++)
            {
                spriteBatch.Draw(Resources.life, hitbox[i], Color.White);
            }
        }
    }
}

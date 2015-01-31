using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Bonus
    {
        //CONSTANTS
        const int TIME_BONUS = 5*60;
        const int NB_BONUS = 4;

        //FIELDS
        Rectangle hitbox;
        int frameColumn;
        int timeBonus;

        //CONSTRUCTOR
        public Bonus(int x, int y)
        {
            frameColumn = 0;
            timeBonus = 0;
            hitbox = new Rectangle(x, y, Tile.TILE_WITDH, Tile.TILE_HEIGHT);
        }

        //METHODS
        public void addBonus()
        {
            frameColumn++;
            if (frameColumn > NB_BONUS)
            {
                frameColumn = 1;
            }
            timeBonus = TIME_BONUS;
        }

        public void removeBonus()
        {
            timeBonus = 0;
        }

        public bool isBonus()
        {
            return (timeBonus > 0);
        }

        public int getID()
        {
            return frameColumn;
        }

        public Rectangle getHitbox()
        {
            return hitbox;
        }

        //UPDATE & DRAW
        public void Update()
        {
            if (timeBonus > 0)
                timeBonus--;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isBonus())
            {
                spriteBatch.Draw(Resources.bonus, hitbox,
                new Rectangle((frameColumn - 1) * hitbox.Width, 0, hitbox.Width, hitbox.Height),
                Color.White);
            }
        }
    }
}

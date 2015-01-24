using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Score
    {
        //CONSTANTS
        const int SCORE_BEAN = 5;

        //FIELDS
        Vector2 position;
        int score;

        //CONSTRUCTOR
        public Score()
        {
            position = new Vector2(0, Grid.GRID_HEIGHT * Tile.TILE_HEIGHT);
            score = 0;
        }

        //METHODS
        public int getScore()
        {
            return score;
        }
        public void eatBean()
        {
            score = score + SCORE_BEAN;
        }

        //UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Resources.font, "Score : " + score, position, Color.White);
        }
    }
}

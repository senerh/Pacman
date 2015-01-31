using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    [Serializable]
    public class Score
    {
        //CONSTANTS
        const int SCORE_BEAN = 10;
        const int SCORE_SUPER_BEAN = 50;
        const int SCORE_GHOST = 200;
        int SCORE_BONUS = 10;

        //FIELDS
        Vector2 position;
        public int score;

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
        public void eatSuperBean()
        {
            score = score + SCORE_SUPER_BEAN;
        }

        public void eatGhost()
        {
            score = score + SCORE_GHOST;
        }

        public void eatBonus()
        {
            SCORE_BONUS = SCORE_BONUS + 10;
            score = score + SCORE_BONUS;
        }

        public void setHighscore()
        {
            if (score > getHighscore())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Score));
                StreamWriter writer = new StreamWriter("Content/highscore.xml", false);
                serializer.Serialize(writer, this);
                writer.Close();
            }
        }

        public int getHighscore()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Score));
                StreamReader reader = new StreamReader("Content/highscore.xml");
                Score s = (Score)serializer.Deserialize(reader);
                reader.Close();
                return s.getScore();
            }
            catch (FileNotFoundException)
            {
                return 0;
            }
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

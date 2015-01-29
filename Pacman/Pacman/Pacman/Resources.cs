using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    static class Resources
    {
        //STATIC FIELS
        public static Texture2D pacman;
        public static Texture2D pacmanDeath;
        public static Texture2D ghostBlue;
        public static Texture2D ghostOrange;
        public static Texture2D ghostPink;
        public static Texture2D ghostRed;
        public static Texture2D wallBot, wallBotLeft, wallBotRight, wallBotX;
        public static Texture2D wallLeft, wallLeftX;
        public static Texture2D wallMid, wallMidX, wallMidXH, wallMidXV;
        public static Texture2D wallRight, wallRightX;
        public static Texture2D wallTop, wallTopLeft, wallTopRight, wallTopX;
        public static Texture2D house;
        public static Texture2D bean;
        public static Texture2D life;
        public static Texture2D start;
        public static Texture2D gameOver;
        public static SoundEffectInstance beginningSound;
        public static SoundEffectInstance eatBean;
        public static SoundEffectInstance pacmanDeathSound;
        public static SpriteFont font;

        //LOAD CONTENT
        public static void LoadContent(ContentManager content)
        {
            pacman = content.Load<Texture2D>("images/pacman");
            pacmanDeath = content.Load<Texture2D>("images/pacman_death");
            ghostBlue = content.Load<Texture2D>("images/ghostBlue");
            ghostOrange = content.Load<Texture2D>("images/ghostOrange");
            ghostPink = content.Load<Texture2D>("images/ghostPink");
            ghostRed = content.Load<Texture2D>("images/ghostRed");
            wallBot = content.Load<Texture2D>("images/wallBot");
            wallBotLeft = content.Load<Texture2D>("images/wallBotLeft");
            wallBotRight = content.Load<Texture2D>("images/wallBotRight");
            wallBotX = content.Load<Texture2D>("images/wallBotX");
            wallLeft = content.Load<Texture2D>("images/wallLeft");
            wallLeftX = content.Load<Texture2D>("images/wallLeftX");
            wallMid = content.Load<Texture2D>("images/wallMid");
            wallMidX = content.Load<Texture2D>("images/wallMidX");
            wallMidXH = content.Load<Texture2D>("images/wallMidXH");
            wallMidXV = content.Load<Texture2D>("images/wallMidXV");
            wallRight = content.Load<Texture2D>("images/wallRight");
            wallRightX = content.Load<Texture2D>("images/wallRightX");
            wallTop = content.Load<Texture2D>("images/wallTop");
            wallTopLeft = content.Load<Texture2D>("images/wallTopLeft");
            wallTopRight = content.Load<Texture2D>("images/wallTopRight");
            wallTopX = content.Load<Texture2D>("images/wallTopX");
            house = content.Load<Texture2D>("images/house");
            bean = content.Load<Texture2D>("images/bean");
            life = content.Load<Texture2D>("images/life");
            start = content.Load<Texture2D>("images/start");
            gameOver = content.Load<Texture2D>("images/gameOver");
            beginningSound = content.Load<SoundEffect>("sons/pacman_beginning").CreateInstance();
            eatBean = content.Load<SoundEffect>("sons/eatBean").CreateInstance();
            pacmanDeathSound = content.Load<SoundEffect>("sons/PacmanEaten").CreateInstance();
            font = content.Load<SpriteFont>("polices/font");
        }
    }
}

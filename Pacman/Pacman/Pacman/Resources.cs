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
        public static Texture2D wall;
        public static Texture2D bean;
        public static SoundEffectInstance beginningSound;
        public static SoundEffectInstance eatBean;

        //LOAD CONTENT
        public static void LoadContent(ContentManager content)
        {
            pacman = content.Load<Texture2D>("images/pacman");
            wall = content.Load<Texture2D>("images/mur");
            bean = content.Load<Texture2D>("images/bean");
            beginningSound = content.Load<SoundEffect>("sons/pacman_beginning").CreateInstance();
            eatBean = content.Load<SoundEffect>("sons/eatBean").CreateInstance();
        }
    }
}

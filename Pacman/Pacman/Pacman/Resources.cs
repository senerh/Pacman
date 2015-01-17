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

        //LOAD CONTENT
        public static void LoadContent(ContentManager content)
        {
            pacman = content.Load<Texture2D>("images/pacman");
        }
    }
}

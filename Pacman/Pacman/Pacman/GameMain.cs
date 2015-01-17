using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class GameMain
    {
        //FIELDS
        Pacman pacman;

        //CONSTRUCTOR
        public GameMain()
        {
            pacman = new Pacman();
        }

        //METHODS

        //UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            pacman.Update(mouse, keyboard);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            pacman.Draw(spriteBatch);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Tile
    {
        //CONSTANTS
        public const int TILE_WITDH = 20;
        public const int TILE_HEIGHT = 20;

        //FIELDS
        Rectangle hitbox;
        Texture2D texture;

        //CONSTRUCTOR
        public Tile(int x, int y, Texture2D texture)
        {
            hitbox = new Rectangle(x, y, TILE_WITDH, TILE_HEIGHT);
            this.texture = texture;
        }

        //METHODS
        public Rectangle getHitbox()
        {
            return hitbox;
        }
        public Coordinates getCoordinatesOnMap()
        {
            return new Coordinates(hitbox.X / Tile.TILE_WITDH, hitbox.Y / Tile.TILE_HEIGHT);
        }

        //UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, Color.White);
        }
    }
}

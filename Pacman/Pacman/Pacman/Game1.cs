using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pacman
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameMain main;
        MenuStart menu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Resources.LoadContent(Content);

            menu = new MenuStart();
            main = new GameMain();

            graphics.PreferredBackBufferWidth = Grid.GRID_WIDTH * Tile.TILE_WITDH;
            graphics.PreferredBackBufferHeight = (Grid.GRID_HEIGHT + 1) * Tile.TILE_HEIGHT;
            graphics.ApplyChanges();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (menu.isFinished())
            {
                if (main.isFinished())
                {
                    menu = new MenuStart();
                    main = new GameMain();
                }
                else
                {
                    main.Update(Mouse.GetState(), Keyboard.GetState());
                }
            }
            else
            {
                menu.Update(Mouse.GetState(), Keyboard.GetState());
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (menu.isFinished())
            {
                main.Draw(spriteBatch);
            }
            else
            {
                menu.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

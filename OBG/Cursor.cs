using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBG
{
    class Cursor : DrawableSprite
    {
        Texture2D CursorTexture;
        public Vector2 CursorLoc;
        Vector2 CursorDir;
        public new Vector2 Origin;
        float CursorSpeed;

        public string TextureName { get; set; }
        Game game;
        float time;

        public Cursor(Game game) : base(game)
        {
            this.game = game;
        }

        public void LoadContent()
        {
            if (string.IsNullOrEmpty(TextureName))
                TextureName = "Curser";
            CursorTexture = game.Content.Load<Texture2D>("Curser");

            CursorLoc = new Vector2(game.GraphicsDevice.Viewport.Width / 2,
                270);
            CursorDir = new Vector2(1, 0);
            Origin = new Vector2(CursorTexture.Width / 2, CursorTexture.Height / 2);

            CursorSpeed = 200;
        }

        public void Update(GameTime gameTime)
        {
            time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //Time corrected move. Moves Sprite for every second instead of frame
            CursorLoc = CursorLoc + ((CursorDir * CursorSpeed) * (time / 1000));

            KeepOnScreen();
        }
         
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(CursorTexture, CursorLoc, Color.White);
        }

        private void KeepOnScreen()
        {
            //X right
            if (CursorLoc.X >
                    game.GraphicsDevice.Viewport.Width - CursorTexture.Width)
            {
                //Negate X
                CursorDir = CursorDir * new Vector2(-1, 1);
                CursorLoc.X = game.GraphicsDevice.Viewport.Width - CursorTexture.Width;
            }

            //X left
            if (CursorLoc.X < 0)
            {
                //Negate X
                CursorDir = CursorDir * new Vector2(-1, 1);
                CursorLoc.X = 0;
            }
        }

        public void Hit()
        {
            CursorSpeed *= 1.3f;
        }

        public void Miss()
        {
            CursorSpeed = 0;
        }
    }
}

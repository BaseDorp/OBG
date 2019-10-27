using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBG
{
    class Target : DrawableSprite
    {
        Texture2D Texture;
        public Vector2 Location;
        Vector2 Origin;
        Rectangle rectangleLoc;

        public string TextureName { get; set; }
        Game game;
        float time;

        public Target(Game game) : base(game)
        {
            this.game = game;
        }

        public void LoadContent()
        {
            if (string.IsNullOrEmpty(TextureName))
                TextureName = "Target.png";
            Texture = game.Content.Load<Texture2D>("Target");

            Location = new Vector2(350,
                game.GraphicsDevice.Viewport.Height / 2);
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        public void Update(GameTime gameTime)
        {
            time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Location, Color.White);
        }
    }
}

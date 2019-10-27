using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// 'Keyboard oldState' and 'Keyboard newState' idea from https://www.gamefromscratch.com/post/2015/06/28/MonoGame-Tutorial-Handling-Keyboard-Mouse-and-GamePad-Input.aspx

namespace OBG
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int score = 0;
        SpriteFont scoreFont;
        string scoreString;
        float pixelsMissed = 0;

        Cursor cursor;
        Target target;

        KeyboardState oldState;
        KeyboardState newState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            cursor = new Cursor(this) { TextureName = "Curser" };
            target = new Target(this) { TextureName = "Target" };
        }

        
        protected override void Initialize()
        {
            base.Initialize();
            scoreString = $"Score: {score}\nPress Space when the crosshair is over the target. Get to 10 to win. Miss once and you lose.";
            oldState = Keyboard.GetState();
        }

        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            cursor.LoadContent();
            target.LoadContent();

            scoreFont = Content.Load<SpriteFont>("Score");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            cursor.Update(gameTime);

            newState = Keyboard.GetState();
            if (newState.IsKeyUp(Keys.Space) & !oldState.IsKeyUp(Keys.Space))
            {
                // Checks if center of cursor hit the target
                if ((cursor.CursorLoc.X + 20) > target.Location.X && (cursor.CursorLoc.X + 20) < (target.Location.X + 104))
                {
                    score++;
                    cursor.Hit();
                    scoreString = $"Score: {score}\nPress Space when the crosshair is over the target. Get to 10 to win. Miss once and you lose.";
                }
                else
                {
                    cursor.Miss();
                    GameOver();
                }
            }
            oldState = newState;

            // Win state
            if (score >= 10)
            {
                GameWin();
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            target.Draw(spriteBatch);
            cursor.Draw(spriteBatch);
            spriteBatch.DrawString(scoreFont, scoreString, new Vector2(10, 10), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void GameWin()
        {
            scoreString = $"Score: {score}\nCongratulations! You Win! Keep going to see how far you can go.";
        }

        void GameOver()
        {
            // Checks if the player is to the left or the right of the target
            if ((cursor.CursorLoc.X + 20) > (target.Location.X + 104))
            {
                pixelsMissed = (cursor.CursorLoc.X + 20) - (target.Location.X + 104);
            }
            else
            {
                pixelsMissed = target.Location.X - (cursor.CursorLoc.X + 20);
            }
            scoreString = $"Score: {score}\nGame Over. You missed the target by {pixelsMissed} pixls.";
        }
    }
}

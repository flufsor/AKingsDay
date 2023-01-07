using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AKingsDay.States
{
    internal class GameOverState : IState
    {
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(AKingsDay.Arial, "Thank you for playing A Kings Day", new Vector2(240, 420), Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.GetPressedKeys().Length > 0)
            {
                AKingsDay.Quit();
            }
        }
    }
}

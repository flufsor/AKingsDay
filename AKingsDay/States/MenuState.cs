using AKingsDay.States.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AKingsDay.States
{
    public class MenuState : IState
    {
        private readonly Crown _crown;
        public MenuState()
        {
            _crown = new Crown(new Vector2(394, 50));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AKingsDay.Textures["MenuBG"], new Rectangle(0, 0, 960, 680), Color.White);
            _crown.Draw(spriteBatch);
            spriteBatch.DrawString(AKingsDay.Arial, "A Kings Day", new Vector2(350, 180), Color.Gold, 0, Vector2.Zero, 3f, SpriteEffects.None, 0);
            spriteBatch.DrawString(AKingsDay.Arial, "Press any key to start a new game", new Vector2(240, 420), Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.GetPressedKeys().Length > 0)
            {
                AKingsDay.ChangeState(new GameState(1));
            }

            _crown.Update(gameTime);
        }
    }
}

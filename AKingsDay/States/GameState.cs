using AKingsDay.States.Game;
using AKingsDay.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.States
{
    public class GameState : IState
    {
        private Level _activeLevel;
        private UserInterface _userInterface;

        public GameState()
        {
            _activeLevel = new Level(AKingsDay.LevelMaps[1]);
            _userInterface = UserInterface.GetInstance();
        }
        public GameState(int level)
        {
            _activeLevel = new Level(AKingsDay.LevelMaps[level]);
            _userInterface = UserInterface.GetInstance();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Level.Draw(spriteBatch);
            _userInterface.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            Level.Update(gameTime);
            _userInterface.Update(gameTime);
        }
    }
}

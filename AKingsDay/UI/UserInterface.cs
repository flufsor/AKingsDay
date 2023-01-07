using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AKingsDay.UI
{
    public sealed class UserInterface
    {
        private readonly List<IUIElement> _userInterfaces;
        private bool _hideUI;

        private volatile static UserInterface _instance;
        private UserInterface()
        {
            _userInterfaces = new List<IUIElement>();
            _userInterfaces.Add(new LifeBar(new Vector2(48, 48)));
            _userInterfaces.Add(new DiamondCounter(new Vector2(873,48)));
        }

        public static UserInterface GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserInterface();
            }
            return _instance;
        }

        public void Update(GameTime gameTime)
        {
            foreach (IUIElement uiElement in _userInterfaces)
            {
                uiElement.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_hideUI)
            {
                foreach (IUIElement uiElement in _userInterfaces)
                {
                    uiElement.Draw(spriteBatch);
                }
            }
        }

        public void ToggleUI()
        {
            _hideUI = !_hideUI;
        }
    }
}

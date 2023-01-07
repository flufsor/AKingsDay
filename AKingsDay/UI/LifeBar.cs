using AKingsDay.States.Game;
using AKingsDay.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AKingsDay.UI
{
    internal class LifeBar : IUIElement
    {
        public Vector2 Position { get; private set; }
        private StaticSprite _lifeBarSprite;
        private List<Animation> _heartSprites;
        private int _hearts;

        public LifeBar(Vector2 position)
        {
            Position = position;
            _lifeBarSprite = new StaticSprite(AKingsDay.Textures["LifeBar"]);

            Texture2D _heartSprite = AKingsDay.Textures["LifeBarHeart"];
            _heartSprites = new List<Animation>();
            _hearts = Level.Hero.Health;
            for (int i = 0; i < _hearts; i++)
            {
                _heartSprites.Add(new Animation(_heartSprite, 0, 8, 18, 14, 5, 0, i % _hearts));
            }
        }

        public void Update(GameTime gameTime)
        {
            _hearts = Level.Hero.Health;
            _lifeBarSprite.Update(gameTime);
            for (int i = 0; i < _hearts; i++)
            {
                _heartSprites[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _lifeBarSprite.Draw(spriteBatch, Position, 0, 1.5f);

            for (int i = 0; i < _hearts; i++)
            {
                _heartSprites[i].Draw(spriteBatch, new Vector2(Position.X + 22 + (i * 17), Position.Y + 18));
            }
        }

    }
}

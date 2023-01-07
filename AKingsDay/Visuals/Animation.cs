using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AKingsDay.Visuals
{
    public class Animation : IShowable
    {
        private readonly Texture2D _spriteSheet;
        private readonly List<Rectangle> _frames;
        private readonly SpriteEffects _effects;
        private int _frameCounter = 0;
        private double _secondCounter;
        private readonly int _fps;

        public Animation(Texture2D spriteSheet, int rowOffset, int frameCount, int width, int height, int fps, int spacing = 5, SpriteEffects effects = SpriteEffects.None)
        {
            _spriteSheet = spriteSheet;
            _fps = fps;
            _effects = effects;

            _frames = new List<Rectangle>();
            for (int i = 0; i < frameCount; i++)
            {
                _frames.Add(new Rectangle(spacing * i + width * i, rowOffset, width, height));
            }
        }

        public Animation(Texture2D spriteSheet, int rowHeight, int frameCount, int width, int height, int fps, int spacing = 5, int startAnimationFrame = 0, SpriteEffects effects = SpriteEffects.None) : this(
            spriteSheet, rowHeight, frameCount, width, height, fps, spacing, effects)
        {
            _frameCounter = startAnimationFrame;
        }

        public void Update(GameTime gameTime)
        {
            _secondCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if (_secondCounter >= 1d / _fps)
            {
                _frameCounter++;
                _secondCounter = 0;
            }

            if (_frameCounter >= _frames.Count)
            {
                _frameCounter = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation = 0, float scale = 1)
        {
            spriteBatch.Draw(_spriteSheet, position, _frames[_frameCounter], Color.White, 0f, Vector2.Zero, scale, _effects, 0f);
        }
    }
}

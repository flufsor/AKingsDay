using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.Visuals
{
    public class StaticSprite : IShowable
    {
        private Texture2D _spriteSheet;
        private Rectangle _sourceRect;
        private SpriteEffects _spriteEffects;

        public StaticSprite(Texture2D spriteSheet, int x = 0, int y = 0, int width = -1, int height = -1, SpriteEffects effects = SpriteEffects.None)
        {
            _spriteSheet = spriteSheet;
            width = width < 0 ? spriteSheet.Width : width;
            height = height < 0 ? spriteSheet.Height : height;
            _sourceRect = new Rectangle(x, y, width, height);
            _spriteEffects = effects;
        }

        public void Update(GameTime gameTime)
        {
            //
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation = 0f, float scale = 1f)
        {
            spriteBatch.Draw(_spriteSheet, position, _sourceRect, Color.White, 0f, Vector2.Zero, scale, _spriteEffects, 0f);
        }

    }
}

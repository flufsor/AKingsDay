using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.Visuals
{
    public interface IShowable
    {
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation = 0f, float scale = 1f);
    }
}

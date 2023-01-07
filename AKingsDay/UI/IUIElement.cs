using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.UI
{
    public interface IUIElement
    {
        Vector2 Position { get; }
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch sprite);
    }
}

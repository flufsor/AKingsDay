using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.States
{
    public interface IState
    {
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public void Update(GameTime gameTime);
    }
}

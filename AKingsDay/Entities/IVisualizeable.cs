using AKingsDay.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.Entities
{
    public interface IVisualizeable : IEntity
    {

        public IShowable CurrentVisual { get; set; }

        public void Draw(SpriteBatch spriteBatch);
        public void UpdateVisual(Vector2 direction);
    }
}
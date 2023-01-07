using AKingsDay.States.Game;
using AKingsDay.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.Entities
{
    public class Tile : IVisualizeable
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public IShowable CurrentVisual { get; set; }
        public EntityStatus Status { get; set; }

        public Tile(Rectangle hitbox, Rectangle srcRect)
        {
            Position = new Vector2(hitbox.X, hitbox.Y);
            Size = new Vector2(hitbox.Width, hitbox.Height);
            CurrentVisual = new StaticSprite(AKingsDay.Textures["Tiles"], srcRect.X, srcRect.Y, (int)Size.X, (int)Size.Y);
            Status = EntityStatus.Idle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentVisual.Draw(spriteBatch, Position);
        }

        public void Destroy()
        {
            Level.RemoveEntity(this);
        }

        public void UpdateVisual(Vector2 direction)
        {
            //Tile does not change Visuals at the moment
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}

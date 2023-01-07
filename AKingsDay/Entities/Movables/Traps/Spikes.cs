using AKingsDay.States.Game;
using AKingsDay.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.Entities.Movables.Traps
{
    public class Spikes : ICollidable, IVisualizeable
    {
        public IShowable CurrentVisual { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public EntityStatus Status { get; set; }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }

        }

        public bool IsSolid { get; internal set; }

        public Spikes(Vector2 position)
        {
            Size = new Vector2(32, 16);
            Position = new Vector2(position.X, position.Y+16);
            IsSolid = false;

            CurrentVisual = new StaticSprite(AKingsDay.Textures["Spikes"], 0, 16, (int)Size.X, (int)Size.Y);
        }

        public void Destroy()
        {
            Level.RemoveEntity(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentVisual.Draw(spriteBatch, Position);
        }

        public void OnCollide(GameTime gameTime, ICollidable sourceHitbox, CollisionDirection collisionDirection)
        {
            //
        }

        public void Update(GameTime gameTime)
        {
            CurrentVisual.Update(gameTime);
        }

        public void UpdateVisual(Vector2 direction)
        {
            //
        }
    }
}

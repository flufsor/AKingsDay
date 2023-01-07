using AKingsDay.States.Game;
using AKingsDay.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.Entities.Movables.Objects
{
    public class Diamond : ICollidable, IVisualizeable
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }

        }
        public bool IsSolid { get; internal set; }
        public IShowable CurrentVisual { get; set; }
        public EntityStatus Status { get; set; }

        public Diamond(Vector2 position)
        {
            Position = position;
            Size = new Vector2(12, 10);
            IsSolid = false;

            int frameCount = 10;
            int randomStart = AKingsDay.Rng.Next(0, frameCount + 1);

            CurrentVisual = new Animation(AKingsDay.Textures["Diamond"], 0, frameCount, (int)Size.X, (int)Size.Y, 5, 5, randomStart, SpriteEffects.None);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentVisual.Draw(spriteBatch, Position);
        }

        public void OnCollide(GameTime gameTime, ICollidable collider, CollisionDirection collisionDirection)
        {
            if (collider is Hero)
            {
                AKingsDay.SoundEffects["Diamond"].Play();
                Level.DiamondsFound++;
                Level.RemoveEntity(this);
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentVisual.Update(gameTime);
        }
        public void Destroy()
        {
            Level.RemoveEntity(this);
        }
        public void UpdateVisual(Vector2 direction)
        {
            //
        }
    }
}

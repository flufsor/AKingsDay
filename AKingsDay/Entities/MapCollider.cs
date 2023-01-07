using AKingsDay.States.Game;
using Microsoft.Xna.Framework;

namespace AKingsDay.Entities
{
    public class MapCollider : ICollidable
    {
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }

        }
        public bool IsSolid { get; internal set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public EntityStatus Status { get; set; }

        public MapCollider(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
            IsSolid = true;
        }

        public void Destroy()
        {
            Level.TileCollidables.Remove(this);
        }

        public void OnCollide(GameTime gameTime, ICollidable sourceHitbox, CollisionDirection collisionDirection)
        {
            //
        }

        public void Update(GameTime gameTime)
        {
            //
        }
    }
}

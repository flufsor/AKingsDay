using AKingsDay.States.Game;
using Microsoft.Xna.Framework;

namespace AKingsDay.Entities
{
    public interface ICollidable : IEntity
    {
        public Rectangle Bounds { get; }

        public bool IsSolid { get; }

        public void OnCollide(GameTime gameTime, ICollidable sourceHitbox, CollisionDirection collisionDirection);
    }
}

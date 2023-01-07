using AKingsDay.Entities;
using AKingsDay.Entities.Movables;
using AKingsDay.States.Game;
using Microsoft.Xna.Framework;

namespace AKingsDay.Behaviours.Movement
{
    public interface IBehaviour
    {
        public bool BlockMoving { get; set; }
        public void Update(IMoveable entity, GameTime gameTime);
        public void OnCollide(GameTime gameTime, ICollidable collider, CollisionDirection collisionDirection);
    }
}

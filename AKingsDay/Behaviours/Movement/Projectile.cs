using AKingsDay.Entities;
using AKingsDay.Entities.Movables;
using AKingsDay.States.Game;
using Microsoft.Xna.Framework;

namespace AKingsDay.Behaviours.Movement
{
    public class Projectile : IBehaviour
    {
        public bool BlockMoving { get; set; }

        private Vector2 _direction;
        
        public Projectile(Vector2 direction)
        {
            _direction = direction;

        }

        public void OnCollide(GameTime gameTime, ICollidable collider, CollisionDirection collisionDirection)
        {

        }

        public void Update(IMoveable entity, GameTime gameTime)
        {
            entity.Move(gameTime, _direction, entity);
        }
    }
}

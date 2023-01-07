using AKingsDay.Entities;
using AKingsDay.Entities.Movables;
using AKingsDay.States.Game;
using Microsoft.Xna.Framework;

namespace AKingsDay.Behaviours.Movement
{
    public class Patrolling : IBehaviour
    {
        public bool BlockMoving { get; set; }

        private bool _runLeft;
        public Patrolling()
        {
            _runLeft = true;
        }

        public void OnCollide(GameTime gameTime, ICollidable collider, CollisionDirection collisionDirection)
        {
            if (collisionDirection == CollisionDirection.Left || collisionDirection == CollisionDirection.Right)
                _runLeft = !_runLeft;
        }

        public void Update(IMoveable entity, GameTime gameTime)
        {
            Vector2 direction;

            if (_runLeft)
            {
                direction = new Vector2(-1, 0);
            }
            else
            {
                direction = new Vector2(1, 0);
            }

            entity.Move(gameTime, direction, entity);
        }
    }
}

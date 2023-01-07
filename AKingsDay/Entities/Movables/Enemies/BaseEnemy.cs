using AKingsDay.States.Game;
using Microsoft.Xna.Framework;

namespace AKingsDay.Entities.Movables.Enemies
{
    public abstract class BaseEnemy : BaseMoveable
    {
        protected BaseEnemy(Vector2 position) : base(position)
        {
        }

        public override void OnCollide(GameTime gameTime, ICollidable collider, CollisionDirection collisionDirection)
        {
            Behaviour.OnCollide(gameTime, collider, collisionDirection);
        }

    }
}

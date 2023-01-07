using AKingsDay.Behaviours.Movement;
using AKingsDay.Constants;
using AKingsDay.States.Game;
using AKingsDay.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AKingsDay.Entities.Movables.Enemies
{
    public class CannonBall : BaseEnemy, IMoveable
    {

        public CannonBall(Vector2 position, Vector2 direction) : base(position)
        {
            Size = new Vector2(12, 12);
            Texture2D _spriteSheet = AKingsDay.Textures["Cannon"];

            #region Sprites
            _visuals.Add("CannonBall", new StaticSprite(_spriteSheet, 0, 50, (int)Size.X, (int)Size.Y));
            #endregion

            CurrentVisual = _visuals["CannonBall"];
            Status = EntityStatus.Idle;
            Behaviour = new Projectile(direction);
        }

        public override void OnCollide(GameTime gameTime, ICollidable collider, CollisionDirection collisionDirection)
        {
            if (collider is not Cannon)
            {
                Destroy();
            }
        }

        public override void Move(GameTime gameTime, Vector2 direction, IMoveable moveable)
        {
            float velocityX = direction.X * Physics.PROJECTILE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
            float velocityY = direction.Y * Physics.PROJECTILE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;


            Velocity = new Vector2(velocityX, velocityY);

            float x = Math.Clamp(Velocity.X, -Physics.PROJECTILE_SPEED, Physics.PROJECTILE_SPEED);
            float y = Math.Clamp(Velocity.Y, -Physics.PROJECTILE_SPEED, Physics.PROJECTILE_SPEED);
            Velocity = new Vector2(x, y);

            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            CollisionManager.CheckForCollisions(gameTime, this);
        }
    }
}

using AKingsDay.Behaviours.Movement;
using AKingsDay.Constants;
using AKingsDay.States.Game;
using AKingsDay.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AKingsDay.Entities.Movables
{
    public abstract class BaseMoveable : IMoveable
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
        public Vector2 Velocity { get; set; }
        public EntityStatus Status { get; set; }
        public bool IsSolid { get; internal set; }
        public IShowable CurrentVisual { get; set; }

        public bool IsFalling { get; set; }

        public IBehaviour Behaviour { get; set; }

        internal Dictionary<string, IShowable> _visuals;

        internal Vector2 _prevDirection;

        public BaseMoveable(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.Zero;
            IsSolid = true;
            _visuals = new Dictionary<string, IShowable>();
        }

        public void Destroy()
        {
            Level.RemoveEntity(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentVisual.Draw(spriteBatch, Position);
        }

        public virtual void Move(GameTime gameTime, Vector2 direction, IMoveable moveable)
        {
            UpdatePosition(gameTime, direction);
            CollisionManager.CheckForCollisions(gameTime, moveable);
            moveable.UpdateVisual(direction);
        }

        public abstract void OnCollide(GameTime gameTime, ICollidable collidable, CollisionDirection collisionDirection);

        public virtual void Update(GameTime gameTime)
        {
            Behaviour.Update(this, gameTime);
            CurrentVisual.Update(gameTime);
        }

        internal void UpdatePosition(GameTime gameTime, Vector2 direction)
        {
            int velocitySign = MathF.Sign(Velocity.X);
            Vector2 velocity = Velocity;

            if (CollisionManager.CheckIfFalling(this))
            {
                velocity.Y += Physics.GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
                velocity.X -= -direction.X * Physics.ACCELERATION * (float)gameTime.ElapsedGameTime.TotalSeconds * Physics.JUMP_CONTROL;
            }
            else
            {
                velocity.X -= velocitySign * Physics.FRICTION * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Math.Abs(velocitySign - Math.Sign(velocity.X)) > float.Epsilon)
                {
                    velocity.X = 0f;
                }

                velocity.Y = 0;

                if (direction.Y > 0)
                {
                    velocity.Y += Physics.JUMP_VELOCITY;
                    IsFalling = true;
                }
                velocity.X -= -direction.X * Physics.ACCELERATION * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            float x = Math.Clamp(velocity.X, -Physics.MAX_VELOCITY, Physics.MAX_VELOCITY);
            float y = Math.Clamp(velocity.Y, -Physics.GRAVITY, Physics.GRAVITY);
            Velocity = new Vector2(x, y);
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void UpdateVisual(Vector2 direction) { }

    }
}

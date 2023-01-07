using AKingsDay.Entities;
using AKingsDay.Entities.Movables;
using AKingsDay.Entities.Movables.Enemies;
using AKingsDay.States.Game;
using Microsoft.Xna.Framework;

namespace AKingsDay.Behaviours.Movement
{
    public class AttackOnTimer : IBehaviour
    {
        public bool BlockMoving { get; set; }
        private const int ATTACKINTERVAL = 5;
        private double _attackCounter;
        private double _attackresetCounter;
        private bool _isAttacking;
        private Vector2 _attackPosition;
        private Vector2 _attackDirection;

        public AttackOnTimer(bool immovable, Vector2 attackDirection, Vector2 attackPosition)
        {
            BlockMoving = immovable;
            _attackPosition = attackPosition;
            _attackDirection = attackDirection;

        }

        public void Update(IMoveable entity, GameTime gameTime)
        {
            _attackCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if (_isAttacking)
            {
                _attackresetCounter += gameTime.ElapsedGameTime.TotalSeconds;
                if (_attackresetCounter >= 0.6f)
                {
                    entity.Status = EntityStatus.Idle;
                    _isAttacking = false;
                }
            }
            else if (_attackCounter >= ATTACKINTERVAL)
            {
                entity.Status = EntityStatus.Attack;
                AKingsDay.SoundEffects["Cannon"].Play();
                Level.Entities.Add(new CannonBall(_attackPosition, _attackDirection));
                _attackCounter = 0;
                _attackresetCounter = 0;
                _isAttacking = true;
            }

            entity.Move(gameTime, Vector2.Zero, entity);
        }

        public void OnCollide(GameTime gameTime, ICollidable collider, CollisionDirection collisionDirection)
        {
            //
        }
    }
}

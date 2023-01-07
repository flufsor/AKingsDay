using AKingsDay.Behaviours.Movement;
using AKingsDay.Entities.Movables.Enemies;
using AKingsDay.Entities.Movables.Traps;
using AKingsDay.States;
using AKingsDay.States.Game;
using AKingsDay.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.Entities.Movables
{
    public class Hero : BaseMoveable, IMoveable
    {
        public int Health { get; private set; }

        private const double INVULNERABLETIME = .6;
        private double _invulnerableCounter;

        private const int DEATHTIMER = 3;
        private double _deathCounter;

        public Hero(Vector2 position) : base(position)
        {
            Size = new Vector2(37, 28);
            Texture2D _spriteSheet = AKingsDay.Textures["Hero"];

            #region Sprites
            _visuals.Add("IdleRight", new Animation(_spriteSheet, 0, 11, 37, 28, 5, 5, SpriteEffects.None));
            _visuals.Add("IdleLeft", new Animation(_spriteSheet, 0, 11, 37, 28, 5, 5, SpriteEffects.FlipHorizontally));

            _visuals.Add("RunRight", new Animation(_spriteSheet, 28, 8, 37, 28, 5, 5, SpriteEffects.None));
            _visuals.Add("RunLeft", new Animation(_spriteSheet, 28, 8, 37, 28, 5, 5, SpriteEffects.FlipHorizontally));

            _visuals.Add("DieRight", new OneShot(_spriteSheet, 57, 4, 38, 34, 5, 5, SpriteEffects.None));
            _visuals.Add("DieLeft", new OneShot(_spriteSheet, 57, 4, 38, 34, 5, 5, SpriteEffects.FlipHorizontally));

            _visuals.Add("HitRight", new Animation(_spriteSheet, 91, 2, 37, 28, 5, 2, SpriteEffects.None));
            _visuals.Add("HitLeft", new Animation(_spriteSheet, 91, 2, 37, 28, 5, 2, SpriteEffects.FlipHorizontally));
            #endregion

            CurrentVisual = _visuals["IdleRight"];
            Behaviour = new PlayerControlled();

            Health = 3;
        }
        public override void Update(GameTime gameTime)
        {
            switch (Status)
            {
                case EntityStatus.Hit:
                    _invulnerableCounter -= gameTime.ElapsedGameTime.TotalSeconds;

                    if (_invulnerableCounter <= 0)
                    {
                        _invulnerableCounter = 0;
                        Status = EntityStatus.Idle;
                    }
                    break;
                case EntityStatus.Death:
                    _deathCounter -= gameTime.ElapsedGameTime.TotalSeconds;

                    if (_deathCounter <= 0)
                    {
                        AKingsDay.ChangeState(new GameOverState());
                    }
                    break;
            }

            base.Update(gameTime);
        }

        private void Takedamage(int damage = 1)
        {
            if (Status != EntityStatus.Death && Status != EntityStatus.Hit)
            {
                Status = EntityStatus.Hit;

                Health -= damage;
                if (Health > 0)
                {
                    _invulnerableCounter = INVULNERABLETIME;
                    AKingsDay.SoundEffects["Grunt"].Play();
                }
                else
                {
                    Die();
                }
            }
        }

        private void Die()
        {
            Status = EntityStatus.Death;
            _deathCounter = DEATHTIMER;
            Behaviour.BlockMoving = true;
            AKingsDay.SoundEffects["Death"].Play();
            IsSolid = false;
        }

        public override void UpdateVisual(Vector2 direction)
        {
            bool isFacingLeft = direction.X < 0 || _prevDirection.X < 0 && direction.X == 0;

            if (Status != EntityStatus.Death && Status != EntityStatus.Hit)
            {
                if (direction.X != 0)
                {
                    Status = EntityStatus.Run;
                }
                else
                {
                    Status = EntityStatus.Idle;
                }
            }

            switch (Status)
            {
                case EntityStatus.Hit:
                    CurrentVisual = isFacingLeft ? _visuals["HitLeft"] : _visuals["HitRight"];
                    break;
                case EntityStatus.Death:
                    CurrentVisual = isFacingLeft ? _visuals["DieLeft"] : _visuals["DieRight"];
                    break;
                case EntityStatus.Idle:
                    CurrentVisual = isFacingLeft ? _visuals["IdleLeft"] : _visuals["IdleRight"];
                    break;
                case EntityStatus.Run:
                    CurrentVisual = isFacingLeft ? _visuals["RunLeft"] : _visuals["RunRight"];
                    break;
            }

            if (direction.X != 0)
            {
                _prevDirection.X = direction.X;
            }
        }

        public override void OnCollide(GameTime gameTime, ICollidable collider, CollisionDirection collisionDirection)
        {
            if(collider is BaseEnemy || collider is Spikes)
            {
                    Takedamage();
            }
        }
    }
}

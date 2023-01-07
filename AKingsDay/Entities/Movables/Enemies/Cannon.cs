using AKingsDay.Behaviours.Movement;
using AKingsDay.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.Entities.Movables.Enemies
{
    public class Cannon : BaseEnemy, IMoveable
    {
        public new Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }

        }

        private Vector2 _attackPosition;
        private Vector2 _attackDirection;

        public Cannon(Vector2 position) : base(position)
        {
            Size = new Vector2(41, 24);
            Texture2D _spriteSheet = AKingsDay.Textures["Cannon"];

            #region Sprites
            _visuals.Add("ShootLeft", new OneShot(_spriteSheet, 0, 3, (int)Size.X, (int)Size.Y, 5, 0, SpriteEffects.None));
            _visuals.Add("ShootRight", new OneShot(_spriteSheet, 0, 3, (int)Size.X, (int)Size.Y, 5, 0, SpriteEffects.FlipHorizontally));
            _visuals.Add("IdleLeft", new StaticSprite(_spriteSheet, 0, 26, (int)Size.X, (int)Size.Y, SpriteEffects.None));
            _visuals.Add("IdleRight", new StaticSprite(_spriteSheet, 0, 26, (int)Size.X, (int)Size.Y, SpriteEffects.FlipHorizontally));
            #endregion

            _attackPosition = new Vector2(Position.X, Position.Y+3 );
            _attackDirection = new Vector2(-1, 0);


            CurrentVisual = _visuals["IdleLeft"];
            Status = EntityStatus.Idle;
            Behaviour = new AttackOnTimer(true, _attackDirection, _attackPosition);
        }

        public override void UpdateVisual(Vector2 direction)
        {
            switch (Status)
            {
                case EntityStatus.Idle:
                    CurrentVisual = _visuals["IdleLeft"];
                    break;
                case EntityStatus.Attack:
                    CurrentVisual = _visuals["ShootLeft"];
                    break;
            }
        }
    }
}
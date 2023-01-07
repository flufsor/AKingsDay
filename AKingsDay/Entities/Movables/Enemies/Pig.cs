using AKingsDay.Behaviours.Movement;
using AKingsDay.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKingsDay.Entities.Movables.Enemies
{
    public class Pig : BaseEnemy, IMoveable
    {
        public Pig(Vector2 position) : base(position)
        {
            Size = new Vector2(19, 18);
            Velocity = new Vector2(200, 0);
            Texture2D _spriteSheet = AKingsDay.Textures["Pig"];

            #region Sprites
            _visuals.Add("RunRight", new Animation(_spriteSheet, 37, 6, 19, 18, 5, 0, SpriteEffects.FlipHorizontally));
            _visuals.Add("RunLeft", new Animation(_spriteSheet, 37, 6, 19, 18, 5, 0, SpriteEffects.None));
            #endregion

            CurrentVisual = _visuals["RunLeft"];
            Status = EntityStatus.Run;
            Behaviour = new Patrolling();
        }

        public override void UpdateVisual(Vector2 direction)
        {
            switch (Status)
            {
                case EntityStatus.Run:
                    CurrentVisual = direction.X < 0 ? _visuals["RunLeft"] : _visuals["RunRight"];
                    break;
            }
        }

    }
}

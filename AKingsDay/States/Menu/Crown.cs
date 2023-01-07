using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AKingsDay.States.Menu
{
    internal class Crown
    {
        private const float CROWNAMPLITUDE = 10;
        private readonly Vector2 CROWNBASEPOS;
        private Vector2 _crownPos;
        private double _elapsedTime;

        public Crown(Vector2 position)
        {
            CROWNBASEPOS = position;
            _crownPos = CROWNBASEPOS;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AKingsDay.Textures["Crown"], _crownPos, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            _crownPos.Y = (float)Math.Sin(_elapsedTime) * CROWNAMPLITUDE + CROWNBASEPOS.Y;
        }
    }
}

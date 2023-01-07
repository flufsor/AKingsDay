using AKingsDay.States.Game;
using AKingsDay.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AKingsDay.UI
{
    public class DiamondCounter : IUIElement
    {
        public Vector2 Position { get; private set; }
        private IShowable _showable;
        private Vector2 _diamondsFoundPos;
        private Vector2 _diamondCountPos;

        public DiamondCounter(Vector2 position)
        {
            Position = position;
            _showable = new StaticSprite(AKingsDay.Textures["Panel"]);

            _diamondsFoundPos = new Vector2(Position.X + 6, Position.Y + 4);
            _diamondCountPos = new Vector2(Position.X + 24, Position.Y + 18);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _showable.Draw(spriteBatch, Position);

            spriteBatch.DrawString(AKingsDay.Arial, Level.DiamondsFound.ToString(), _diamondsFoundPos, Color.Gold, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);

            spriteBatch.DrawString(AKingsDay.Arial, Level.DiamondCount.ToString(), _diamondCountPos, Color.Gold, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);

        }

        public void Update(GameTime gameTime)
        {
            //
        }
    }
}

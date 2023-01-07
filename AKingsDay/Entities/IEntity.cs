using Microsoft.Xna.Framework;

namespace AKingsDay.Entities
{
    public interface IEntity
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public EntityStatus Status { get; set; }
        public void Update(GameTime gameTime);
        public void Destroy();

    }
}

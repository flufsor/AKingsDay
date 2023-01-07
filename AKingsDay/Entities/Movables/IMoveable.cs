using AKingsDay.Behaviours.Movement;
using Microsoft.Xna.Framework;


namespace AKingsDay.Entities.Movables
{
    public interface IMoveable : IVisualizeable, ICollidable
    {
        public Vector2 Velocity { get; set; }
        public IBehaviour Behaviour { get; }
        public bool IsFalling { get; set; }
        public void Move(GameTime gameTime, Vector2 direction, IMoveable moveable);
    }
}

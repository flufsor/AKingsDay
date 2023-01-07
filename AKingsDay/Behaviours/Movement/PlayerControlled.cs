using AKingsDay.Behaviours.Movement.InputReaders;
using AKingsDay.Entities;
using AKingsDay.Entities.Movables;
using AKingsDay.States.Game;
using Microsoft.Xna.Framework;


namespace AKingsDay.Behaviours.Movement
{
    public class PlayerControlled : IBehaviour
    {
        public bool BlockMoving { get; set; }

        private readonly IInputReader _inputReader;

        public PlayerControlled()
        {
            _inputReader = new KeyboardReader();
        }

        public void Update(IMoveable moveable, GameTime gameTime)
        {
            Vector2 direction = Vector2.Zero;

            if (moveable.Status != EntityStatus.Death)
            {
                direction = _inputReader.ReadInput();
            }

            moveable.Move(gameTime, direction, moveable);
        }
        public void OnCollide(GameTime gameTime, ICollidable collider, CollisionDirection collisionDirection)
        {
            //
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AKingsDay.Behaviours.Movement.InputReaders;

public class KeyboardReader : IInputReader
{
    public bool IsDestinationInput => false;

    public Vector2 ReadInput()
    {
        KeyboardState _keyboardState = Keyboard.GetState();
        Vector2 direction = Vector2.Zero;
        if (_keyboardState.IsKeyDown(Keys.Left) || _keyboardState.IsKeyDown(Keys.Q))
        {
            direction.X -= 1;
        }

        if (_keyboardState.IsKeyDown(Keys.Right) || _keyboardState.IsKeyDown(Keys.D))
        {
            direction.X += 1;
        }

        if (_keyboardState.IsKeyDown(Keys.Space))
        {
            direction.Y = 1;
        }

        return direction;
    }
}
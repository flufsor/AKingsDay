using Microsoft.Xna.Framework;

namespace AKingsDay.Behaviours.Movement.InputReaders;

public interface IInputReader
{
    Vector2 ReadInput();
    public bool IsDestinationInput { get; }
}
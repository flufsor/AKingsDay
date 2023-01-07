using AKingsDay.Entities;
using AKingsDay.Entities.Movables;
using Microsoft.Xna.Framework;

namespace AKingsDay.States.Game
{
    public enum CollisionDirection
    {
        Left, Top, Right, Bottom
    }

    public enum CollisionType
    {
        Enemy,
        Player,
        Wall,
        Diamond
    }

    public static class CollisionManager
    {
        public static void CheckForCollisions(GameTime gameTime, IMoveable entity)
        {
            CollisionDirection collisionDirection;

            foreach (var colObject in Level.TileCollidables)
            {
                if (entity.Bounds.Intersects(colObject.Bounds))
                {
                    Vector2 intersection = Rectangle.Intersect(entity.Bounds, colObject.Bounds).Size.ToVector2();

                    if (intersection.Y < intersection.X)
                    {
                        entity.Velocity = new Vector2(entity.Velocity.X, 0);
                        if (entity.Position.Y < colObject.Bounds.Y)
                        {
                            entity.Position = new Vector2(entity.Position.X, colObject.Bounds.Y - entity.Bounds.Height);
                            collisionDirection = CollisionDirection.Top;
                        }
                        else
                        {
                            entity.Velocity = new Vector2(entity.Velocity.X, 0);
                            entity.Position = new Vector2(entity.Position.X, colObject.Bounds.Y + colObject.Bounds.Height);
                            collisionDirection = CollisionDirection.Bottom;
                        }

                    }
                    else
                    {
                        entity.Velocity = new Vector2(0, entity.Velocity.Y);
                        if (entity.Position.X < colObject.Bounds.X)
                        {
                            entity.Position = new Vector2(colObject.Bounds.X - entity.Bounds.Width, entity.Position.Y);
                            collisionDirection = CollisionDirection.Right;

                        }
                        else
                        {
                            entity.Position = new Vector2(colObject.Bounds.X + colObject.Bounds.Width, entity.Position.Y);
                            collisionDirection = CollisionDirection.Left;
                        }
                    }

                    entity.OnCollide(gameTime, colObject, collisionDirection);
                }
            }

            for (int i = 0; i < Level.Entities.Count; i++)
            {
                if (entity != Level.Entities[i] && Level.Entities[i] is ICollidable colObject)
                {
                    if (entity.Bounds.Intersects(colObject.Bounds))
                    {
                        Vector2 intersection = Rectangle.Intersect(entity.Bounds, colObject.Bounds).Size.ToVector2();
                        Vector2 newVelocity;
                        Vector2 newPosition;

                        if (intersection.Y < intersection.X)
                        {
                            newVelocity = new Vector2(entity.Velocity.X, 0);
                            newPosition = new Vector2(entity.Position.X, colObject.Bounds.Y - entity.Bounds.Height);
                            collisionDirection = CollisionDirection.Top;

                        }
                        else if (intersection.X < intersection.Y)
                        {
                            newVelocity = new Vector2(0, entity.Velocity.Y);
                            if (entity.Position.X < colObject.Bounds.X)
                            {
                                newPosition = new Vector2(colObject.Bounds.X - entity.Bounds.Width, entity.Position.Y);
                                collisionDirection = CollisionDirection.Right;

                            }
                            else
                            {
                                newPosition = new Vector2(colObject.Bounds.X + colObject.Bounds.Width, entity.Position.Y);
                                collisionDirection = CollisionDirection.Left;
                            }
                        }
                        else
                        {
                            newVelocity = new Vector2(entity.Velocity.X, 0);
                            newPosition = new Vector2(entity.Position.X, colObject.Bounds.Y + colObject.Bounds.Height);
                            collisionDirection = CollisionDirection.Bottom;
                        }

                        if (colObject.IsSolid && entity.IsSolid && !entity.Behaviour.BlockMoving)
                        {
                            entity.Position = newPosition;
                            entity.Velocity = newVelocity;
                        }

                        entity.OnCollide(gameTime, colObject, collisionDirection);
                        colObject.OnCollide(gameTime, entity, collisionDirection);
                    }
                }

            }
        }

        public static bool CheckIfFalling(IMoveable entity)
        {
            Rectangle fallHitbox = new Rectangle(entity.Bounds.X + 2, entity.Bounds.Y + entity.Bounds.Height, entity.Bounds.Width - 4, 1);

            foreach (var colObject in Level.TileCollidables)
            {
                if (colObject.Bounds.Intersects(fallHitbox))
                {
                    entity.IsFalling = false;
                    entity.Position = new Vector2(entity.Position.X, colObject.Bounds.Y - entity.Bounds.Height);
                    return false;
                }
            }

            return true;
        }
    }
}

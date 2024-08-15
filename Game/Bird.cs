using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

public class Bird
{
    const float FRICTION = 1f;
    const float SPEED = 100f;
    public static Vector2 Friction = new Vector2(FRICTION, FRICTION);

    private PhysicsObject _physicsObject;

    public Vector2 Position
    {
        get { return _physicsObject.Position; }
    }
    public Vector2 Velocity
    {
        get { return _physicsObject.Velocity; }
    }

    public Vector2 Acceleration
    {
        get { return _physicsObject.Acceleration; }
    }

    public Bird(Vector2 initialPosition)
    {
        _physicsObject = new PhysicsObject(initialPosition);
    }


    public void Update(GameTime gameTime)
    {
        //apply physics (see commit description for the link to doc) https://guide.handmadehero.org/code/day043/#1535
        float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Example of applying a force based on user input
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            _physicsObject.ApplyForce(new Vector2(-100, 0));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            _physicsObject.ApplyForce(new Vector2(100, 0));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            _physicsObject.ApplyForce(new Vector2(0, -100));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            _physicsObject.ApplyForce(new Vector2(0, 100));
        }

        _physicsObject.Update(gameTime);
    }
}
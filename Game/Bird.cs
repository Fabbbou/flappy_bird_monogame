using Microsoft.Xna.Framework;

public class Bird
{
    const float GRAVITY = 9.8f;
    public static Vector2 Gravity = new Vector2(0, GRAVITY);
    const float FRICTION = 0.1f;

    Vector2 position;
    Vector2 velocity;
    Vector2 acceleration;
    float friction;

    //constructor no fields all init to zero
    public Bird()
    {
        position = Vector2.Zero;
        acceleration = Vector2.Zero;
        velocity = Vector2.Zero;
        friction = FRICTION;
    }
    public Bird(Vector2 position, Vector2 acceleration, Vector2 velocity, float friction)
    {
        this.position = position;
        this.acceleration = acceleration;
        this.velocity = velocity;
        this.friction = friction;
    }


    public void Update(GameTime gameTime)
    {


        //apply physics see https://guide.handmadehero.org/code/day043/#3683 for more info
        float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
        acceleration += velocity * -friction;
        position += (acceleration * 0.5f * time*time) + (velocity * time);
        velocity += acceleration * time;
        
    }
}
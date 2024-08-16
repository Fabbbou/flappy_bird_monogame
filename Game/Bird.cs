using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Bird
{
    private const float SPEED = 400f;
    private const float GRAVITY = 1300f;

    public readonly PhysicsObject physicsObject;
    private AnimatedTexture _animatedTexture;

    private Vector2 _jumpForce = new Vector2(0, -SPEED);

    public Bird(Vector2 initialPosition)
    {
        physicsObject = new PhysicsObject(initialPosition);
        _animatedTexture = new AnimatedTexture(0.1f, "sprites/bird", "sprites/bird.json");
    }

    public void Load(ContentManager content)
    {
        _animatedTexture.Load(content);
        physicsObject.Gravity = new Vector2(0, GRAVITY);
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _animatedTexture.Update(gameTime);

        // crossplatform jump input
        if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
        {
            physicsObject.Velocity = _jumpForce;     
        }

        physicsObject.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
    {
        _animatedTexture.Draw(spriteBatch, physicsObject.Position);

        DrawDebug(spriteBatch, content);
    }

    private void DrawDebug(SpriteBatch spriteBatch, ContentManager content)
    {
        //drw debug of bird fields
        spriteBatch.DrawString(content.Load<SpriteFont>("fonts/04B_19"), $"Position: {physicsObject.Position}", new Vector2(10, 10), Color.White);
        spriteBatch.DrawString(content.Load<SpriteFont>("fonts/04B_19"), $"Velocity: {physicsObject.Velocity}", new Vector2(10, 30), Color.White);
        spriteBatch.DrawString(content.Load<SpriteFont>("fonts/04B_19"), $"Acceleration: {physicsObject.Acceleration}", new Vector2(10, 50), Color.White);
    }
}
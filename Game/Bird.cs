using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.ViewportAdapters;

public class Bird
{
    private const float SPEED = 400f;
    private const float GRAVITY = 0;//1300f;

    public readonly PhysicsObject physicsObject;
    private BitmapFont _font;
    private AnimatedTexture _animatedTexture = new AnimatedTexture(0.1f, "sprites/bird", "sprites/bird.json");

    private Vector2 _jumpForce = new Vector2(0, -SPEED);

    public Bird(Vector2 initialPosition)
    {
        physicsObject = PhysicsObject.Rectangle(initialPosition,17,12,ColliderType.Physics);
        physicsObject.Gravity = new Vector2(0, GRAVITY);
    }

    public void Load(ContentManager content)
    {
        _animatedTexture.Load(content);
        _font = content.Load<BitmapFont>("fonts/04b19");
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

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, ViewportAdapter viewportAdapter)
    {
        _animatedTexture.Draw(spriteBatch, physicsObject.Position);

        DrawDebug(spriteBatch, content, viewportAdapter); //to be fixed with bitmapfonts
    }

    private void DrawDebug(SpriteBatch spriteBatch, ContentManager content, ViewportAdapter viewportAdapter)
    {
        float scale = 0.5f;
        spriteBatch.DrawString(_font, $"Position: {physicsObject.Position}", new Vector2(10, 10), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        spriteBatch.DrawString(_font, $"Velocity: {physicsObject.Velocity}", new Vector2(10, 30), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        spriteBatch.DrawString(_font, $"Acceleration: {physicsObject.Acceleration}", new Vector2(10, 50), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
    }
}
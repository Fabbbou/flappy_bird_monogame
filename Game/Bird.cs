using AsepriteDotNet.Aseprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.ViewportAdapters;

public class Bird
{
    private const float SPEED = 400f;
    private const float GRAVITY = 0;//1300f;

    public readonly PhysicsObject physicsObject;
    private BitmapFont _font;
    private SpriteSheet _spriteSheet;
    private AnimatedSprite _idleCycle;
    private Vector2 _jumpForce = new Vector2(0, -SPEED);


    public Bird(Vector2 initialPosition)
    {
        physicsObject = PhysicsObject.Rectangle(initialPosition,17,12,ColliderType.Physics);
        physicsObject.Gravity = new Vector2(0, GRAVITY);
    }

    public void Load(ContentManager content, GraphicsDeviceManager graphicsDeviceManager)
    {
        _font = content.Load<BitmapFont>("fonts/04b19");
        AsepriteFile aseFile = content.Load<AsepriteFile>("sprites/bird");
        _spriteSheet = aseFile.CreateSpriteSheet(graphicsDeviceManager.GraphicsDevice);
        _idleCycle = _spriteSheet.CreateAnimatedSprite("idle"); //tag created in aseprite file selecting the frames to be animated
        _idleCycle.Play();
    }

    public void Update(GameTime gameTime, GraphicsDeviceManager graphicsDeviceManager)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _idleCycle.Update(deltaTime);

        // crossplatform jump input
        if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
        {
            physicsObject.Velocity = _jumpForce;     
        }

        physicsObject.Update(gameTime);
    }
    private Vector2 _scale;
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, ViewportAdapter viewportAdapter, GraphicsDeviceManager graphicsDeviceManager)
    {
        spriteBatch.Draw(_idleCycle, physicsObject.Position);

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
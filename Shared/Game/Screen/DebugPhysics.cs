using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;

public class DebugPhysics : GameScreen
{
    protected ScalingViewportAdapter ViewportAdapter;
    private SpriteBatch _spriteBatch;
    private OrthographicCamera _camera;

    //to create a pixel texture for filled rectangle
    private Texture2D pixelTexture;

    private PhysicsObject partFloor1;
    private PhysicsObject partFloor2;
    private PhysicsObject partFloor3;
    private PhysicsObject partFloor4;
    private PhysicsObject partFloor5;
    private PhysicsObject partFloor6;
    private PhysicsObject partFloor7;

    private PhysicsObject movingBox;

    public DebugPhysics(Game game) : base(game) { }

    public override void LoadContent()
    {
        Game.IsMouseVisible = true;
        Main.Instance.Graphics.PreferredBackBufferWidth = Constants.DEBUG_SCREEN_WIDTH;
        Main.Instance.Graphics.PreferredBackBufferHeight = Constants.DEBUG_SCREEN_HEIGHT;
        Main.Instance.Graphics.ApplyChanges();

        ViewportAdapter = new ScalingViewportAdapter(GraphicsDevice, Constants.DEBUG_WORLD_WIDTH, Constants.DEBUG_WORLD_WIDTH);
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _camera = new OrthographicCamera(ViewportAdapter);
        _camera.ZoomOut(0.1f);

        //set the filled color texture
        // Create a 1x1 pixel texture
        pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White }); //the color here is not important, as we will change it later

        var yFloor = 390;
        var xFloor = 0;
        var floorHeigth = 100;
        var smallFloorWidth = 10;
        partFloor1 = new("partFloor1", xFloor, yFloor, 100, floorHeigth, CollisionType.Static);
        xFloor += 100;
        partFloor2 = new("partFloor2", xFloor, yFloor, smallFloorWidth, floorHeigth, CollisionType.Static);
        xFloor += smallFloorWidth;
        partFloor3 = new("partFloor3", xFloor, yFloor, smallFloorWidth, floorHeigth, CollisionType.Static);
        xFloor += smallFloorWidth;
        partFloor4 = new("partFloor4", xFloor, yFloor, smallFloorWidth, floorHeigth, CollisionType.Static);
        xFloor += smallFloorWidth;
        partFloor5 = new("partFloor5", xFloor, yFloor, smallFloorWidth, floorHeigth, CollisionType.Static);
        xFloor += smallFloorWidth;
        partFloor6 = new("partFloor6", xFloor, yFloor, smallFloorWidth, floorHeigth, CollisionType.Static);
        xFloor += smallFloorWidth;
        partFloor7 = new("partFloor7", xFloor, yFloor, Constants.DEBUG_WORLD_WIDTH - xFloor, floorHeigth, CollisionType.Static);

        var boxSize = 10;
        movingBox = new("movingBox", 0, 0, boxSize, boxSize, CollisionType.Moving);
        movingBox.Gravity = Vector2.Zero;

    }

    public override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Game.Exit();

        //arrow to move the box using addForce
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            movingBox.ApplyForce(new Vector2(0, -1000), ForceType.Continuous);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            movingBox.ApplyForce(new Vector2(0, 1000), ForceType.Continuous);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            movingBox.ApplyForce(new Vector2(-1000, 0), ForceType.Continuous);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            movingBox.ApplyForce(new Vector2(1000, 0), ForceType.Continuous);
        }
        //spacebar to reset the velocity of the box
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            movingBox.Velocity = Vector2.Zero;
        }
        PhysicsEngine.Instance.MoveAndSlide(movingBox, gameTime);

        PhysicsEngine.Instance.Update(gameTime);
    }

    protected virtual Matrix GetTransformMatrix()
    {
        return _camera.GetViewMatrix();
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(transformMatrix: GetTransformMatrix(), samplerState: SamplerState.PointClamp);
        //draw a rectangle filled with blue color
        _spriteBatch.Draw(pixelTexture, new Rectangle(0, 0, Constants.DEBUG_WORLD_WIDTH, Constants.DEBUG_WORLD_HEIGHT), Color.Blue);

        PhysicsDebug.Instance.Draw(_spriteBatch);
        _spriteBatch.End();
    }
}



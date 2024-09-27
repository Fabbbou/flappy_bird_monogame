using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using System;
using static System.Net.Mime.MediaTypeNames;

public class DebugPhysics : GameScreen
{
    protected ScalingViewportAdapter ViewportAdapter;
    private SpriteBatch _spriteBatch;
    private OrthographicCamera _camera;

    //to create a pixel texture for filled rectangle
    private Texture2D pixelTexture;
    private BitmapFont _font;

    private PhysicsObject partFloor1;
    private PhysicsObject partFloor2;
    private PhysicsObject partFloor3;
    private PhysicsObject partFloor4;
    private PhysicsObject partFloor5;
    private PhysicsObject partFloor6;
    private PhysicsObject partFloor7;

    private PhysicsObject movingBox;
    private PhysicsObject movingCircle;

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
        _font = Content.Load<BitmapFont>("fonts/04b19");

        var yFloor = 390;
        var xFloor = 0;
        var floorHeigth = 100;
        var smallFloorWidth = 10;
        partFloor1 = PhysicsObjectFactory.Rect("partFloor1", xFloor, yFloor, CollisionType.Static, 100, floorHeigth);
        xFloor += 100;
        partFloor2 = PhysicsObjectFactory.Rect("partFloor2", xFloor, yFloor, CollisionType.Static, smallFloorWidth, floorHeigth);
        xFloor += smallFloorWidth;
        partFloor3 = PhysicsObjectFactory.Rect("partFloor3", xFloor, yFloor, CollisionType.Static, smallFloorWidth, floorHeigth);
        xFloor += smallFloorWidth;
        partFloor4 = PhysicsObjectFactory.Rect("partFloor4", xFloor, yFloor, CollisionType.Static, smallFloorWidth, floorHeigth);
        xFloor += smallFloorWidth;
        partFloor5 = PhysicsObjectFactory.Rect("partFloor5", xFloor, yFloor,  CollisionType.Static, smallFloorWidth, floorHeigth);
        xFloor += smallFloorWidth;
        partFloor6 = PhysicsObjectFactory.Rect("partFloor6", xFloor, yFloor, CollisionType.Static, smallFloorWidth, floorHeigth);
        xFloor += smallFloorWidth;
        partFloor7 = PhysicsObjectFactory.Rect("partFloor7", xFloor, yFloor, CollisionType.Static, Constants.DEBUG_WORLD_WIDTH - xFloor, floorHeigth);

        var boxSize = 10;
        var circleRadius = 10;
        movingBox = PhysicsObjectFactory.Rect("movingBox", 40, 80, CollisionType.Moving, boxSize, boxSize);
        movingBox.Gravity = Vector2.Zero;

        movingCircle = PhysicsObjectFactory.Circl("movingCircle", 80, 80, CollisionType.Moving, circleRadius);
        movingCircle.Gravity = Vector2.Zero;

    }
    public override void UnloadContent()
    {
        pixelTexture.Dispose();
    }
    public override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Game.Exit();

        KeyboardState keyboardState = Keyboard.GetState();
        //arrow to move the box using addForce
        if (keyboardState.IsKeyDown(Keys.Up))
        {
            movingBox.ApplyForce(new Vector2(0, -1000), ForceType.Continuous);
        }
        if (keyboardState.IsKeyDown(Keys.Down))
        {
            movingBox.ApplyForce(new Vector2(0, 1000), ForceType.Continuous);
        }
        if (keyboardState.IsKeyDown(Keys.Left))
        {
            movingBox.ApplyForce(new Vector2(-1000, 0), ForceType.Continuous);
        }
        if (keyboardState.IsKeyDown(Keys.Right))
        {
            movingBox.ApplyForce(new Vector2(1000, 0), ForceType.Continuous);
        }
        //spacebar to reset the velocity of the box
        if (keyboardState.IsKeyDown(Keys.Space))
        {
            //movingBox.Velocity = Vector2.Zero;
            movingCircle.Velocity = Vector2.Zero;
        }
        //arrow to move the box using addForce
        if (keyboardState.IsKeyDown(Keys.W))
        {
            //print when button pressed
            movingCircle.ApplyForce(new Vector2(0, -1000), ForceType.Continuous);
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            movingCircle.ApplyForce(new Vector2(0, 1000), ForceType.Continuous);
        }
        if (keyboardState.IsKeyDown(Keys.A))
        {
            movingCircle.ApplyForce(new Vector2(-1000, 0), ForceType.Continuous);
        }
        if (keyboardState.IsKeyDown(Keys.D))
        {
            movingCircle.ApplyForce(new Vector2(1000, 0), ForceType.Continuous);
        }


        PhysicsEngine.Instance.MoveAndSlide(movingBox, gameTime);
        PhysicsEngine.Instance.MoveAndSlide(movingCircle, gameTime);

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
        //draw moving box tostring to see the velocity
        _spriteBatch.DrawString(_font, "box vel: "+movingBox.Velocity.ToString(), new Vector2(0, 0), Color.White);
        _spriteBatch.DrawString(_font, "circle vel: " + movingCircle.Velocity.ToString(), new Vector2(0, 20), Color.White);
        PhysicsDebug.Instance.Draw(_spriteBatch);
        _spriteBatch.End();
    }
}



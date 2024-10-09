using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using static Constants;

public class MenuScreen : GameScreen
{
    private BoxingViewportAdapter ViewportAdapter;
    private OrthographicCamera _camera;
    private SpriteBatch _spriteBatch;

    private Texture2DRegion _background;
    private Texture2DRegion _flappybirdTitle;
    private Texture2DRegion _playButtonTexture;

    private ClickableRegionHandler _playButtonClickHandler;

    private Entity _entity;
    private Floor _floor;

    public MenuScreen(Game game) : base(game) { }

    public override void Initialize()
    {
        ViewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, Constants.WORLD_WIDTH, Constants.WORLD_HEIGHT);
        Game.IsMouseVisible = true;
        _camera = new OrthographicCamera(ViewportAdapter);
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _entity = new Entity();
        _playButtonClickHandler = new ClickableRegionHandler(_entity, _camera, OnClickPlay, new(SPRITE_POSITION_PLAY_BUTTON_MENU.ToPoint(), ATLAS_SIZE_PLAY_BUTTON.ToPoint()));
    }

    public override void LoadContent()
    {
        _background = AssetsLoader.Instance.Background;
        _flappybirdTitle = AssetsLoader.Instance.FlappyBirdLogo;
        _playButtonTexture = AssetsLoader.Instance.PlayButton;
        _flappybirdTitle = AssetsLoader.Instance.FlappyBirdLogo;
        _floor = new Floor();
        _floor.LoadContent(Game.Content);
    }

    public override void UnloadContent()
    {
        GizmosRegistry.Instance.Clear();
        ClickRegistry.Instance.Clear();
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _floor.Draw(_spriteBatch);
        _spriteBatch.Draw(_flappybirdTitle, SPRITE_POSITION_MAIN_SCREEN_LOGO_FLAPPYBIRD, Color.White);
        _spriteBatch.Draw(_playButtonTexture, SPRITE_POSITION_PLAY_BUTTON_MENU, Color.White);

        GizmosRegistry.Instance.Draw(_spriteBatch);

        _spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        ClickRegistry.Instance.Update(gameTime);
        _floor.Update(gameTime);
    }

    private void OnClickPlay()
    {
        Main.Instance.LoadScreen(ScreenName.MainGameScreen);
    }
}
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
    private SpriteBatch _spriteBatch;
    private ScreenHandler _screenHandler;

    private Texture2DRegion _background;
    private Texture2DRegion _flappybirdTitle;
    private Texture2DRegion _playButtonTexture;
    private ClickableRegionHandler _playButtonClickHandler;

    private Entity _entity;
    private Floor _floor;

    public MenuScreen(Game game, ScreenHandler screenHandler) : base(game)
    {
        _screenHandler = screenHandler;
    }

    public override void Initialize()
    {
        ViewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, Constants.WORLD_WIDTH, Constants.WORLD_HEIGHT);
        Game.IsMouseVisible = true;
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _entity = new Entity();
        _playButtonClickHandler = new ClickableRegionHandler(_entity, OnClickPlay, new(SPRITE_POSITION_PLAY_BUTTON_MENU.ToPoint(), ATLAS_SIZE_PLAY_BUTTON.ToPoint()));
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
        GraphicsDevice.Clear(Color.Red);

        _spriteBatch.Begin(transformMatrix: _screenHandler.GetViewMatrix(), samplerState: SamplerState.PointClamp);

        Extensions.SpriteBatchExtensions.Draw(_spriteBatch, _background, Vector2.Zero, LAYER_DEPTH_UI);
        _floor.Draw(_spriteBatch);
        Extensions.SpriteBatchExtensions.Draw(_spriteBatch, _flappybirdTitle, SPRITE_POSITION_MAIN_SCREEN_LOGO_FLAPPYBIRD, LAYER_DEPTH_UI);
        Extensions.SpriteBatchExtensions.Draw(_spriteBatch, _playButtonTexture, SPRITE_POSITION_PLAY_BUTTON_MENU, LAYER_DEPTH_UI);
        _spriteBatch.End();

        GizmosRegistry.Instance.Draw();
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
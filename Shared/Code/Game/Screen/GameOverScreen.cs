using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using static Constants;

public class GameOverScreen : GameScreen
{
    private BoxingViewportAdapter ViewportAdapter;
    private OrthographicCamera _camera;
    private SpriteBatch _spriteBatch;

    private Texture2DRegion _background;
    private Texture2DRegion _gameOverTexture;
    private Texture2DRegion _playButtonTexture;
    private Texture2DRegion _scoreButtonTexture;
    private Texture2DRegion _menuButtonTexture;

    private ClickableRegionHandler _playButtonClickHandler;
    private ClickableRegionHandler _scoreButtonClickHandler;
    private ClickableRegionHandler _menuButtonClickHandler;

    private Entity _entity;

    public GameOverScreen(Game game) : base(game){}

    public override void Initialize()
    {
        ViewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, Constants.WORLD_WIDTH, Constants.WORLD_HEIGHT);
        Game.IsMouseVisible = true;
        _camera = new OrthographicCamera(ViewportAdapter);
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _entity = new Entity();
        _playButtonClickHandler = new ClickableRegionHandler(_entity, _camera, OnClickPlay, new(SPRITE_POSITION_PLAY_BUTTON_GAMEOVER.ToPoint(), ATLAS_SIZE_PLAY_BUTTON.ToPoint()));
        _scoreButtonClickHandler = new ClickableRegionHandler(_entity, _camera, OnClickScore, new(SPRITE_POSITION_SCORE_BUTTON_GAMEOVER.ToPoint(), ATLAS_SIZE_SCORE_BUTTON.ToPoint()));
        _menuButtonClickHandler = new ClickableRegionHandler(_entity, _camera, OnClickMenu, new(CLICK_REGION_POSITION_GAMEOVER_MENU_BUTTON.ToPoint(), CLICK_REGION_SIZE_GAMEOVER_MENU_BUTTON.ToPoint()));
    }

    public override void LoadContent()
    {
        AssetsLoader.Instance.LoadContent(Game.Content);
        _background = AssetsLoader.Instance.Background;
        _gameOverTexture = AssetsLoader.Instance.GameOver;
        _playButtonTexture = AssetsLoader.Instance.PlayButton;
        _scoreButtonTexture = AssetsLoader.Instance.ScoreButton;
        _menuButtonTexture = AssetsLoader.Instance.MenuButton;
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
        _spriteBatch.Draw(_gameOverTexture, SPRITE_POSITION_GAMEOVER, Color.White);
        _spriteBatch.Draw(_playButtonTexture, SPRITE_POSITION_PLAY_BUTTON_GAMEOVER, Color.White);
        _spriteBatch.Draw(_scoreButtonTexture, SPRITE_POSITION_SCORE_BUTTON_GAMEOVER, Color.White);
        _spriteBatch.Draw(_menuButtonTexture, SPRITE_POSITION_MENU_BUTTON_GAMEOVER, Color.White);
        
        _spriteBatch.End();
    }

    public override void Update(GameTime gameTime) 
    {
        ClickRegistry.Instance.Update(gameTime);
    }

    private void OnClickPlay()
    {
        Main.Instance.LoadScreen(ScreenName.MainGameScreen);
    }
    private void OnClickScore()
    {
    }
    private void OnClickMenu()
    {
        Main.Instance.LoadScreen(ScreenName.MenuScreen);
    }
}
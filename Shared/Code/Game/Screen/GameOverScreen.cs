using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using static Constants;

public class GameOverScreen : GameScreen
{
    private BoxingViewportAdapter ViewportAdapter;
    private SpriteBatch _spriteBatch;
    private ScreenHandler _screenHandler;


    //UI
    private Texture2DRegion _background;
    private Texture2DRegion _gameOverTitle;
    private Texture2DRegion _uIScore;
    private Texture2DRegion _badgeScore;
    private Texture2DRegion _badgeNew;
    private BitmapFont _font;

    //Buttons
    private Texture2DRegion _playButtonTexture;
    private Texture2DRegion _menuButtonTexture;
    private ClickableRegionHandler _playButtonClickHandler;
    private ClickableRegionHandler _menuButtonClickHandler;

    private Entity _entityClickableZones;

    private ScoreManager.Score _score;

    public GameOverScreen(Game game, ScreenHandler screenHandler) : base(game)
    {
        _screenHandler = screenHandler;
    }

    public override void Initialize()
    {
        ViewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, Constants.WORLD_WIDTH, Constants.WORLD_HEIGHT);
        Game.IsMouseVisible = true;
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _entityClickableZones = new Entity();
        _playButtonClickHandler = new ClickableRegionHandler(_entityClickableZones, OnClickPlay, new(SPRITE_POSITION_PLAY_BUTTON_GAMEOVER.ToPoint(), ATLAS_SIZE_PLAY_BUTTON.ToPoint()));
        _menuButtonClickHandler = new ClickableRegionHandler(_entityClickableZones, OnClickMenu, new(CLICK_REGION_POSITION_GAMEOVER_MENU_BUTTON.ToPoint(), CLICK_REGION_SIZE_GAMEOVER_MENU_BUTTON.ToPoint()));
    }

    public override void LoadContent()
    {
        AssetsLoader.Instance.LoadContent(Game.Content);
        _background = AssetsLoader.Instance.Background;
        _gameOverTitle = AssetsLoader.Instance.GameOver;
        _playButtonTexture = AssetsLoader.Instance.PlayButton;
        _menuButtonTexture = AssetsLoader.Instance.MenuButton;
        _uIScore = AssetsLoader.Instance.UIScore;
        _font = AssetsLoader.Instance.Font;
        _badgeNew = AssetsLoader.Instance.BadgeNew;

        //load the best score badge if needed
        _score = ScoreManager.Instance.SaveScores();
        if (_score.ScoreRank == ScoreManager.ScoreRank.First)
        {
            _badgeScore = AssetsLoader.Instance.ScoreBadgeFirst;
        }
        else if (_score.ScoreRank == ScoreManager.ScoreRank.Second)
        {
            _badgeScore = AssetsLoader.Instance.ScoreBadgeSecond;
        }
        else if (_score.ScoreRank == ScoreManager.ScoreRank.Third)
        {
            _badgeScore = AssetsLoader.Instance.ScoreBadgeThird;
        }
    }

    public override void UnloadContent()
    {
        GizmosRegistry.Instance.Clear();
        ClickRegistry.Instance.Clear();
        _badgeScore = null;
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(transformMatrix: _screenHandler.GetViewMatrix(), samplerState: SamplerState.PointClamp);

        Extensions.SpriteBatchExtensions.Draw(_spriteBatch, _background, Vector2.Zero, LAYER_DEPTH_UI);
        Extensions.SpriteBatchExtensions.Draw(_spriteBatch,_gameOverTitle, SPRITE_POSITION_GAMEOVER, LAYER_DEPTH_UI);
        Extensions.SpriteBatchExtensions.Draw(_spriteBatch, _playButtonTexture, SPRITE_POSITION_PLAY_BUTTON_GAMEOVER, LAYER_DEPTH_UI);

        Extensions.SpriteBatchExtensions.Draw(_spriteBatch, _uIScore, SPRITE_POSITION_SCORE_UI, LAYER_DEPTH_UI);
        if(_badgeScore != null)
        {
            _spriteBatch.Draw(_badgeScore, SPRITE_POSITION_SCORE_BADGE, Color.White);
        }
        if(_score.IsNewScore)
        {
            _spriteBatch.Draw(_badgeNew, SPRITE_POSITION_NEW_BADGE, Color.White);
        }
        _spriteBatch.DrawString(_font, _score.Value.ToString(), TEXT_POSITION_SCORE_CURRENT, Color.White, layerDepth: LAYER_DEPTH_UI);    
        _spriteBatch.DrawString(_font, _score.Best.ToString(), TEXT_POSITION_SCORE_BEST, Color.White, layerDepth: LAYER_DEPTH_UI);
        Extensions.SpriteBatchExtensions.Draw(_spriteBatch, _menuButtonTexture, SPRITE_POSITION_MENU_BUTTON_GAMEOVER, LAYER_DEPTH_UI);


        _spriteBatch.End();
        GizmosRegistry.Instance.Draw();
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
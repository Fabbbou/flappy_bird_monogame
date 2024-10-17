using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using static Constants;
using Extensions;
using MonoGame.Extended;

public class MenuScreen : GameScreen
{
    private SpriteBatch _spriteBatch;

    private Texture2DRegion _background;
    private Texture2DRegion _flappybirdTitle;
    private Texture2DRegion _playButtonTexture;
    private ClickableRegionHandler _playButtonClickHandler;

    private Entity _entity;
    private Floor _floor;

    public MenuScreen(Game game) : base(game) {}

    public override void Initialize()
    {
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
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        //the upper sky
        _spriteBatch.FillRectangle(new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height / 2), COLOR_SKY);
        _spriteBatch.End();

        _spriteBatch.Begin(transformMatrix: MainRegistry.I.GetScaleMatrix(), samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(_background, Vector2.Zero, LAYER_DEPTH_UI);

        _spriteBatch.FillRectangle(new Rectangle(0, (int)SPRITE_POSITION_FLOOR.Y, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), COLOR_FLOOR);
        _floor.Draw(_spriteBatch);

        _spriteBatch.Draw(_flappybirdTitle, SPRITE_POSITION_MAIN_SCREEN_LOGO_FLAPPYBIRD, LAYER_DEPTH_UI);
        _spriteBatch.Draw(_playButtonTexture, SPRITE_POSITION_PLAY_BUTTON_MENU, LAYER_DEPTH_UI);
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
        MainRegistry.I.ScreenRegistry.LoadScreen(ScreenName.MainGameScreen);
    }
}
using flappyrogue_mg.Core;
using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using static Constants;

public class SoundUI : GameEntity
{
    private MainGameScreen _mainGameScreen;
    private Texture2DRegion _barSound;

    private Texture2DRegion _okButton;
    private ClickableRegionHandler _okClickableRegionHandler;
    private Texture2DRegion _menuButton;
    private ClickableRegionHandler _menuClickableRegionHandler;
    private Texture2DRegion _uiSettings;

    private ClickableRegionHandler _fxMinusClickableRegionHandler;
    private ClickableRegionHandler _fxPlusClickableRegionHandler;
    private ClickableRegionHandler _fxMuteClickableRegionHandler;
    private ClickableRegionHandler _musicMinusClickableRegionHandler;
    private ClickableRegionHandler _musicPlusClickableRegionHandler;
    private ClickableRegionHandler _musicMuteClickableRegionHandler;

    public int FxVolume { get; private set; }
    public int MusicVolume { get; private set; }

    private FilledRectangle _background;
    public SoundUI(MainGameScreen mainGameScreen)
    {
        IsActive = false;
        _mainGameScreen = mainGameScreen;
        _background = new FilledRectangle(mainGameScreen.GraphicsDevice, new Rectangle(0, 0, WORLD_WIDTH, WORLD_HEIGHT), new Color(.6f, .6f, .6f, .9f));
        FxVolume = (int)(SoundManager.Instance.VolumeFX * 10);
        MusicVolume = (int)(SoundManager.Instance.VolumeMusic * 10);

        _okClickableRegionHandler = new ClickableRegionHandler(Entity, mainGameScreen.Camera, OnClickOk, new(CLICK_REGION_POSITION_OK_BUTTON.ToPoint(), CLICK_REGION_SIZE_OK_BUTTON.ToPoint()));
        _menuClickableRegionHandler = new ClickableRegionHandler(Entity, mainGameScreen.Camera, OnClickMenu, new(CLICK_REGION_POSITION_MENU_BUTTON.ToPoint(), CLICK_REGION_SIZE_MENU_BUTTON.ToPoint()));
        
        _fxMinusClickableRegionHandler = new ClickableRegionHandler(Entity, mainGameScreen.Camera, OnClickFxMinus, new(CLICK_REGION_POSITION_MINUS_BUTTON_FX.ToPoint(), CLICK_REGION_SIZE_MINUS_BUTTON.ToPoint()));
        _fxPlusClickableRegionHandler = new ClickableRegionHandler(Entity, mainGameScreen.Camera, OnClickFxPlus, new(CLICK_REGION_POSITION_PLUS_BUTTON_FX.ToPoint(), CLICK_REGION_SIZE_PLUS_BUTTON.ToPoint()));
        _fxMuteClickableRegionHandler = new ClickableRegionHandler(Entity, mainGameScreen.Camera, OnClickFxMute, new(CLICK_REGION_POSITION_LOGO_FX.ToPoint(), CLICK_REGION_SIZE_LOGO_FX.ToPoint()));
        
        _musicMinusClickableRegionHandler = new ClickableRegionHandler(Entity, mainGameScreen.Camera, OnClickMusicMinus, new(CLICK_REGION_POSITION_MINUS_BUTTON_MUSIC.ToPoint(), CLICK_REGION_SIZE_MINUS_BUTTON.ToPoint()));
        _musicPlusClickableRegionHandler = new ClickableRegionHandler(Entity, mainGameScreen.Camera, OnClickMusicPlus, new(CLICK_REGION_POSITION_PLUS_BUTTON_MUSIC.ToPoint(), CLICK_REGION_SIZE_PLUS_BUTTON.ToPoint()));
        _musicMuteClickableRegionHandler = new ClickableRegionHandler(Entity, mainGameScreen.Camera, OnClickMusicMute, new(CLICK_REGION_POSITION_LOGO_MUSIC.ToPoint(), CLICK_REGION_SIZE_LOGO_MUSIC.ToPoint()));
    }

    public override void LoadContent(ContentManager content)
    {
        _background.LoadContent(content);
        _barSound = PreloadedAssets.Instance.BarSound;
        _okButton = PreloadedAssets.Instance.OkButton;
        _menuButton = PreloadedAssets.Instance.MenuButton;
        _uiSettings = PreloadedAssets.Instance.UiSSettings;
    }

    public override void Update(GameTime gameTime) {}

    public override void Draw(SpriteBatch spriteBatch)
    {
        _background.Draw(spriteBatch);
        spriteBatch.Draw(_okButton, SPRITE_POSITION_OK_BUTTON, Color.White);
        spriteBatch.Draw(_menuButton, SPRITE_POSITION_MENU_BUTTON, Color.White);
        spriteBatch.Draw(_uiSettings, SPRITE_POSITION_UI_SETTINGS, Color.White);
        DrawFxBars(spriteBatch);
        DrawMusicBars(spriteBatch);
    }

    private void DrawFxBars(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < FxVolume; i++)
        {
            spriteBatch.Draw(_barSound, new Vector2(POSITION_BARS_FX.X + (ATLAS_SIZE_BAR_SOUND.X + SETTINGS_UI_SPACE_BETWEEN_BARS) * i, POSITION_BARS_FX.Y), Color.White);
        }
    }

    private void DrawMusicBars(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < MusicVolume; i++)
        {
            spriteBatch.Draw(_barSound, new Vector2(POSITION_BARS_MUSIC.X + (ATLAS_SIZE_BAR_SOUND.X + SETTINGS_UI_SPACE_BETWEEN_BARS) * i, POSITION_BARS_MUSIC.Y), Color.White);
        }
    }

    public void OnClickOk()
    {
        _mainGameScreen.StateMachine.ChangeState(new PlayState(_mainGameScreen));
    }
    public void OnClickMenu()
    {
    }
    public void OnClickFxMinus()
    {
        FxVolume = MathHelper.Clamp(FxVolume - 1, 0, 10);
        SoundManager.Instance.SetVolumeFX(FxVolume / 10f);
    }
    public void OnClickFxPlus()
    {
        FxVolume = MathHelper.Clamp(FxVolume + 1, 0, 10);
        SoundManager.Instance.SetVolumeFX(FxVolume / 10f);
    }
    public void OnClickFxMute()
    {
        FxVolume = 0;
        SoundManager.Instance.SetVolumeFX(0);
    }
    public void OnClickMusicMinus()
    {
        MusicVolume = MathHelper.Clamp(MusicVolume - 1, 0, 10);
        SoundManager.Instance.SetVolumeMusic(MusicVolume / 10f);
    }
    public void OnClickMusicPlus()
    {
        MusicVolume = MathHelper.Clamp(MusicVolume + 1, 0, 10);
        SoundManager.Instance.SetVolumeMusic(MusicVolume / 10f);
    }
    public void OnClickMusicMute()
    {
        MusicVolume = 0;
        SoundManager.Instance.SetVolumeMusic(0);
    }
}
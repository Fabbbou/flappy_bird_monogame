using flappyrogue_mg.Core;
using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using static Constants;
using Extensions;

public class SoundUI : GameEntity
{
    private MainGameScreen _mainGameScreen;
    private Texture2DRegion _barSound;

    private Texture2DRegion _okButton;
    private ClickableRegionHandler _okClickableRegionHandler;
    private Texture2DRegion _menuButton;
    private ClickableRegionHandler _menuClickableRegionHandler;
    private Texture2DRegion _uiSettings;
    private Texture2DRegion _flappyBirdLogo;

    private ClickableRegionHandler _fxMinusClickableRegionHandler;
    private ClickableRegionHandler _fxPlusClickableRegionHandler;
    private ClickableRegionHandler _fxMuteClickableRegionHandler;
    private ClickableRegionHandler _musicMinusClickableRegionHandler;
    private ClickableRegionHandler _musicPlusClickableRegionHandler;
    private ClickableRegionHandler _musicMuteClickableRegionHandler;

    public int FxVolume { get; private set; }
    public int MusicVolume { get; private set; }
    public SoundUI(MainGameScreen mainGameScreen)
    {
        IsActive = false;
        _mainGameScreen = mainGameScreen;

        _okClickableRegionHandler = new ClickableRegionHandler(this, OnClickOk, new(CLICK_REGION_POSITION_OK_BUTTON.ToPoint(), CLICK_REGION_SIZE_OK_BUTTON.ToPoint()));
        _menuClickableRegionHandler = new ClickableRegionHandler(this, OnClickMenu, new(CLICK_REGION_SOUND_UI_POSITION_MENU_BUTTON.ToPoint(), CLICK_REGION_SOUND_UI_SIZE_MENU_BUTTON.ToPoint()));
        
        _fxMinusClickableRegionHandler = new ClickableRegionHandler(this, OnClickFxMinus, new(CLICK_REGION_POSITION_MINUS_BUTTON_FX.ToPoint(), CLICK_REGION_SIZE_MINUS_BUTTON.ToPoint()));
        _fxPlusClickableRegionHandler = new ClickableRegionHandler(this, OnClickFxPlus, new(CLICK_REGION_POSITION_PLUS_BUTTON_FX.ToPoint(), CLICK_REGION_SIZE_PLUS_BUTTON.ToPoint()));
        _fxMuteClickableRegionHandler = new ClickableRegionHandler(this, OnClickFxMute, new(CLICK_REGION_POSITION_LOGO_FX.ToPoint(), CLICK_REGION_SIZE_LOGO_FX.ToPoint()));
        
        _musicMinusClickableRegionHandler = new ClickableRegionHandler(this, OnClickMusicMinus, new(CLICK_REGION_POSITION_MINUS_BUTTON_MUSIC.ToPoint(), CLICK_REGION_SIZE_MINUS_BUTTON.ToPoint()));
        _musicPlusClickableRegionHandler = new ClickableRegionHandler(this, OnClickMusicPlus, new(CLICK_REGION_POSITION_PLUS_BUTTON_MUSIC.ToPoint(), CLICK_REGION_SIZE_PLUS_BUTTON.ToPoint()));
        _musicMuteClickableRegionHandler = new ClickableRegionHandler(this, OnClickMusicMute, new(CLICK_REGION_POSITION_LOGO_MUSIC.ToPoint(), CLICK_REGION_SIZE_LOGO_MUSIC.ToPoint()));
    }

    public override void LoadContent(ContentManager content)
    {
        _flappyBirdLogo = AssetsLoader.Instance.FlappyBirdLogo;
        _barSound = AssetsLoader.Instance.BarSound;
        _okButton = AssetsLoader.Instance.OkButton;
        _menuButton = AssetsLoader.Instance.MenuButton;
        _uiSettings = AssetsLoader.Instance.UiSSettings;

        FxVolume = (int)(SoundManager.Instance.VolumeFX * 10);
        MusicVolume = (int)(SoundManager.Instance.VolumeMusic * 10);
    }

    public override void Update(GameTime gameTime) {}

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_flappyBirdLogo, SPRITE_POSITION_SOUNDUI_LOGO_FLAPPYBIRD, LAYER_DEPTH_UI);
        spriteBatch.Draw(_okButton, SPRITE_POSITION_OK_BUTTON, LAYER_DEPTH_UI);
        spriteBatch.Draw(_menuButton, SPRITE_POSITION_MENU_BUTTON_SOUND_UI, LAYER_DEPTH_UI);
        spriteBatch.Draw(_uiSettings, SPRITE_POSITION_UI_SETTINGS, LAYER_DEPTH_UI);
        DrawFxBars(spriteBatch);
        DrawMusicBars(spriteBatch);
    }

    private void DrawFxBars(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < FxVolume; i++)
        {
            spriteBatch.Draw(_barSound, new Vector2(POSITION_BARS_FX.X + (ATLAS_SIZE_BAR_SOUND.X + SETTINGS_UI_SPACE_BETWEEN_BARS) * i, POSITION_BARS_FX.Y), LAYER_DEPTH_UI);
        }
    }

    private void DrawMusicBars(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < MusicVolume; i++)
        {
            spriteBatch.Draw(_barSound, new Vector2(POSITION_BARS_MUSIC.X + (ATLAS_SIZE_BAR_SOUND.X + SETTINGS_UI_SPACE_BETWEEN_BARS) * i, POSITION_BARS_MUSIC.Y), LAYER_DEPTH_UI);
        }
    }

    public void OnClickOk()
    {
        _mainGameScreen.StateMachine.ChangeState(new PlayState(_mainGameScreen));
    }
    public void OnClickMenu()
    {
        MainRegistry.I.ScreenRegistry.LoadScreen(ScreenName.MenuScreen);
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
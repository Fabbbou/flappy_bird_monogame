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

        _okClickableRegionHandler = new ClickableRegionHandler(mainGameScreen.Camera, OnClickOk, new(POSITION_OK_BUTTON.ToPoint(), SIZE_OK_BUTTON.ToPoint()));
        _menuClickableRegionHandler = new ClickableRegionHandler(mainGameScreen.Camera, OnClickMenu, new(POSITION_MENU_BUTTON.ToPoint(), SIZE_MENU_BUTTON.ToPoint()));
        
        _fxMinusClickableRegionHandler = new ClickableRegionHandler(mainGameScreen.Camera, OnClickFxMinus, new(POSITION_MINUS_BUTTON_FX.ToPoint(), SIZE_MINUS_BUTTON.ToPoint()));
        _fxPlusClickableRegionHandler = new ClickableRegionHandler(mainGameScreen.Camera, OnClickFxPlus, new(POSITION_PLUS_BUTTON_FX.ToPoint(), SIZE_PLUS_BUTTON.ToPoint()));
        _fxMuteClickableRegionHandler = new ClickableRegionHandler(mainGameScreen.Camera, OnClickFxMute, new(POSITION_LOGO_FX.ToPoint(), SIZE_LOGO_FX.ToPoint()));
        
        _musicMinusClickableRegionHandler = new ClickableRegionHandler(mainGameScreen.Camera, OnClickMusicMinus, new(POSITION_MINUS_BUTTON_MUSIC.ToPoint(), SIZE_MINUS_BUTTON.ToPoint()));
        _musicPlusClickableRegionHandler = new ClickableRegionHandler(mainGameScreen.Camera, OnClickMusicPlus, new(POSITION_PLUS_BUTTON_MUSIC.ToPoint(), SIZE_PLUS_BUTTON.ToPoint()));
        _musicMuteClickableRegionHandler = new ClickableRegionHandler(mainGameScreen.Camera, OnClickMusicMute, new(POSITION_LOGO_MUSIC.ToPoint(), SIZE_LOGO_MUSIC.ToPoint()));
    }

    public override void LoadContent(ContentManager content)
    {
        _background.LoadContent(content);
        _barSound = PreloadedAssets.Instance.BarSound;
        _okButton = PreloadedAssets.Instance.OkButton;
        _menuButton = PreloadedAssets.Instance.MenuButton;
        _uiSettings = PreloadedAssets.Instance.UiSSettings;
    }

    public override void Update(GameTime gameTime)
    {
        _okClickableRegionHandler.Update(gameTime);
        _menuClickableRegionHandler.Update(gameTime);
        _fxMinusClickableRegionHandler.Update(gameTime);
        _fxPlusClickableRegionHandler.Update(gameTime);
        _fxMuteClickableRegionHandler.Update(gameTime);
        _musicMinusClickableRegionHandler.Update(gameTime);
        _musicPlusClickableRegionHandler.Update(gameTime);
        _musicMuteClickableRegionHandler.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _background.Draw(spriteBatch);
        spriteBatch.Draw(_okButton, POSITION_OK_BUTTON, Color.White);
        spriteBatch.Draw(_menuButton, POSITION_MENU_BUTTON, Color.White);
        spriteBatch.Draw(_uiSettings, POSITION_UI_SETTINGS, Color.White);
        DrawFxBars(spriteBatch);
        DrawMusicBars(spriteBatch);
    }

    // draw functions for the bars for the FX sound
    private void DrawFxBars(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < FxVolume; i++)
        {
            spriteBatch.Draw(_barSound, new Vector2(POSITION_FX_BARS.X + (SIZE_BAR_SOUND.X + SETTINGS_UI_SPACE_BETWEEN_BARS) * i, POSITION_FX_BARS.Y), Color.White);
        }
    }

    private void DrawMusicBars(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < MusicVolume; i++)
        {
            spriteBatch.Draw(_barSound, new Vector2(POSITION_MUSIC_BARS.X + (SIZE_BAR_SOUND.X + SETTINGS_UI_SPACE_BETWEEN_BARS) * i, POSITION_MUSIC_BARS.Y), Color.White);
        }
    }

    public void OnClickOk()
    {
        _mainGameScreen.StateMachine.ChangeState(new PlayState(_mainGameScreen));
    }
    public void OnClickMenu()
    {
        //go to menu screen
    }
    public void OnClickFxMinus()
    {
        //decrease fx volume
        FxVolume = MathHelper.Clamp(FxVolume - 1, 0, 10);
        SoundManager.Instance.SetVolumeFX(FxVolume / 10f);
    }
    public void OnClickFxPlus()
    {
        //increase fx volume
        FxVolume = MathHelper.Clamp(FxVolume + 1, 0, 10);
        SoundManager.Instance.SetVolumeFX(FxVolume / 10f);
    }
    public void OnClickFxMute()
    {
        //mute fx
        FxVolume = 0;
        SoundManager.Instance.SetVolumeFX(0);
    }
    public void OnClickMusicMinus()
    {
        //decrease music volume
        MusicVolume = MathHelper.Clamp(MusicVolume - 1, 0, 10);
        SoundManager.Instance.SetVolumeMusic(MusicVolume / 10f);
    }
    public void OnClickMusicPlus()
    {
        //increase music volume
        MusicVolume = MathHelper.Clamp(MusicVolume + 1, 0, 10);
        SoundManager.Instance.SetVolumeMusic(MusicVolume / 10f);
    }
    public void OnClickMusicMute()
    {
        //mute music
        MusicVolume = 0;
        SoundManager.Instance.SetVolumeMusic(0);
    }
}
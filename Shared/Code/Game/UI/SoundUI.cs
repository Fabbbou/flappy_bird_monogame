using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Gum.Wireframe;
using GumFormsSample;
using System.Collections.Generic;
using RenderingLibrary;

public class SoundUI
{
    private const int numberOfVolumeLevels = 3;
    private MainGameScreen _mainGameScreen;
    private GraphicalUiElement _gumScreen;
    private List<GraphicalUiElement> musicBarSounds = new();
    private List<GraphicalUiElement> fxBarSounds = new();

    public int MusicVolume { get; private set; }
    public int FxVolume { get; private set; }
    private bool _isActive;
    public bool IsActive => _isActive;
    public SoundUI(MainGameScreen mainGameScreen, GraphicalUiElement SoundUIScreen)
    {
        _mainGameScreen = mainGameScreen;
        _gumScreen = SoundUIScreen;
        _isActive = false;
    }

    public void Activate()
    {
        _gumScreen.AddToManagers(SystemManagers.Default, layer: null);
        _isActive = true;
    }

    public void Deactivate()
    {
        _gumScreen.RemoveFromManagers();
        _isActive = false;
    }

    public void Load()
    {
        MusicVolume = (int)(SoundManager.Instance.VolumeMusic * numberOfVolumeLevels);
        FxVolume = (int)(SoundManager.Instance.VolumeFX * numberOfVolumeLevels);

        GumTransparentButton.AttachButton(_gumScreen.GetGraphicalUiElementByName("OkButton"), OnClickOk);
        GumTransparentButton.AttachButton(_gumScreen.GetGraphicalUiElementByName("MenuButton"), OnClickMenu);
        GumTransparentButton.AttachButton(_gumScreen.GetGraphicalUiElementByName("MusicMuteButton"), OnClickMusicMute);
        GumTransparentButton.AttachButton(_gumScreen.GetGraphicalUiElementByName("MusicMinusButton"), OnClickMusicMinus);
        GumTransparentButton.AttachButton(_gumScreen.GetGraphicalUiElementByName("MusicPlusButton"), OnClickMusicPlus);
        GumTransparentButton.AttachButton(_gumScreen.GetGraphicalUiElementByName("FxMuteButton"), OnClickFxMute);
        GumTransparentButton.AttachButton(_gumScreen.GetGraphicalUiElementByName("FxMinusButton"), OnClickFxMinus);
        GumTransparentButton.AttachButton(_gumScreen.GetGraphicalUiElementByName("FxPlusButton"), OnClickFxPlus);
        musicBarSounds.Add(_gumScreen.GetGraphicalUiElementByName("MusicBarSound1"));
        musicBarSounds.Add(_gumScreen.GetGraphicalUiElementByName("MusicBarSound2"));
        musicBarSounds.Add(_gumScreen.GetGraphicalUiElementByName("MusicBarSound3"));
        fxBarSounds.Add(_gumScreen.GetGraphicalUiElementByName("FxBarSound1"));
        fxBarSounds.Add(_gumScreen.GetGraphicalUiElementByName("FxBarSound2"));
        fxBarSounds.Add(_gumScreen.GetGraphicalUiElementByName("FxBarSound3"));
        RefreshStatusBars(musicBarSounds, MusicVolume);
        RefreshStatusBars(fxBarSounds, FxVolume);
    }

    public void RefreshStatusBars(List<GraphicalUiElement> bars, int volume)
    {
        for (int i = 1; i < bars.Count+1; i++)
        {
            if(volume >= i)
            {
                bars[i-1].SetProperty("StatusState", "Enabled");
            }
            else
            {
                bars[i-1].SetProperty("StatusState", "Disabled");
            }
        }
    }

    public void OnClickOk()
    {
        _mainGameScreen.StateMachine.ChangeState(new PlayState(_mainGameScreen));
    }
    public void OnClickMenu()
    {
        MainRegistry.I.SceneRegistry.LoadScene(SceneName.MenuScreen);
    }
    public void OnClickFxMinus()
    {
        FxVolume = MathHelper.Clamp(FxVolume - 1, 0, numberOfVolumeLevels);
        SoundManager.Instance.SetVolumeFX((float)FxVolume / (float)numberOfVolumeLevels);
        RefreshStatusBars(fxBarSounds, FxVolume);
    }
    public void OnClickFxPlus()
    {
        FxVolume = MathHelper.Clamp(FxVolume + 1, 0, numberOfVolumeLevels);
        SoundManager.Instance.SetVolumeFX((float)FxVolume / (float)numberOfVolumeLevels);
        RefreshStatusBars(fxBarSounds, FxVolume);
    }
    public void OnClickFxMute()
    {
        FxVolume = 0;
        SoundManager.Instance.SetVolumeFX(0);
        RefreshStatusBars(fxBarSounds, FxVolume);
    }
    public void OnClickMusicMinus()
    {
        MusicVolume = MathHelper.Clamp(MusicVolume - 1, 0, numberOfVolumeLevels);
        SoundManager.Instance.SetVolumeMusic((float)MusicVolume / (float)numberOfVolumeLevels);
        RefreshStatusBars(musicBarSounds, MusicVolume);
    }
    public void OnClickMusicPlus()
    {
        MusicVolume = MathHelper.Clamp(MusicVolume + 1, 0, numberOfVolumeLevels);
        SoundManager.Instance.SetVolumeMusic((float)MusicVolume / (float)numberOfVolumeLevels);
        RefreshStatusBars(musicBarSounds, MusicVolume);
    }
    public void OnClickMusicMute()
    {
        MusicVolume = 0;
        SoundManager.Instance.SetVolumeMusic(0);
        RefreshStatusBars(musicBarSounds, MusicVolume);
    }
}
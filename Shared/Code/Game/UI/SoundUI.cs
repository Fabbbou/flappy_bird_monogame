using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Gum.Wireframe;
using GumFormsSample;
using System.Collections.Generic;

public class SoundUI
{
    private const int numberOfVolumeLevels = 3;
    private MainGameScreen _mainGameScreen;
    private GraphicalUiElement _rootComponent;
    private GraphicalUiElement _soundUIContainer;
    private List<GraphicalUiElement> musicBarSounds = new();
    private List<GraphicalUiElement> fxBarSounds = new();

    public int MusicVolume { get; private set; }
    public int FxVolume { get; private set; }
    public SoundUI(MainGameScreen mainGameScreen, GraphicalUiElement screen)
    {
        _mainGameScreen = mainGameScreen;
        _soundUIContainer = screen.GetGraphicalUiElementByName("SoundUIContainer");
    }

    public void Activate()
    {
        if(_rootComponent == null)
        {
            _rootComponent = GumHelper.InstanciateComponent("SoundUIComponent", _soundUIContainer);
            Load();
        }
        else
        {
            _soundUIContainer.Children.Add(_rootComponent);
        }
    }

    public void Deactivate()
    {
        _soundUIContainer.Children.Remove(_rootComponent);
    }

    private void Load()
    {
        MusicVolume = (int)(SoundManager.Instance.VolumeMusic * numberOfVolumeLevels);
        FxVolume = (int)(SoundManager.Instance.VolumeFX * numberOfVolumeLevels);

        GumTransparentButton.AttachButton(_rootComponent.GetGraphicalUiElementByName("OkButton"), OnClickOk);
        GumTransparentButton.AttachButton(_rootComponent.GetGraphicalUiElementByName("MenuButton"), OnClickMenu);
        GumTransparentButton.AttachButton(_rootComponent.GetGraphicalUiElementByName("MusicMuteButton"), OnClickMusicMute);
        GumTransparentButton.AttachButton(_rootComponent.GetGraphicalUiElementByName("MusicMinusButton"), OnClickMusicMinus);
        GumTransparentButton.AttachButton(_rootComponent.GetGraphicalUiElementByName("MusicPlusButton"), OnClickMusicPlus);
        GumTransparentButton.AttachButton(_rootComponent.GetGraphicalUiElementByName("FxMuteButton"), OnClickFxMute);
        GumTransparentButton.AttachButton(_rootComponent.GetGraphicalUiElementByName("FxMinusButton"), OnClickFxMinus);
        GumTransparentButton.AttachButton(_rootComponent.GetGraphicalUiElementByName("FxPlusButton"), OnClickFxPlus);
        musicBarSounds.Add(_rootComponent.GetGraphicalUiElementByName("MusicBarSound1"));
        musicBarSounds.Add(_rootComponent.GetGraphicalUiElementByName("MusicBarSound2"));
        musicBarSounds.Add(_rootComponent.GetGraphicalUiElementByName("MusicBarSound3"));
        fxBarSounds.Add(_rootComponent.GetGraphicalUiElementByName("FxBarSound1"));
        fxBarSounds.Add(_rootComponent.GetGraphicalUiElementByName("FxBarSound2"));
        fxBarSounds.Add(_rootComponent.GetGraphicalUiElementByName("FxBarSound3"));
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
        SoundManager.Instance.Save((float)FxVolume / (float)numberOfVolumeLevels, (float)MusicVolume / (float)numberOfVolumeLevels);
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
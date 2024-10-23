using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Diagnostics;

public enum SoundType
{
    FX, Music
}
public class SoundManager
{
    public bool IsLoaded { get; private set; }
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SoundManager();
            return _instance;
        }
    }
    private SoundEffectInstance DieSound;
    private SoundEffectInstance JumpSound;
    private SoundEffectInstance HitSound;
    private SoundEffectInstance ScoreSound;
    public void PlayDieSound() => PlayAndCut(DieSound);
    public void PlayJumpSound() => PlayAndCut(JumpSound);
    public void PlayHitSound() => PlayAndCut(HitSound);
    public void PlayScoreSound() => PlayAndCut(ScoreSound);
    public float VolumeFX => SettingsManager.Instance.UserSettings.VolumeFX;
    public float VolumeMusic => SettingsManager.Instance.UserSettings.VolumeMusic;

    private Dictionary<SoundEffectInstance, SoundType> _sounds = new Dictionary<SoundEffectInstance, SoundType>();

    public void Initialize()
    {
        if (IsLoaded) return;
        IsLoaded = true;
        JumpSound = AssetsLoader.Instance.JumpSound;
        ScoreSound = AssetsLoader.Instance.ScoreSound;
        HitSound = AssetsLoader.Instance.HitSound;
        DieSound = AssetsLoader.Instance.DieSound;

        _sounds.Add(JumpSound, SoundType.FX);
        _sounds.Add(ScoreSound, SoundType.FX);
        _sounds.Add(HitSound, SoundType.FX);
        _sounds.Add(DieSound, SoundType.FX);
        LoadVolumes();
    }

    private void LoadVolumes()
    {
        SetVolumeFX(VolumeFX);
        SetVolumeMusic(VolumeMusic);
    }

    public void Save(float fxVolume, float musicVolume)
    {
        SettingsManager.Instance.UserSettings.VolumeFX = fxVolume;
        SettingsManager.Instance.UserSettings.VolumeMusic = musicVolume;
        SettingsManager.Instance.SaveSettings();
    }

    public void SetVolumeFX(float volume)
    {
        foreach (var sound in _sounds)
        {
            if (sound.Value == SoundType.FX)
                sound.Key.Volume = volume;
        }
    }

    public void SetVolumeMusic(float volume)
    {
        foreach (var sound in _sounds)
        {
            if (sound.Value == SoundType.Music)
                sound.Key.Volume = volume;
        }
    }

    private static void PlayAndCut(SoundEffectInstance sound)
    {
        if (sound.State == SoundState.Playing)
            sound.Stop();
        sound.Play();
    }
}
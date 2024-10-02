using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

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
    private SoundEffectInstance _dieSound;
    public void PlayDieSound() => PlayAndCut(_dieSound);
    private SoundEffectInstance JumpSound;
    public void PlayJumpSound() => PlayAndCut(JumpSound);
    private SoundEffectInstance HitSound;
    public void PlayHitSound() => PlayAndCut(HitSound);
    private SoundEffectInstance ScoreSound;
    public void PlayScoreSound() => PlayAndCut(ScoreSound);
    public float VolumeFX => SettingsManager.Instance.UserSettings.VolumeFX;
    public float VolumeMusic => SettingsManager.Instance.UserSettings.VolumeMusic;

    private Dictionary<SoundEffectInstance, SoundType> _sounds = new Dictionary<SoundEffectInstance, SoundType>();

    public void LoadContent(ContentManager content)
    {
        IsLoaded = true;
        JumpSound = content.Load<SoundEffect>("sounds/sfx_wing").CreateInstance();
        ScoreSound = content.Load<SoundEffect>("sounds/sfx_point").CreateInstance();
        HitSound = content.Load<SoundEffect>("sounds/sfx_hit").CreateInstance();
        _dieSound = content.Load<SoundEffect>("sounds/sfx_die").CreateInstance();

        _sounds.Add(JumpSound, SoundType.FX);
        _sounds.Add(ScoreSound, SoundType.FX);
        _sounds.Add(HitSound, SoundType.FX);
        _sounds.Add(_dieSound, SoundType.FX);
        LoadVolumes();
    }

    private void LoadVolumes()
    {
        SetVolumeFX(VolumeFX);
        SetVolumeFX(VolumeMusic);
    }

    //volume control
    public void SetVolumeFX(float volume)
    {
        if (!IsLoaded) return;
        foreach (var sound in _sounds)
        {
            if (sound.Value == SoundType.FX)
                sound.Key.Volume = volume;
        }
    }

    public void SetVolumeMusic(float volume)
    {
        if (!IsLoaded) return;
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
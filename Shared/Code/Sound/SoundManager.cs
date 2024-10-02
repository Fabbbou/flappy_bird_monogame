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
    public SoundEffectInstance DieSound { get; private set; }
    public SoundEffectInstance JumpSound { get; private set; }
    public SoundEffectInstance HitSound { get; private set; }
    public SoundEffectInstance ScoreSound { get; private set; }

    private Dictionary<SoundEffectInstance, SoundType> _sounds = new Dictionary<SoundEffectInstance, SoundType>();

    public void LoadContent(ContentManager content)
    {
        JumpSound = content.Load<SoundEffect>("sounds/sfx_wing").CreateInstance();
        ScoreSound = content.Load<SoundEffect>("sounds/sfx_point").CreateInstance();
        HitSound = content.Load<SoundEffect>("sounds/sfx_hit").CreateInstance();
        DieSound = content.Load<SoundEffect>("sounds/sfx_die").CreateInstance();

        _sounds.Add(JumpSound, SoundType.FX);
        _sounds.Add(ScoreSound, SoundType.FX);
        _sounds.Add(HitSound, SoundType.FX);
        _sounds.Add(DieSound, SoundType.FX);

        IsLoaded = true;
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
}
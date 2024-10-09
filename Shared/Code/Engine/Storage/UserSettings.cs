using System.Collections.Generic;
using System.Text.Json.Serialization;

public class UserSettings
{
    public float VolumeFX { get; set; }
    public float VolumeMusic { get; set; }
    public List<int> Scores { get; set; }

    public UserSettings(float volumeFX, float volumeMusic, List<int> scores)
    {
        VolumeFX = volumeFX;
        VolumeMusic = volumeMusic;
        Scores = scores;
    }

    public UserSettings()
    {
        VolumeFX = 1f;
        VolumeMusic = 1f;
        Scores = [0,0,0];
    }

    public override string ToString()
    {
        return $"VolumeFX: {VolumeFX}, VolumeMusic: {VolumeMusic}";
    }
}
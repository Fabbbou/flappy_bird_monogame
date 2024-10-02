using System.Text.Json.Serialization;

public class UserSettings
{
    public UserSettings(float volumeFX, float volumeMusic)
    {
        VolumeFX = volumeFX;
        VolumeMusic = volumeMusic;
    }
    public float VolumeFX { get; private set; } = 1.0f; 
    public float VolumeMusic { get; private set; } = 1.0f;
}
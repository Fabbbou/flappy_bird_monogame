using System.Diagnostics;
using System.IO;
using System.Text.Json;

public class SettingsManager
{
    private static SettingsManager _instance;

    public static SettingsManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SettingsManager();
            return _instance;
        }
    }
    private static readonly string SettingsFilePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "userSettings.json");

    public bool IsLoaded { get; private set; }
    private UserSettings _userSettings;
    public UserSettings UserSettings
    {
        get
        {
#if DEBUG
            if (!IsLoaded)
            {
                throw new System.Exception("Settings not loaded");
            }
#endif
            return _userSettings;
        }
        set
        {
            _userSettings = value;
            if (_userSettings != null)
            {
                SaveSettings();
            }
        }
    }
    public void LoadSettings()
    {
        IsLoaded = true;
        if (File.Exists(SettingsFilePath))
        {
            string json = File.ReadAllText(SettingsFilePath);
            _userSettings = JsonSerializer.Deserialize<UserSettings>(json);
            Debug.WriteLine("Settings loaded: "+UserSettings);
        }
        else
        {
            _userSettings =  new UserSettings(1f,1f);
            SaveSettings();
        }
    }

    private void SaveSettings()
    {
        string json = JsonSerializer.Serialize(UserSettings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SettingsFilePath, json);
    }
}
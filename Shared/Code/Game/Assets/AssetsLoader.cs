using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
public class AssetsLoader
{
    //singleton
    public bool IsLoaded = false;
    private static AssetsLoader _instance;
    public static AssetsLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AssetsLoader();
            }
            return _instance;
        }
    }
    private AssetsLoader(){}

    //Sounds
    public SoundEffectInstance DieSound { get; private set; }
    public SoundEffectInstance JumpSound { get; private set; }
    public SoundEffectInstance HitSound { get; private set; }
    public SoundEffectInstance ScoreSound { get; private set;  }


    public void LoadContent(ContentManager content)
    {
        //load sounds
        JumpSound = content.Load<SoundEffect>("sounds/sfx_wing").CreateInstance();
        ScoreSound = content.Load<SoundEffect>("sounds/sfx_point").CreateInstance();
        HitSound = content.Load<SoundEffect>("sounds/sfx_hit").CreateInstance();
        DieSound = content.Load<SoundEffect>("sounds/sfx_die").CreateInstance();
        IsLoaded = true;
    }
}
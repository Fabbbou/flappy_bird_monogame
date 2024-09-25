using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;

public class Constants
{
    public const int WORLD_WIDTH = 144;
    public const int WORLD_HEIGHT = 256;
    public const int DEBUG_WORLD_WIDTH = 1000;
    public const int DEBUG_WORLD_HEIGHT = 1000;
    public const int PLAYABLE_WORLD_HEIGHT = WORLD_HEIGHT - Floor.SPRITE_HEIGHT;
    public static readonly Color DEFAULT_DEBUG_COLOR_GIZMOS = Color.Yellow;
}
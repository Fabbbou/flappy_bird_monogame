using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;

public class Constants
{
    // Ingame world dimensions
    public const int WORLD_WIDTH = 144;
    public const int WORLD_HEIGHT = 256;
    public const float WORLD_MIDDLE_SCREEN_WIDTH = WORLD_WIDTH * .5f;
    public const float WORLD_MIDDLE_SCREEN_HEIGHT = WORLD_HEIGHT * .5f;

    // Debug world dimensions
    public const int DEBUG_WORLD_WIDTH = 500;
    public const int DEBUG_WORLD_HEIGHT = 500;
    //Debug screen dimensions
    public const int DEBUG_SCREEN_WIDTH = 1000;
    public const int DEBUG_SCREEN_HEIGHT = 1000;

    public const float PLAYABLE_WORLD_HEIGHT = WORLD_HEIGHT - Floor.SPRITE_HEIGHT;
    public static readonly Color DEFAULT_DEBUG_COLOR_GIZMOS = Color.Yellow;
}
using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public abstract class DrawableEntity : GameEntity
{
    public abstract override void LoadContent(ContentManager content);
    public override void Update(GameTime gameTime) {}
    public abstract override void Draw(SpriteBatch spriteBatch);
}
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace flappyrogue_mg.Core
{
    public interface GameEntity
    {
        public abstract void LoadSingleInstance(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

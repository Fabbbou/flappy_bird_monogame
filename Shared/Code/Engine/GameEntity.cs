using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace flappyrogue_mg.Core
{
    public abstract class GameEntity
    {
        protected bool _isActive = true;
        public virtual bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }
        protected bool _isPaused = false;
        public virtual bool IsPaused
        {
            get => _isPaused;
            set => _isPaused = value;
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

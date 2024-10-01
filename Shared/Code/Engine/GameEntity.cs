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

        public virtual void LoadContent(ContentManager content) { }

        public virtual void Update(GameTime gameTime)
        {
            if (!IsActive) return;
            if(IsPaused) return;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(!IsActive) return;
        }
    }
}

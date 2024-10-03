using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace flappyrogue_mg.Core
{
    public abstract class GameEntity
    {
        private Entity _entity = new();
        public Entity Entity
        {
            get => _entity;
            private set => _entity = value;
        }
        public bool IsActive
        {
            get => _entity.IsActive;
            set => _entity.IsActive = value;
        }
        public bool IsPaused
        {
            get => _entity.IsPaused;
            set => _entity.IsPaused = value;
        }
        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

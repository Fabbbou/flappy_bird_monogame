using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flappyrogue_mg.Core
{
    public interface GameObject
    {
        public abstract void Load(ContentManager content, GraphicsDevice graphicsDevice);

        public abstract void Update(GameTime gameTime, GraphicsDevice graphicsDevice);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, ViewportAdapter viewportAdapter, GraphicsDevice graphicsDevice);
    }
}

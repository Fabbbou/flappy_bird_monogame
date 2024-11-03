using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class World
{
    private List<Entity> _gameEntities = new();
    private GraphicsDevice GraphicsDevice;
    private SpriteBatch _spriteBatch;

    public World(GraphicsDevice graphicsDevice)
    {
        GraphicsDevice = graphicsDevice;
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    public void AddEntity(Entity gameEntity)
    {
        _gameEntities.Add(gameEntity);
    }
    public void Update(GameTime gametime)
    {
        ClickRegistry.Instance.Update(gametime);
        foreach (Entity entity in _gameEntities)
        {
            if (!entity.IsActive) continue;
            if (entity.IsPaused) continue;
            if (entity is GameEntity gameEntity)
            {
                gameEntity.Update(gametime);
            }
        }
        PhysicsEngine.Instance.Update(gametime);
    }

    public void LoadContent(ContentManager content)
    {
        SettingsManager.Instance.LoadSettings();
        SoundManager.Instance.Initialize();

        foreach (Entity entity in _gameEntities)
        {
            if (entity is GameEntity gameEntity)
            {
                gameEntity.LoadContent(content);
            }
        }
    }

    public void UnloadContent()
    {
        ClickRegistry.Instance.Clear();
        PhysicsEngine.Instance.Clear();
        _gameEntities.Clear();
    }
}
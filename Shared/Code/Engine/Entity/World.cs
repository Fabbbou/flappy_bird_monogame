using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ECS;
using System.Collections.Generic;

public class World
{
    private List<Entity> _gameEntities = new();

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
            if(entity is GameEntity gameEntity)
            {
                gameEntity.Update(gametime);
            }
        }
        PhysicsEngine.Instance.Update(gametime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var entity in _gameEntities)
        {
            if (!entity.IsActive) continue;
            if (entity is GameEntity gameEntity)
            {
                gameEntity.Draw(spriteBatch);
            }
        }
        GizmosRegistry.Instance.Draw(spriteBatch);
    }

    public void LoadContent(ContentManager content)
    {
        SettingsManager.Instance.LoadSettings();
        SoundManager.Instance.LoadContent(content);

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
        GizmosRegistry.Instance.Clear();
    }
}
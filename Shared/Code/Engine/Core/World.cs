using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class World
{
    private List<GameEntity> _gameEntities = new();

    public void AddGameEntity(GameEntity gameEntity)
    {
        _gameEntities.Add(gameEntity);
    }

    public void Update(GameTime gametime)
    {
        ClickRegistry.Instance.Update(gametime);
        foreach (var gameEntity in _gameEntities)
        {
            if (!gameEntity.IsActive) continue;
            if (gameEntity.IsPaused) continue;
            gameEntity.Update(gametime);
        }
        PhysicsEngine.Instance.Update(gametime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var gameEntity in _gameEntities)
        {
            if (!gameEntity.IsActive) continue;
            gameEntity.Draw(spriteBatch);
        }
    }

    public void LoadContent(ContentManager content)
    {
        PreloadedAssets.Instance.LoadContent(content);
        SettingsManager.Instance.LoadSettings();
        SoundManager.Instance.LoadContent(content);

        foreach (var gameEntity in _gameEntities)
        {
            gameEntity.LoadContent(content);
        }
    }

    public void UnloadContent()
    {
        ClickRegistry.Instance.Clear();
        PhysicsEngine.Instance.Clear();
        PhysicsEngine.Instance.Clear();
        GizmosRegistry.Instance.Clear();
    }
}
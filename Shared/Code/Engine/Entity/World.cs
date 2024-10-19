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
    private List<Entity> _uiGameEntities = new();
    private List<Entity> _inGameEntities = new();
    private List<Entity> _backgroundUIEntities = new();
    private List<Entity> _backgroundIngameEntities = new();

    public World(GraphicsDevice graphicsDevice)
    {
        GraphicsDevice = graphicsDevice;
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    public void AddUIEntity(Entity gameEntity)
    {
        _uiGameEntities.Add(gameEntity);
    }
    public void AddIngameEntity(Entity gameEntity)
    {
        _inGameEntities.Add(gameEntity);
    }
    public void AddBackgroundUIEntity(Entity gameEntity)
    {
        _backgroundUIEntities.Add(gameEntity);
    }
    public void AddBackgroundIngameEntity(Entity gameEntity)
    {
        _backgroundIngameEntities.Add(gameEntity);
    }

    public void Update(GameTime gametime)
    {
        ClickRegistry.Instance.Update(gametime);
        UpdateList(_backgroundIngameEntities, gametime);
        UpdateList(_backgroundUIEntities, gametime);
        UpdateList(_inGameEntities, gametime);
        UpdateList(_uiGameEntities, gametime);
        PhysicsEngine.Instance.Update(gametime);
    }

    private void UpdateList(List<Entity> entities, GameTime gametime)
    {
        foreach (Entity entity in entities)
        {
            if (!entity.IsActive) continue;
            if (entity.IsPaused) continue;
            if (entity is GameEntity gameEntity)
            {
                gameEntity.Update(gametime);
            }
        }
    }

    public void Draw(Matrix transformationMatrix, Matrix? UITransformationMatrix = null)
    {
        DrawUiAndIngame(_backgroundIngameEntities, _backgroundUIEntities, transformationMatrix, UITransformationMatrix);
        DrawUiAndIngame(_inGameEntities, _uiGameEntities, transformationMatrix, UITransformationMatrix);

        //GizmosRegistry.Instance.Draw(spriteBatch);
    }

    public void DrawUiAndIngame(List<Entity> ingames, List<Entity> uis, Matrix transformationMatrix, Matrix? UITransformationMatrix = null)
    {
        _spriteBatch.Begin(transformMatrix: transformationMatrix, samplerState: SamplerState.PointClamp);
        DrawList(_spriteBatch, ingames);
        _spriteBatch.End();

        _spriteBatch.Begin(transformMatrix: UITransformationMatrix, samplerState: SamplerState.PointClamp);
        DrawList(_spriteBatch, uis);
        _spriteBatch.End();
    }

    private void DrawList(SpriteBatch spriteBatch, List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            if (!entity.IsActive) continue;
            if (entity is GameEntity gameEntity)
            {
                gameEntity.Draw(spriteBatch);
            }
        }
    }

    public void LoadContent(ContentManager content)
    {
        SettingsManager.Instance.LoadSettings();
        SoundManager.Instance.LoadContent(content);

        foreach (Entity entity in _backgroundUIEntities)
        {
            if (entity is GameEntity gameEntity)
            {
                gameEntity.LoadContent(content);
            }
        }
        foreach (Entity entity in _backgroundIngameEntities)
        {
            if (entity is GameEntity gameEntity)
            {
                gameEntity.LoadContent(content);
            }
        }
        foreach (Entity entity in _inGameEntities)
        {
            if (entity is GameEntity gameEntity)
            {
                gameEntity.LoadContent(content);
            }
        }
        foreach (Entity entity in _uiGameEntities)
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
        _uiGameEntities.Clear();
        _inGameEntities.Clear();
        _backgroundUIEntities.Clear();
        _backgroundIngameEntities.Clear();
    }
}

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Graphics;
using System;
using flappyrogue_mg.Core;
using Gum.Wireframe;

public class PipesSpawner : GameEntity
{
    public float TimeToSpawn;

    //MIN and MAX are height from top to bottom (seems reversed)
    public const int MIN_HEIGHT_PIPES = -33;
    public const int MAX_HEIGHT_PIPES = -147;
    private List<Pipes> _pipes = new();
    private int _pipesCounter = 0;
    private readonly GraphicalUiElement _rootIngameWorld;
    private readonly GraphicalUiElement _pipeContainer;
    private float _timeToSpawnCounter = 0f;

    //constructor with all fields
    public PipesSpawner(GraphicalUiElement rootIngameWorld, float initialTimeBetween2PipesSpawn = 2f)
    {
        TimeToSpawn = initialTimeBetween2PipesSpawn;
        _rootIngameWorld = rootIngameWorld;
        _pipeContainer = rootIngameWorld.GetGraphicalUiElementByName("PipeContainer");
    }

    public new bool IsPaused
    {
        set
        {
            base.IsPaused = value;
            foreach (Pipes pipe in _pipes)
            {
                pipe.IsPaused = value;
            }

        }
    }

    public override void Update(GameTime gameTime)
    {
        SpawnPipes(gameTime);
        UpdatePipes(gameTime);
    }

    private static float RandomHeight(int min, int max)
    {
        return (float)new Random().NextDouble() * (max - min) + min; //to be tested with negative positions
    }

    private void SpawnPipes(GameTime gameTime)
    {
        _timeToSpawnCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_timeToSpawnCounter >= TimeToSpawn)
        {
            _timeToSpawnCounter = 0f;
            _pipesCounter++;
            Pipes newPipe = CreatePipes(Pipes.DEFAULT_SPAWN_POSITION.X, RandomHeight(MIN_HEIGHT_PIPES, MAX_HEIGHT_PIPES));
            _pipes.Add(newPipe);
        }
    }

    private void UpdatePipes(GameTime gameTime)
    {
        for (int i = 0; i < _pipes.Count; i++)
        {
            _pipes[i].Update(gameTime);
            //remove pipes that are out of the screen
            if (_pipes[i].Right < 0)
            {
                _pipes[i].Kill(); // we need to kill the pipes manually otherwise the collision detections still collides with out of screen objects and bugs a lot. (the bird.x position becomes the pipe width)
                _pipes.RemoveAt(i);
                i--;
            }
        }
    }

    public override void LoadContent(ContentManager content) { }
    public override void Draw(SpriteBatch spriteBatch)
    {
        foreach (Pipes pipe in _pipes)
        {
            pipe.Draw(spriteBatch);
        }
    }

    /// <summary>
    /// Creates a new pipe with the given position
    /// </summary>
    /// <param name="x">left side</param>
    /// <param name="y">top side</param>
    /// <returns></returns>
    private Pipes CreatePipes(float x, float y)
    {
        var pipes = new Pipes(_pipeContainer, spawnPosition: new(x, y), pipesInstanceNumber: _pipesCounter);
        pipes.LoadContent(null);
        return pipes;
    }
}
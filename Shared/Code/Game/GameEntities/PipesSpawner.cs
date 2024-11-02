
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Graphics;
using System;
using flappyrogue_mg.Core;

public class PipesSpawner : GameEntity
{
    public const int MIN_HEIGHT_PIPES = 43;
    public const int MAX_HEIGHT_PIPES = 157;
    public const int INIT_HEIGHT_PIPES = 320;
    public const float SPEED = 60f;

    //MIN and MAX are height from top to bottom (seems reversed)
    private List<Pipes> _pipes = new();
    private int pipesCounter = 0;
    private float _timeToSpawn = 2f;
    private float _timeToSpawnCounter = 0f;

    private float _xOffsetFromRightBorder = 60f;

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
        return (float)new Random().NextDouble() * (max - min) + min;
    }

    private void SpawnPipes(GameTime gameTime)
    {
        _timeToSpawnCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_timeToSpawnCounter >= _timeToSpawn)
        {
            _timeToSpawnCounter = 0f;
            pipesCounter++;
            //_pipes.Add(CreatePipes(RandomHeight(MIN_HEIGHT_PIPES, MAX_HEIGHT_PIPES)));
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

    public override void LoadContent(ContentManager content) {}
    public override void Draw(SpriteBatch spriteBatch)
    {
        foreach (Pipes pipe in _pipes)
        {
            pipe.Draw(spriteBatch);
        }
    }

    //private Pipes CreatePipes(float height)
    //{
    //    Pipes pipes = new(" " + pipesCounter, _xOffsetFromRightBorder, height, GAP_HEIGHT, SPEED);
    //    return pipes;
    //}
}

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
    public const float GAP_HEIGHT = 60f;
    public const float OFFSET_PIPES_VISIBLE = 14;
    public const float SPEED = 60f;

    //MIN and MAX are height from top to bottom (seems reversed)
    public const float MIN_HEIGTH_PIPES = OFFSET_PIPES_VISIBLE;
    public static readonly float MAX_HEIGTH_PIPES = Constants.PLAYABLE_WORLD_HEIGHT - GAP_HEIGHT - OFFSET_PIPES_VISIBLE;
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

    private static float RandomHeight()
    {
        //float minHeight = OFFSET_PIPES_VISIBLE; //to see a little bit of the pipe
        //max height is the height of the screen minus the height of the floor (PLAYABLE_WORLD_HEIGHT) minus the height of the pipe (so it doesnt fly)
        //float maxHeight = Constants.PLAYABLE_WORLD_HEIGHT - GAP_HEIGHT - OFFSET_PIPES_VISIBLE;
        return (float)new Random().NextDouble() * (MAX_HEIGTH_PIPES - MIN_HEIGTH_PIPES) + MIN_HEIGTH_PIPES;
    }

    private void SpawnPipes(GameTime gameTime)
    {
        _timeToSpawnCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_timeToSpawnCounter >= _timeToSpawn)
        {
            _timeToSpawnCounter = 0f;
            pipesCounter++;
            //_pipes.Add(CreatePipes(RandomHeight()));
            _pipes.Add(CreatePipes(RandomHeight()));
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

    public Pipes CreatePipes(float height)
    {
        Pipes pipes = new(" " + pipesCounter, _xOffsetFromRightBorder, height, GAP_HEIGHT, SPEED);
        pipes.BypassLoadContent();
        return pipes;
    }
}
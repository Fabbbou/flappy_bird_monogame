
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
    public static readonly float SPEED = 60f;

    private List<Pipes> _pipes = new();
    private int pipesCounter = 0;
    private float _timeToSpawn = 2f;
    private float _timeToSpawnCounter = 0f;

    private float _xOffsetFromRightBorder = 60f;

    private Texture2DRegion _pipeTopTexture;
    private Texture2DRegion _pipeBottomTexture;
    
    public void Update(GameTime gameTime)
    {
        SpawnPipes(gameTime, _xOffsetFromRightBorder, RandomHeight(), GAP_HEIGHT, SPEED);
        UpdatePipes(gameTime);
    }

    private float RandomHeight()
    {
        float minHeight = OFFSET_PIPES_VISIBLE; //to see a little bit of the pipe
        //max height is the height of the screen minus the height of the floor (PLAYABLE_WORLD_HEIGHT) minus the height of the pipe (so it doesnt fly)
        float maxHeight = Constants.PLAYABLE_WORLD_HEIGHT - GAP_HEIGHT - OFFSET_PIPES_VISIBLE;
        return (float)new Random().NextDouble() * (maxHeight - minHeight) + minHeight;
    }

    private void SpawnPipes(GameTime gameTime, float xOffsetFromRightBorder, float yOffsetFromTop, float gapHeight, float speed)
    {
        _timeToSpawnCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_timeToSpawnCounter >= _timeToSpawn)
        {
            _timeToSpawnCounter = 0f;
            pipesCounter++;
            _pipes.Add(new Pipes(" "+ pipesCounter, xOffsetFromRightBorder, yOffsetFromTop, gapHeight, speed));
        }
    }

    private void UpdatePipes(GameTime gameTime)
    {
        for (int i = 0; i < _pipes.Count; i++)
        {
            _pipes[i].Update(gameTime);
            //remove pipes that are out of the screen
            if (_pipes[i].PhysicsObjectPipeTop.Position.X < -Pipes.SPRITE_WIDTH)
            {
                _pipes[i].Kill(); // we need to kill the pipes manually otherwise the collision detections still collides with out of screen objects and bugs a lot. (the bird.x position becomes the pipe width)
                _pipes.RemoveAt(i);
                i--;
            }
        }
    }

    public void LoadContent(ContentManager content)
    {
        //we still load the atlas here to get the texture
        // this has been already called in the MainGame Screen PreloadedAssets.Instance.Load(content);

        _pipeTopTexture = PreloadedAssets.Instance.PipeTop;
        _pipeBottomTexture = PreloadedAssets.Instance.PipeBottom;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (Pipes pipe in _pipes)
        {
            pipe.Draw(spriteBatch, _pipeTopTexture, _pipeBottomTexture);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Represents an animated texture that can be drawn on the screen.
/// 
/// </summary>
public class AnimatedTexture
{
    private AtlasTexture _atlasTexture;
    private float _frameTime = 0.1f; // time to wait before changing the frame in seconds
    private float _timerFrameAtlas = 0;

    public AnimatedTexture(float frameTime, string contentPathTexture, string contentPathJsonAtlas)
    {
        _frameTime = frameTime;
        _atlasTexture = new AtlasTexture(contentPathTexture, contentPathJsonAtlas);
    }

    public void Load(ContentManager content)
    {
        _atlasTexture.Load(content);
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Update the frame of the bird animation
        _timerFrameAtlas += deltaTime;
        if (_timerFrameAtlas >= _frameTime)
        {
            _atlasTexture.Index = (_atlasTexture.Index + 1) % _atlasTexture.Count;
            _timerFrameAtlas = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        _atlasTexture.Draw(spriteBatch, position);
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Represents an animated texture that can be drawn on the screen.
/// Example of json structure:
/// {
///     "bird1": {
///         "x": 0,
///         "y": 0,
///         "width": 34,
///         "height": 24
///     },
///     "bird2": {
///         "x": 34,
///         "y": 0,
///         "width": 34,
///         "height": 24
///     },
/// }
/// 
/// </summary>
public class AnimatedTexture
{
    private string _contentPathTexture;
    private string _contentPathJsonAtlas;
    private float _frameTime = 0.1f; // time to wait before changing the frame in seconds

    private Texture2D _spriteTexture;
    private List<Rectangle> _atlasData;
    private int _indexAtlas = 0;
    private float _timerFrameAtlas = 0;

    public AnimatedTexture(float frameTime, string contentPathTexture, string contentPathJsonAtlas)
    {
        _frameTime = frameTime;
        _contentPathTexture = contentPathTexture;
        _contentPathJsonAtlas = contentPathJsonAtlas;
    }

    public void Load(ContentManager content)
    {
        string json = File.ReadAllText($"Content/{_contentPathJsonAtlas}");
        Dictionary<string, Rectangle> atlasDataDict = JsonConvert.DeserializeObject<Dictionary<string, Rectangle>>(json);
        //convert the dictionary values to a list of Rectangles
        _atlasData = new List<Rectangle>(atlasDataDict.Values);

        //set _spriteTexture to the bird texture from the Content pipeline
        _spriteTexture = content.Load<Texture2D>(_contentPathTexture);
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Update the frame of the bird animation
        _timerFrameAtlas += deltaTime;
        if (_timerFrameAtlas >= _frameTime)
        {
            _indexAtlas = (_indexAtlas + 1) % _atlasData.Count;
            _timerFrameAtlas = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(_spriteTexture, position, _atlasData[_indexAtlas], Color.White);
    }
}
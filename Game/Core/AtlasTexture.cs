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
public class AtlasTexture
{
    private string _contentPathTexture;
    private string _contentPathJsonAtlas;

    private Texture2D _spriteTexture;
    private List<Rectangle> _atlasData;
    public int Index;
    public int Count => _atlasData.Count;
    public int Width => _atlasData[Index].Width;
    public int Height => _atlasData[Index].Height;

    public AtlasTexture(string contentPathTexture, string contentPathJsonAtlas)
    {
        _contentPathTexture = contentPathTexture;
        _contentPathJsonAtlas = contentPathJsonAtlas;
        Index = 0;
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

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(_spriteTexture, position, _atlasData[Index], Color.White);
    }
}
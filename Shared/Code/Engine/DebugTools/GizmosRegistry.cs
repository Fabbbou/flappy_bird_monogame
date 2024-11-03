using Microsoft.Xna.Framework.Graphics;
using RenderingLibrary;
using System.Collections.Generic;

public class GizmosRegistry
{
    private static GizmosRegistry _instance;
    public static GizmosRegistry Instance
    {
        get
        {
            _instance ??= new GizmosRegistry();
            return _instance;
        }
    }

    private List<Gizmo> _gizmos = new List<Gizmo>();
    private GraphicsDevice _graphicsDevice;
    private SpriteBatch _spriteBatch;
    private bool _enabled;
    public bool IsDebugging => _enabled;

    public void Start(GraphicsDevice graphicsDevice, bool enabled)
    {
        _graphicsDevice = graphicsDevice;
        _enabled = enabled;
        _spriteBatch = new SpriteBatch(_graphicsDevice);
    }

    public void Add(Gizmo gizmo)
    {
        _gizmos.Add(gizmo);
    }

    public void Refresh()
    {
        if (_enabled)
        {
            foreach (Gizmo gizmo in _gizmos)
            {
                gizmo.UpdateGizmo();
            }
        }
    }
    public void RemoveObject(Gizmo gizmo)
    {
        _gizmos.Remove(gizmo);
    }

    public void Clear()
    {
        _gizmos.Clear();
    }   
}
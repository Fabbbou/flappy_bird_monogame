using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;

public class ClickableRegionHandler : Gizmo
{
    private Entity _entity;
    public bool IsActive
    {
        get => _entity.IsActive;
    }
    public bool IsPaused
    {
        get => _entity.IsPaused;
    }
    private Rectangle _clickableRegion;
    private readonly Action _onRegionClicked;
    public OrthographicCamera Camera { get; private set; }
    public string Label => ToString();
    public ClickableRegionHandler(Entity entity, OrthographicCamera camera, Action onRegionClicked, Rectangle clickableRegion)
    {
        GizmosRegistry.Instance.Add(this);
        ClickRegistry.Instance.Add(this);
        _entity = entity;
        _clickableRegion = clickableRegion;
        _onRegionClicked = onRegionClicked;
        Camera = camera;

    }

    ~ClickableRegionHandler() => Kill();

    public void Click(GameTime gameTime)
    {
        _onRegionClicked();
    }

    //tostring
    public override string ToString()
    {
        return "ClickableRegionHandler(" + _clickableRegion.Location + ";"+_clickableRegion.Size+")";
    }

    public bool Contains(Vector2 worldPosition)
    {
        return _clickableRegion.Contains(worldPosition);
    }


    public void DrawGizmo(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;
        if (IsPaused) return;
        spriteBatch.DrawRectangle(_clickableRegion, Color.Red, 2);
    }

    public void Kill()
    {
        GizmosRegistry.Instance.RemoveObject(this);
    }
}
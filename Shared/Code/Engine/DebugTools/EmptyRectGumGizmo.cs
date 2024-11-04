using Gum.Wireframe;
using MonoGameGum.GueDeriving;
using RenderingLibrary;
using RenderingLibrary.Graphics;
using RenderingLibrary.Math.Geometry;
using System;
using Color = Microsoft.Xna.Framework.Color;

public class EmptyRectGumGizmo : GumGizmo
{
    public const int BORDER_SIZE = 1;
    private readonly GraphicalUiElement _rootIngameWorld;
    private readonly Color _debugColor;
    private ColoredRectangleRuntime _top;
    private ColoredRectangleRuntime _bottom;
    private ColoredRectangleRuntime _left;
    private ColoredRectangleRuntime _right;
    public PhysicsObject _physicsObject { get; private set; }
    private RectCollider rect => _physicsObject.Collider as RectCollider;
    public EmptyRectGumGizmo(GraphicalUiElement rootIngameWorld, Color debugColor)
    {   
        _rootIngameWorld = rootIngameWorld;
        _debugColor = debugColor;
    }

    public void AttachToPhysicsObject(PhysicsObject physicsObject)
    {
        _physicsObject = physicsObject;
        _top = new();
        SetColoredRectangleRuntime(_top, rect.Width, BORDER_SIZE);

        _bottom = new();
        SetColoredRectangleRuntime(_bottom, rect.Width, BORDER_SIZE);

        _left = new ColoredRectangleRuntime();
        SetColoredRectangleRuntime(_left, BORDER_SIZE, rect.Height);

        _right = new ColoredRectangleRuntime();
        SetColoredRectangleRuntime(_right, BORDER_SIZE, rect.Height);
        SetPositions();
        if (GizmosRegistry.Instance.IsDebugging)
        {
            Activate();
        }
    }

    private void SetColoredRectangleRuntime(ColoredRectangleRuntime coloredRectangleRuntime, float width, float height)
    {
        coloredRectangleRuntime.Width = width;
        coloredRectangleRuntime.Height = height;
        coloredRectangleRuntime.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
        coloredRectangleRuntime.Color = _debugColor;
        coloredRectangleRuntime.XUnits = Gum.Converters.GeneralUnitType.PixelsFromSmall;
        coloredRectangleRuntime.YUnits = Gum.Converters.GeneralUnitType.PixelsFromSmall;
        coloredRectangleRuntime.XOrigin = HorizontalAlignment.Left;
        coloredRectangleRuntime.YOrigin = VerticalAlignment.Top;
    }

    public void Refresh() 
    {
        SetPositions();
    }

    private void SetPositions()
    {
        SetPosition(_top, 0, 0);
        SetPosition(_bottom, 0, rect.Height - BORDER_SIZE);
        SetPosition(_left, 0, 0);
        SetPosition(_right, rect.Width - BORDER_SIZE, 0);
    }

    private void SetPosition(ColoredRectangleRuntime coloredRectangleRuntime, float x, float y)
    {
        coloredRectangleRuntime.X = _rootIngameWorld.AbsoluteLeft + _physicsObject.Position.X + x;
        coloredRectangleRuntime.Y = _rootIngameWorld.AbsoluteTop + _physicsObject.Position.Y + y;
    }

    public void Activate()
    { 
        _top.AddToManagers();
        _bottom.AddToManagers();
        _left.AddToManagers();
        _right.AddToManagers();
    }

    public void Deactivate()
    {
        //_top.RemoveFromManagers();
        //_bottom.RemoveFromManagers();
        //_left.RemoveFromManagers();
        //_right.RemoveFromManagers();
    }
}
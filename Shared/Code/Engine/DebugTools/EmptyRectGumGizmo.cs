using Gum.Wireframe;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using RenderingLibrary.Math.Geometry;
using System;
using Color = Microsoft.Xna.Framework.Color;

public class EmptyRectGumGizmo : GumGizmo
{
    public const int BORDER_SIZE = 1;
    public static readonly Color DebugColor = Color.Pink;
    private readonly GraphicalUiElement _parentGraphicalUiElement;
    private readonly ColoredRectangleRuntime _top;
    private readonly ColoredRectangleRuntime _bottom;
    private readonly ColoredRectangleRuntime _left;
    private readonly ColoredRectangleRuntime _right;

    public EmptyRectGumGizmo(GraphicalUiElement graphicalUiElement)
    {   
        _parentGraphicalUiElement = graphicalUiElement;
        _top = new();
        SetColoredRectangleRuntime(_top, 0, 0, graphicalUiElement.Width, BORDER_SIZE);

        _bottom = new();
        SetColoredRectangleRuntime(_bottom, 0, graphicalUiElement.Height - BORDER_SIZE, graphicalUiElement.Width, BORDER_SIZE);

        _left = new ColoredRectangleRuntime();
        SetColoredRectangleRuntime(_left, 0, 0, BORDER_SIZE, graphicalUiElement.Height);

        _right = new ColoredRectangleRuntime();
        SetColoredRectangleRuntime(_right, graphicalUiElement.Width - BORDER_SIZE, 0, BORDER_SIZE, graphicalUiElement.Height);
    }

    private static void SetColoredRectangleRuntime(ColoredRectangleRuntime coloredRectangleRuntime, float x, float y, float width, float height)
    {
        coloredRectangleRuntime.X = x;
        coloredRectangleRuntime.Y = y;
        coloredRectangleRuntime.Width = width;
        coloredRectangleRuntime.Height = height;
        coloredRectangleRuntime.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
        coloredRectangleRuntime.Color = DebugColor;
        coloredRectangleRuntime.XUnits = Gum.Converters.GeneralUnitType.PixelsFromSmall;
        coloredRectangleRuntime.YUnits = Gum.Converters.GeneralUnitType.PixelsFromSmall;
        coloredRectangleRuntime.XOrigin = HorizontalAlignment.Left;
        coloredRectangleRuntime.YOrigin = VerticalAlignment.Top;
    }

    public void Update() { }

    public void Display()
    {
        _parentGraphicalUiElement.Children.Add(_top);
        _parentGraphicalUiElement.Children.Add(_bottom);
        _parentGraphicalUiElement.Children.Add(_left);
        _parentGraphicalUiElement.Children.Add(_right);
    }

    public void Kill()
    {
        _parentGraphicalUiElement?.Children.Remove(_top);
        _parentGraphicalUiElement?.Children.Remove(_bottom);
        _parentGraphicalUiElement?.Children.Remove(_left);
        _parentGraphicalUiElement?.Children.Remove(_right);
    }
}
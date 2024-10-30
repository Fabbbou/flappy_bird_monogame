using Gum.Wireframe;
using RenderingLibrary;
using RenderingLibrary.Math.Geometry;

public class EmptyCircleGizmo : GumGizmo
{
    private LineCircle _lineCircle;
    private GraphicalUiElement _parentGraphicalUiElement;
    public EmptyCircleGizmo(GraphicalUiElement graphicalUiElement, float radius)
    {
        _parentGraphicalUiElement = graphicalUiElement;
        _lineCircle = new LineCircle();
        _lineCircle.X = _parentGraphicalUiElement.X;
        _lineCircle.Y = _parentGraphicalUiElement.Y;
        _lineCircle.Radius = radius;
        _lineCircle.Color = System.Drawing.Color.Pink;
    }

    public void Update()
    {
        _lineCircle.X = _parentGraphicalUiElement.X;
        _lineCircle.Y = _parentGraphicalUiElement.Y;
    }

    public void Display()
    {
        SystemManagers.Default.ShapeManager.Add(_lineCircle);
    }

    public void Kill()
    {
        SystemManagers.Default.ShapeManager.Remove(_lineCircle);
    }
}
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using RenderingLibrary;
using RenderingLibrary.Math.Geometry;

public class EmptyCircleGizmo : GumGizmo
{
    private readonly LineCircle _lineCircle;
    private readonly GraphicalUiElement _rootIngameWorld;
    public PhysicsObject _physicsObject { get; private set; }
    public EmptyCircleGizmo(GraphicalUiElement rootIngameWorld, float radius)
    {
        _rootIngameWorld = rootIngameWorld;
        _lineCircle = new LineCircle();
        _lineCircle.Radius = radius;
        _lineCircle.Color = System.Drawing.Color.Pink;
    }

    public void AttachToPhysicsObject(PhysicsObject physicsObject)
    {
        _physicsObject = physicsObject;
        SetPosition();
    }

    bool _isActivated = false;
    public void Draw()
    {
        if (!_isActivated)
        {
            Activate();
            _isActivated = true;
        }
        SetPosition();
    }

    public void Activate()
    {
        
        SystemManagers.Default.ShapeManager.Add(_lineCircle);
    }

    public void Deactivate()
    {
        SystemManagers.Default.ShapeManager.Remove(_lineCircle);
    }

    public void SetPosition()
    {
            _lineCircle.X = _rootIngameWorld.AbsoluteLeft + _physicsObject.Position.X;
            _lineCircle.Y = _rootIngameWorld.AbsoluteTop + _physicsObject.Position.Y;
    }
}
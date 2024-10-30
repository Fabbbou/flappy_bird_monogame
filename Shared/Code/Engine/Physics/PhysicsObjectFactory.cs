using Gum.Wireframe;
using System;
using System.Numerics;

public class PhysicsObjectFactory

{
    public static PhysicsObject Rect(string label, float x, float y, ColliderType collisionType, float width, float height, GraphicalUiElement graphicalUiElement = null)
    {
        EmptyRectGumGizmo emptyRectGumGizmo = null;
        if (graphicalUiElement != null)
        {
            emptyRectGumGizmo = new EmptyRectGumGizmo(graphicalUiElement);
        }
        var physicsObject = new PhysicsObject(label, x, y, collisionType, graphicalUiElement, emptyRectGumGizmo);
        var rect = new RectCollider(physicsObject, collisionType, width, height);
        physicsObject.Collider = rect;
        return physicsObject;
    }

    public static PhysicsObject Circl(string label, float x, float y, ColliderType collisionType, float radius, GraphicalUiElement graphicalUiElement = null)
    {
        EmptyCircleGizmo emptyCircleGizmo = null;
        if (graphicalUiElement != null)
        {
            emptyCircleGizmo = new EmptyCircleGizmo(graphicalUiElement, radius);
        }
        var physicsObject = new PhysicsObject(label, x, y, collisionType, graphicalUiElement, emptyCircleGizmo);
        var circl = new CirclCollider(physicsObject, collisionType, radius);
        physicsObject.Collider = circl;
        return physicsObject;
    }

    public static PhysicsObject AreaRectTriggerOnce(string label, float x, float y, ColliderType collisionType, float width, float height, Action onTrigger, GraphicalUiElement graphicalUiElement = null)
    {
        var physicsObject = new PhysicsObject(label, x, y, collisionType, graphicalUiElement)
        {
            Gravity = Vector2.Zero,
        };
        var rect = new RectCollider(physicsObject, collisionType, width, height)
        {
            OnCollisionAction = onTrigger
        };
        physicsObject.Collider = rect;
        return physicsObject;
    }
}   
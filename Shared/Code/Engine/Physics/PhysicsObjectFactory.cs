using Gum.Wireframe;
using Microsoft.Xna.Framework;
using System;

public class PhysicsObjectFactory
{
    public static readonly Color DebugColorPink = Color.Pink;

    //pre: toute les positions sont relatives au root topleft
    //si collidertype static, prend la position du root + position du physicsobject
    //sinon, position du physicsobject
    public static PhysicsObject Rect(string label, float x, float y, ColliderType collisionType, float width, float height, GraphicalUiElement rootGraphicalUiElement = null, GraphicalUiElement graphicalUiElement = null, Color? debugColor = null)
    {
        EmptyRectGumGizmo emptyRectGumGizmo = null;
        if (rootGraphicalUiElement != null)
        {
            emptyRectGumGizmo = new EmptyRectGumGizmo(rootGraphicalUiElement, debugColor ?? DebugColorPink);
        }
        var physicsObject = new PhysicsObject(label, x, y, collisionType, graphicalUiElement, emptyRectGumGizmo);
        var rect = new RectCollider(physicsObject, collisionType, width, height);
        physicsObject.Collider = rect;
        emptyRectGumGizmo?.AttachToPhysicsObject(physicsObject);
        return physicsObject;
    }

    public static PhysicsObject Circl(string label, float x, float y, ColliderType collisionType, float radius, GraphicalUiElement rootGraphicalUiElement = null, GraphicalUiElement graphicalUiElement = null)
    {
        EmptyCircleGizmo emptyCircleGizmo = null;
        if (rootGraphicalUiElement != null)
        {
            emptyCircleGizmo = new EmptyCircleGizmo(rootGraphicalUiElement, radius);
        }
        var physicsObject = new PhysicsObject(label, x, y, collisionType, graphicalUiElement, emptyCircleGizmo);
        var circl = new CirclCollider(physicsObject, collisionType, radius);
        physicsObject.Collider = circl;
        emptyCircleGizmo?.AttachToPhysicsObject(physicsObject);
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
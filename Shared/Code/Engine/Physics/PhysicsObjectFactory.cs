using Gum.Wireframe;
using Microsoft.Xna.Framework;
using System;

public class PhysicsObjectFactory
{
    public static readonly Color DebugColorPink = Color.Pink;

    //pre: toute les positions sont relatives au root topleft
    //si collidertype static, prend la position du root + position du physicsobject
    //sinon, position du physicsobject
    public static PhysicsObject Rect(Entity entity, string label, float x, float y, ColliderType collisionType, float width, float height, GraphicalUiElement graphicalUiElement = null, GraphicalUiElement rootGraphicalUiElement = null, Color? debugColor = null)
    {
        EmptyRectGumGizmo emptyRectGumGizmo = null;
        if (rootGraphicalUiElement != null)
        {
            emptyRectGumGizmo = new EmptyRectGumGizmo(rootGraphicalUiElement, debugColor ?? DebugColorPink);
        }
        var physicsObject = new PhysicsObject(label, x, y, collisionType, graphicalUiElement, emptyRectGumGizmo, entity);
        var rect = new RectCollider(physicsObject, collisionType, width, height);
        physicsObject.Collider = rect;
        emptyRectGumGizmo?.AttachToPhysicsObject(physicsObject);
        return physicsObject;
    }

    public static PhysicsObject Circl(string label, float x, float y, ColliderType collisionType, float radius, GraphicalUiElement rootGraphicalUiElement = null, GraphicalUiElement graphicalUiElement = null, Entity entity = null)
    {
        EmptyCircleGizmo emptyCircleGizmo = null;
        if (rootGraphicalUiElement != null)
        {
            emptyCircleGizmo = new EmptyCircleGizmo(rootGraphicalUiElement, radius);
        }
        var physicsObject = new PhysicsObject(label, x, y, collisionType, graphicalUiElement, emptyCircleGizmo, entity);
        var circl = new CirclCollider(physicsObject, collisionType, radius);
        physicsObject.Collider = circl;
        emptyCircleGizmo?.AttachToPhysicsObject(physicsObject);
        return physicsObject;
    }

    public static PhysicsObject AreaRectTriggerOnce(Entity entity, string label, float x, float y, float width, float height, Action onTrigger, GraphicalUiElement graphicalUiElement = null, GraphicalUiElement rootGraphicalUiElement = null, Color? debugColor = null)
    {
        var physicsObject = Rect(entity, label, x, y, ColliderType.AreaCastTrigger, width, height, graphicalUiElement, rootGraphicalUiElement, debugColor);
        physicsObject.Collider.OnCollisionAction = onTrigger;
        physicsObject.Gravity = Vector2.Zero;
        return physicsObject;
    }
}   
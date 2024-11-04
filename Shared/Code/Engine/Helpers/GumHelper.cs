using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using Microsoft.Xna.Framework;
using RenderingLibrary;
using System.Linq;

public class GumHelper
{
    /// <summary>
    /// Create a component located in the Components folder of the Gum project
    /// </summary>
    /// <param name="name">This is the name of the component. If there is subfodlers, you need to use path like so: Myfolder\\MyComponent</param>
    /// <param name="parentItemTarget">the parent item to add thee component into. Important to have a uniform position and size of the component.</param>
    /// <returns></returns>
    public static GraphicalUiElement InstanciateComponent(string name, GraphicalUiElement parentItemTarget)
    {
        var componentSave = ObjectFinder.Self.GumProjectSave.Components.First(item => item.Name == name);
        //addToManagers: false => so pipes are inside the parentItemTarget and not added in front of everything
        var componentRuntime = componentSave.ToGraphicalUiElement(SystemManagers.Default, addToManagers: false);
        parentItemTarget.Children.Add(componentRuntime);
        return componentRuntime;
    }

    public static void RemoveComponent(GraphicalUiElement parentItemTarget, GraphicalUiElement component)
    {
        parentItemTarget.Children.Remove(component);
    }

    public static Vector2 AddRootPosition(GraphicalUiElement root, float xScreen, float yScreen)
    {
        return new(root.AbsoluteLeft + xScreen, root.AbsoluteTop + yScreen);
    }

    /// <summary>
    /// You give the root element of your ingame world, and it will return the position 
    /// </summary>
    /// <param name="root"></param>
    /// <param name="xInRoot"></param>
    /// <param name="yInRoot"></param>
    /// <returns></returns>
    public static Vector2 RemoveRootPosition(GraphicalUiElement root, float xInRoot, float yInRoot)
    {
        return new(root.AbsoluteLeft - xInRoot, root.AbsoluteTop - yInRoot);
    }
}
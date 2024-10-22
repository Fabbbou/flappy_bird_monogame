using Gum.Wireframe;
using GumFormsSample;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGameGum.Input;
using System.Collections.Generic;

public class TouchscreenCursor : ICursor
{
    private List<GumTransparentButton> buttons = new();
    public void AddButton(GumTransparentButton button)
    {
#if ANDROID || IOS
            buttons.Add(button);
#endif
    }

    public void ClearButtons()
        {
            buttons.Clear();
        }
        
    public void UpdateButtons()
    {
#if ANDROID || IOS
        Activity();
        var buttonsCopy = new List<GumTransparentButton>(buttons);
        foreach (var button in buttonsCopy)
        {
            ClickButton(button);
        }
#endif
    }

    private void ClickButton(GumTransparentButton button)
    {
        if (PrimaryClick && button.HasCursorOver(this))
        {
            button.PushBtn?.Invoke();
        }
    }

    public int X => (int) mTouchLocation.Position.X;

    public int Y => (int)mTouchLocation.Position.Y;

    /// <summary>
    /// Returns the screen space (in pixels) change on the X axis since the last frame.
    /// </summary>
    public int XChange => (int)mTouchLocation.Position.X;

    /// <summary>
    /// Returns the screen space (in pixel) change on the Y axis since the last frame.
    /// </summary>
    public int YChange => (int)mTouchLocation.Position.Y;

    public int ScrollWheelChange => 0;

    public bool PrimaryPush
    {
        get
        {
            return this.mTouchLocation.State == TouchLocationState.Pressed;
        }
    }

    public bool PrimaryDown
    {
        get
        {
            return this.mTouchLocation.State == TouchLocationState.Pressed;
        }
    }

    public bool PrimaryClick
    {
        get
        {
            return this.mTouchLocation.State == TouchLocationState.Released;
        }
    }

    public bool PrimaryDoubleClick { get; private set; }

    // for now just return true, but we'll need to keep track of actual push/clicks eventually:
    public bool PrimaryClickNoSlide => PrimaryClick;

    public bool SecondaryPush => false;

    public bool SecondaryDown => false;

    public bool SecondaryClick => false;

    public bool SecondaryDoubleClick => false;

    public bool MiddlePush => false;

    public bool MiddleDown => false;

    public bool MiddleClick => false;
    
    public bool MiddleDoubleClick => false;

    public InteractiveGue WindowPushed { get; set; }
    public InteractiveGue WindowOver { get; set; }

    TouchLocation mTouchLocation;

    public void Activity()
    {
        mTouchLocation = GetFirstTouchLocation();
    }
    private TouchLocation GetFirstTouchLocation()
    {
        TouchCollection touchCollection = TouchPanel.GetState();
        if (touchCollection.Count > 0)
        {
            return touchCollection[0];
        }
        else
        {
            return new TouchLocation();
        }
    }

}

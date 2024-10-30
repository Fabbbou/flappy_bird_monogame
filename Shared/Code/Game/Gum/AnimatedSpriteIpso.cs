using Gum;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGame.Aseprite;
using MonoGameGum.GueDeriving;
using RenderingLibrary;
using RenderingLibrary.Graphics;


/// <summary>
/// Inspired from the original Gum.Wireframe.Sprite implementation for the IRenderableIpso implementations.
/// </summary>
public class AnimatedSpriteIpso : IRenderable
{
    private AnimatedSprite _animatedSprite;
    private GraphicalUiElement _el;

    public AnimatedSpriteIpso(AnimatedSprite animatedSprite, GraphicalUiElement el)
    {
        _animatedSprite = animatedSprite;
        _el = el;
    }

    public BlendState BlendState => BlendState.AlphaBlend;

    public bool Wrap => false;

    public void PreRender(){}

    public void Render(ISystemManagers managers)
    {

    }

    public void Update(GameTime gameTime)
    {
        _animatedSprite.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
        //_el.Texture = _animatedSprite.TextureRegion.Texture;
        ((GraphicalUiElement)_el).Rotation = _animatedSprite.Rotation;
    }
}
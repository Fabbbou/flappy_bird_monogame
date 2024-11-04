using Gum.Converters;
using Gum.DataTypes.Variables;
using Gum.DataTypes;
using MonoGameGum.Forms.Controls;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Gum.Wireframe;
using System;

namespace GumFormsSample
{
    public class GumTransparentButton : InteractiveGue
    {
        public static GraphicalUiElement AttachButton(string name, GraphicalUiElement parentToExplore, Action actionClicked = null, Action actionPushed = null, Color? overrideDebugColor = null)
        {
            var component = parentToExplore.GetGraphicalUiElementByName(name);
            var button = new GumTransparentButton(overrideDebugColor: overrideDebugColor);
            if(actionClicked != null)
            {
                button.Click += (o, e) => actionClicked();
            }
            if (actionPushed != null)
            {
                button.Push += (o, e) => actionPushed();
            }
            component.Children.Add(button);
            return component;
        }

        public static GraphicalUiElement AttachButton(GraphicalUiElement component, EventHandler onPushAction, Color? overrideDebugColor = null)
        {
            var button = new GumTransparentButton(overrideDebugColor: overrideDebugColor);
            button.Push += onPushAction;
            component.Children.Add(button);
            return component;
        }

        public static GraphicalUiElement AttachButton(GraphicalUiElement component, Action actionClicked = null, Action actionPushed = null, Color? overrideDebugColor = null)
        {
            return AttachButton(component, (o, e) => actionClicked(), overrideDebugColor: overrideDebugColor);
        }

        public readonly static Color TransparentRed = new Color(255, 0, 0, 123);
        public Color colorDebug = TransparentRed;
        public GumTransparentButton(bool fullInstantiation = true, bool tryCreateFormsObject = true, Color? overrideDebugColor = null) : base(new InvisibleRenderable()) {
            if (GizmosRegistry.Instance.IsDebugging)
            {
                colorDebug = overrideDebugColor ?? TransparentRed;
            }
            else
            {
                colorDebug = Color.Transparent;
            }
            
            
            if (fullInstantiation)
            {
                this.Width = 100f;
                this.Height = 100f;
                this.WidthUnits = DimensionUnitType.Percentage;
                this.HeightUnits = DimensionUnitType.Percentage;
                ColoredRectangleRuntime item = new ColoredRectangleRuntime
                {
                    Width = 100f,
                    Height = 100f,
                    WidthUnits = DimensionUnitType.Percentage,
                    HeightUnits = DimensionUnitType.Percentage,
                    Name = "ButtonBackground",
                    Color = colorDebug,
                };
                this.Children.Add(item);
                AddCategory(new StateSaveCategory
                {
                    Name = "ButtonCategory",
                    States =
                {
                    new StateSave
                    {
                        Name = "Enabled",
                        Variables = new List<VariableSave>
                        {
                            new VariableSave
                            {
                                Name = "ButtonBackground.Color",
                                Value = colorDebug,
                            }
                        }
                    },
                    new StateSave
                    {
                        Name = "Pushed",
                        Variables = new List<VariableSave>
                        {
                            new VariableSave
                            {
                                Name = "ButtonBackground.Color",
                                Value = colorDebug,
                            }
                        }
                    },
                }
                });
            }

            if (tryCreateFormsObject)
            {
                FormsControlAsObject = new Button(this);
            }
        }
    }
}
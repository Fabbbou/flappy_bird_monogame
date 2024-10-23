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
        public static void AttachButton(GraphicalUiElement component, EventHandler onPushAction)
        {
            var button = new GumTransparentButton();
            button.Push += onPushAction;
            component.Children.Add(button);
        }

        public static void AttachButton(GraphicalUiElement component, Action onPushAction)
        {
            var button = new GumTransparentButton();
            button.Push += (o, e) => onPushAction();
            component.Children.Add(button);
        }

        public readonly static Color TransparentRed = new Color(255, 0, 0, 123);
        public GumTransparentButton(bool fullInstantiation = true, bool tryCreateFormsObject = true) : base(new InvisibleRenderable()) {
            Color color = GizmosRegistry.Instance.IsDebugging ? TransparentRed : Color.Transparent;
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
                    Color = color,
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
                                Value = color,
                            }
                        }
                    },
                    //new StateSave
                    //{
                    //    Name = "Highlighted",
                    //    Variables = new List<VariableSave>
                    //    {
                    //        new VariableSave
                    //        {
                    //            Name = "ButtonBackground.Color",
                    //            Value = new Color(0, 0, 160)
                    //        }
                    //    }
                    //},
                    new StateSave
                    {
                        Name = "Pushed",
                        Variables = new List<VariableSave>
                        {
                            new VariableSave
                            {
                                Name = "ButtonBackground.Color",
                                Value = color,
                            }
                        }
                    },
                    //new StateSave
                    //{
                    //    Name = "Disabled",
                    //    Variables = new List<VariableSave>
                    //    {
                    //        new VariableSave
                    //        {
                    //            Name = "ButtonBackground.Color",
                    //            Value = new Color(48, 48, 64)
                    //        }
                    //    }
                    //}
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
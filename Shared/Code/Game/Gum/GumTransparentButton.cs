using Gum.Converters;
using Gum.DataTypes.Variables;
using Gum.DataTypes;
using MonoGameGum.Forms.Controls;
using MonoGameGum.Forms.DefaultVisuals;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Gum.Wireframe;

namespace GumFormsSample
{
    internal class GumTransparentButton : InteractiveGue
    {
        public GumTransparentButton(bool fullInstantiation = true, bool tryCreateFormsObject = true) : base(new InvisibleRenderable()) {
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
                    Name = "ButtonBackground"
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
                                Value = Color.Transparent,
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
                    //new StateSave
                    //{
                    //    Name = "Pushed",
                    //    Variables = new List<VariableSave>
                    //    {
                    //        new VariableSave
                    //        {
                    //            Name = "ButtonBackground.Color",
                    //            Value = new Color(0, 0, 96)
                    //        }
                    //    }
                    //},
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
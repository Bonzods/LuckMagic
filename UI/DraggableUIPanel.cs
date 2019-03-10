using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LuckMagic.UI
{
    internal class DraggableUIPanel : UIPanel
    {
        private Vector2 offset;
        public bool dragging;

        public DraggableUIPanel()
        {
            OnMouseDown += new UIElement.MouseEvent(StartDrag);
            OnMouseUp += new UIElement.MouseEvent(EndDrag);
        }

        private void StartDrag(UIMouseEvent evt, UIElement listen)
        {
            Main.NewText("Jogging");
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            dragging = true;
        }

        private void EndDrag(UIMouseEvent evt, UIElement listen)
        {
            Vector2 end = evt.MousePosition;
            dragging = false;

            Left.Set(end.X - offset.X, 0f);
            Top.Set(end.Y - offset.Y, 0f);

            Recalculate();
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (dragging)
            {
                Left.Set(Main.mouseX - offset.X, 0f);
                Top.Set(Main.mouseY - offset.Y, 0f);
            }

            var space = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(space))
            {
                Left.Pixels = Math.Max(0, Math.Min(Left.Pixels, space.Right - Width.Pixels));
                Top.Pixels = Math.Max(0, Math.Min(Top.Pixels, space.Bottom - Height.Pixels));
            }
        }
    }
}
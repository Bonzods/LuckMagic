using Terraria;
using Terraria.UI;
using Terraria.ID;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.DataStructures;

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using LuckMagic.Items;

namespace LuckMagic.UI
{
	internal class LuckUI : UIState
	{
		public static bool Visible;
		public static LuckItem MyItem;
		public UILuckBar LuckBar;

		public override void OnInitialize()
		{
			LuckBar = new UILuckBar();
			LuckBar.SetPadding(0);
			LuckBar.Width.Set(235, 0f);
			LuckBar.Height.Set(50, 0f);
			LuckBar.Left.Set(Main.screenWidth / 2f - 235 / 2f, 0f);
			LuckBar.Top.Set(40, 0f);

			LuckBar.OnMouseDown += new UIElement.MouseEvent(StartDrag);
			LuckBar.OnMouseUp += new UIElement.MouseEvent(EndDrag);

			Append(LuckBar);
		}

		private Vector2 offset;
		public bool dragging;

		private void StartDrag(UIMouseEvent evt, UIElement listener)
		{
			offset = new Vector2(evt.MousePosition.X - LuckBar.Left.Pixels, evt.MousePosition.Y - LuckBar.Top.Pixels);
			dragging = true;
		}

		private void EndDrag(UIMouseEvent evt, UIElement listener)
		{
			dragging = false;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (LuckBar.ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}

			if (dragging)
			{
				LuckBar.Left.Set(Main.mouseX - offset.X, 0f);
				LuckBar.Top.Set(Main.mouseY - offset.Y, 0f);
				Recalculate();
			}
		}
	}
	
	public class UILuckBar : UIElement
	{
		public UILuckBar()
		{
			Width.Set(235, 0f);
			Height.Set(50, 0f);
		}

		protected override void DrawSelf(SpriteBatch batch)
		{
			CalculatedStyle dimensions = GetInnerDimensions();

			float x = dimensions.X;
			float y = dimensions.Y;

			LuckPlayer player = Main.LocalPlayer.GetModPlayer<LuckPlayer>();
			LuckItem item = LuckUI.MyItem;

			if (item != null)
			{
				Texture2D texture = Main.itemTexture[item.item.type];
				Vector2 size = texture.Size();
				batch.Draw(texture, new Vector2(x + size.X / 2f, y + size.Y / 2f), null, Color.White, 0f, size / 2f, 0.8f, SpriteEffects.None, 0f);
				
				if (item.MaxLuck > 0)
				{
					Utils.DrawBorderStringFourWay(batch, Main.fontItemStack, String.Format("{0}'s Luck: {1} / {2}", item.item.Name, Math.Floor(item.Luck), item.MaxLuck), x + size.X, y, new Color(127, 255, 127), Color.Black, new Vector2(0), 1f);
					y += 20;
				}
				
				Utils.DrawBorderStringFourWay(batch, Main.fontItemStack, String.Format("Luck: {0}", Utils.Clamp(Math.Floor(player.Luck), 0, 10000)), x + size.X, y, new Color(127, 255, 127), Color.Black, new Vector2(0), 1f);
			}
		}
	}
}
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using LuckMagic.UI;

namespace LuckMagic
{
	class LuckMagic : Mod
	{
		private UserInterface _userInterface;
		internal LuckUI LuckUI;

		public LuckMagic()
		{
		}

		public override void Load()
		{
			LuckUI = new LuckUI();
			LuckUI.Activate();
			_userInterface = new UserInterface();
			_userInterface.SetState(LuckUI);
		}

		public override void UpdateUI(GameTime gameTime)
		{
			if (_userInterface != null && LuckUI.Visible)
			{
				_userInterface.Update(gameTime);
			}
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseIndex != 1)
			{
				layers.Insert(mouseIndex, new LegacyGameInterfaceLayer(
				"LuckMagic: Luck UI",
				delegate {
					if (LuckUI.Visible)
					{
						_userInterface.Draw(Main.spriteBatch, new GameTime());
					}
					return true;
				},
				InterfaceScaleType.UI));
			}
		}
	}
}
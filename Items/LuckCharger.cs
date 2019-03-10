using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

using System;

namespace LuckMagic.Items
{
	public class LuckCharger : LuckItem
	{
		private bool Active = false;
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luck Charger");
			Tooltip.SetDefault("Takes your luck and charges your items");
		}

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 38;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 4;
			item.value = 10000;
			item.rare = 10;
		}

		public override void UpdateInventory(Player player)
		{
			base.UpdateInventory(player);
			
			if (Active && MyPlayer.Luck >= 0.5)
			{
				foreach (LuckItem luckItem in MyPlayer.LuckItems)
				{
					float zucc = (float) Utils.Clamp(MyPlayer.Luck, 0, 1);
					if (luckItem.CanCharge && luckItem.Luck < luckItem.MaxLuck && luckItem.item != item)
					{
						luckItem.Luck += zucc;
						MyPlayer.Luck -= zucc;
					}
					
					if (MyPlayer.Luck < 0.5)
						break;
				}
			}
		}
		
		public override Color? GetAlpha(Color light)
		{
			return Active ? Main.DiscoColor : Color.White;
		}
		
		public override bool UseItem(Player player)
		{
			Active = !Active;
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 10);
			recipe.AddIngredient(ItemID.Amber, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
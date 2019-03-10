using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

using System;

namespace LuckMagic.Items
{
	public class LuckBadge : LuckItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luck Badge");
			Tooltip.SetDefault("Passively generates Luck");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = 10000;
			item.rare = 10;
			item.accessory = true;
		}
		
		public override void UpdateEquip(Player player)
		{
			base.UpdateEquip(player);
			
			if (player.active && !player.dead)
			{
				LuckPlayer luckPlayer = player.GetModPlayer<LuckPlayer>();
				luckPlayer.Luck += 1 / 60f;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 10);
			recipe.AddIngredient(ItemID.Emerald, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
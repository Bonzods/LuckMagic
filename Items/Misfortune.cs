using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

using System;

namespace LuckMagic.Items
{
	public class Misfortune : LuckItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Misfortune");
			Tooltip.SetDefault("Voids your luck");
		}

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 38;
			item.damage = 16;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = 5;
			item.ranged = true;
			item.value = 10000;
			item.rare = 10;
			item.autoReuse = true;
			item.UseSound = SoundID.Item5;
			item.shootSpeed = 20f;
			item.shoot = 1;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			bool success = MyPlayer.ConsumeLuck(2);
			item.noUseGraphic = success;
			return success;
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
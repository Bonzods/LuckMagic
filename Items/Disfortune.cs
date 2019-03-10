using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace LuckMagic.Items
{
	public class Disfortune : LuckItem
	{
        public override float MaxLuck
        {
            get
            {
                return 100;
            }
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Disfortune");
			Tooltip.SetDefault("Striking enemies gives it Luck!\nRight click to inject its Luck into you!");
		}

		public override void SetDefaults()
		{
			item.damage = 15;
			item.melee = true;
			item.width = 38;
			item.height = 38;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.autoReuse = true;
			item.UseSound = SoundID.Item4;
		}

		public override bool AltFunctionUse(Player player)
		{
			return Luck > 0;
		}

		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.useStyle = 4;
				item.noMelee = true;
				MyPlayer.AddLuck(Luck);
				Luck = 0;
			}
			else
			{
				item.useStyle = 1;
				item.noMelee = false;
			}
			return true;
		}

		public override void OnHitNPC(Player player, NPC npc, int damage, float knockBack, bool crit)
		{
			AddLuck(1);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 10);
			recipe.AddIngredient(ItemID.Emerald, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
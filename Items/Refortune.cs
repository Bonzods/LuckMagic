using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace LuckMagic.Items
{
	public class Refortune : LuckItem
	{
        public override float MaxLuck
        {
            get
            {
                return 500;
            }
        }
		
        public override bool CanCharge
        {
            get
            {
                return true;
            }
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Refortune");
			Tooltip.SetDefault("Left click to store Luck\nRight click to take it out");
		}

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 38;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 4;
			item.value = 10000;
			item.rare = 6;
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
				MyPlayer.AddLuck(Luck);
				Luck = 0;
			}
			else if (MyPlayer.Luck > 0)
			{
				float add = Utils.Clamp(MyPlayer.Luck, 0, MaxLuck);
				float actual = AddLuck(add);
				MyPlayer.AddLuck(-actual);
			}
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
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

using Microsoft.Xna.Framework;

using System;
using System.Linq;
using System.Collections.Generic;

using LuckMagic.UI;

namespace LuckMagic.Items
{
    public abstract class LuckItem : ModItem
    {
        public float Luck = 0;
        public virtual float MaxLuck
        {
            get
            {
                return 0;
            }
        }
		public virtual bool CanCharge
		{
			get
			{
				return false;
			}
		}

        public override TagCompound Save()
        {
            return new TagCompound()
            {
                {"Luck", Luck},
            };
        }

        public override void Load(TagCompound tag)
        {
            Luck = tag.GetFloat("Luck");
        }

        public float AddLuck(float luck)
        {
            float old = Luck;
            Luck = Utils.Clamp(Luck + luck, 0, MaxLuck);
            return Luck - old;
        }

        public LuckPlayer MyPlayer
        {
            get
            {
                return Main.LocalPlayer.GetModPlayer<LuckPlayer>();
            }
        }

        public override void HoldItem(Player player)
        {
            LuckUI.Visible = true;
            LuckUI.MyItem = this;
        }
		
		/*public override void UpdateInventory(Player player)
		{
			if (MyPlayer.player == player)
			{
				MyPlayer.LuckItems.Add(this);
			}
		}*/
		
		public override void UpdateEquip(Player player)
		{
			if (LuckUI.MyItem == null)
			{
				LuckUI.Visible = true;
				LuckUI.MyItem = this;
			}
		}
    }
}
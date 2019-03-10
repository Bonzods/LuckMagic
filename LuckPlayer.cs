using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;

using System;
using System.Linq;
using System.Collections.Generic;

using LuckMagic.UI;
using LuckMagic.Items;

namespace LuckMagic
{
	public class LuckPlayer : ModPlayer
	{
		public float Luck = 0;
		public float LuckMultiplier = 1;
		public List<LuckItem> LuckItems;

		public override TagCompound Save()
		{
			return new TagCompound()
			{
				{"Luck", Luck}
			};
		}

		public override void Load(TagCompound tag)
		{
			Luck = tag.GetFloat("Luck");
		}

		public void AddLuck(float luck, bool multiplier = false)
		{
			float add = luck * (multiplier ? LuckMultiplier : 1);
			Luck = Utils.Clamp(Luck + add, 0, 10000);
		}

		public bool ConsumeLuck(float luck)
		{
			float toSate = 0; //How much Luck we need to get
			if (LuckItems.Count > 0) //Check if we have any luck storers
			{
				foreach (LuckItem luckItem in LuckItems)
				{
					toSate = toSate + luckItem.Luck;
					
					if (toSate >= luck) //Break the loop when we get enough Luck
					{
						break;
					}
				}
				
				bool enough = toSate >= luck || Luck > (luck - toSate); //Check if we got enough from inventory, otherwise from the player
				
				if (enough)
				{
					toSate = luck; //Reset required Luck, we are going to count down now
					
					foreach (LuckItem luckItem in LuckItems)
					{
						float actual = Utils.Clamp(toSate, 0, Math.Min(toSate, luckItem.Luck)); //Extremely ugly way to make sure the least is subtracted
						toSate -= actual;
						luckItem.Luck -= actual;
						
						if (toSate <= 0)
						{
							break;
						}
					}
					
					if (toSate >= 0) //If the inventory doesn't have enough, we use the player's
					{
						Luck -= toSate;
						return true;
					}
					
					return true;
				}
			}
			
			if (Luck > luck) //If we have no inventory items, take the player's Luck
			{
				Luck -= luck;
				
				return true;
			}
			
			return false;
		}

		public override void ResetEffects()
		{
			LuckUI.Visible = false;
			LuckUI.MyItem = null;
			LuckMultiplier = 1f;
			LuckItems = new List<LuckItem>();
			
			foreach (Item item in player.inventory)
			{
				if (item.modItem != null && item.modItem is LuckItem)
				{
					LuckItems.Add((LuckItem) item.modItem);
				}
			}
		}
	}
}
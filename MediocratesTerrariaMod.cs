using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;
using Terraria.IO;
using System;
using Steamworks;

namespace MediocratesTerrariaMod
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class MediocratesTerrariaMod : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {

            int index = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            if (index != -1)
            {
                tasks.Insert(index + 1, new PassLegacy("MyChestMod", ModifyChests));
            }
        }

        private void ModifyChests(GenerationProgress progress, GameConfiguration configuration)
        {
            //for each chest
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                    continue;

                Random rand = new Random(Main.worldID);
                int amountOfItemsToAdd = rand.Next(1, 4);

                // add items to chest.
                for (int a = 0; a < amountOfItemsToAdd; a++)
                {
                    // Find first empty slot
                    for (int i = 0; i < chest.item.Length; i++)
                    {
                        if (chest.item[i] == null || chest.item[i].type == ItemID.None)
                        {
                            int randomItemId = PreHardmodeItems[Main.rand.Next(PreHardmodeItems.Length)];
                            chest.item[i].SetDefaults(randomItemId);

                            if(chest.item[i].maxStack > 1)
                            {
                                chest.item[i].stack = rand.Next(2, 11); // set amount if stackable
                                break; // only add one item
                            }
                            else
                            {
                                chest.item[i].stack = 1; // set amount if stackable
                                break; // only add one item
                            }    
                        }
                    }
                }
            }
        }

        public static int[] PreHardmodeItems =
       {
            ItemID.RocketBoots,
            ItemID.CelestialMagnet,
            ItemID.PygmyNecklace,
            ItemID.CelestialCuffs,
            ItemID.AngelWings,
            ItemID.DemonWings,
            ItemID.Jetpack,
            ItemID.FairyWings,
            ItemID.FinWings,
            ItemID.FrozenWings,
            ItemID.HarpyWings,
            ItemID.QueenSlimeMountSaddle,
            ItemID.WolfMountItem,
            ItemID.DarkMageBookMountItem,
            ItemID.LightningBoots,
            ItemID.WormScarf,
            ItemID.BerserkerGlove,
            ItemID.ObsidianShield,
            ItemID.Diamond,
            ItemID.Ruby,
            ItemID.Emerald,
            ItemID.Sapphire,
            ItemID.Amethyst,

            //Weapons
            ItemID.HiveFive,
            ItemID.NightsEdge,
            ItemID.LightsBane,
            ItemID.YoyoBag,
            ItemID.MoltenQuiver,
            ItemID.MoltenFury,
            ItemID.DemonScythe,
            ItemID.HornetStaff,
            ItemID.ImpStaff,
            ItemID.VampireFrogStaff,

            //Tools
            ItemID.DeathbringerPickaxe,
            ItemID.NightmarePickaxe,

            //Potions
            ItemID.IronskinPotion,
            ItemID.SwiftnessPotion,
            ItemID.RegenerationPotion,
            ItemID.EndurancePotion,
            ItemID.SummoningPotion,
            ItemID.RagePotion,
            ItemID.WrathPotion,
            ItemID.HeartreachPotion,
            ItemID.SliceOfCake,
            ItemID.Sake,
            ItemID.FlaskofFire,
            ItemID.FlaskofPoison,
            ItemID.ArcheryPotion,

            //Armor
            ItemID.WizardHat,
            ItemID.DiamondRobe,
            ItemID.MoltenHelmet,
            ItemID.MoltenGreaves,
            ItemID.MoltenBreastplate,
            ItemID.NecroHelmet,
            ItemID.NecroBreastplate,
            ItemID.NecroGreaves,
            ItemID.FlinxFurCoat,
            ItemID.ObsidianPants,
            ItemID.ObsidianHelm,
            ItemID.ObsidianShirt,
        };
    }
}
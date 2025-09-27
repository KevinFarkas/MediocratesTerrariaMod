using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;
using Terraria.IO;
using System;
using System.Security.Permissions;
using Steamworks;

namespace MediocratesTerrariaMod
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class MediocratesTerrariaMod : ModSystem
    {

        private static Mod calamityMod;
        public static bool IsCalamityLoaded => ModLoader.TryGetMod("CalamityMod", out calamityMod);

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
            bool lastItemWasVanilla = true;

            //for each chest
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                    continue;

                Random rand = new Random(Main.worldID);
                int amountOfItemsToAdd = rand.Next(1, 4);
                //if amountOfItemsToAdd is 1, use a rare loot table.
                //if amountOfItemsToAdd is 2, use a somewhat-rare loot table.
                //if amountOfItemsToAdd is 3, use a decent loot table.
                //if amountOfItemsToAdd is 4, use a not good loot table.
                //if amountOfItemsToAdd is 5, use a worst loot table.

                // add items to chest.

                #region works with random Calamity and Vanilla items

                for (int a = 0; a < amountOfItemsToAdd; a++)
                {
                    // Find first empty slot
                    for (int i = 0; i < chest.item.Length; i++)
                    {
                        if (chest.item[i] == null || chest.item[i].type == ItemID.None)
                        {
                            if (IsCalamityLoaded)
                            {
                                if (calamityMod != null)
                                {
                                    if (lastItemWasVanilla)
                                    {
                                        string randomCalamityItemName = CalamityItemNames[Main.rand.Next(CalamityItemNames.Length)];

                                        if (calamityMod.TryFind<ModItem>(randomCalamityItemName, out ModItem exampleItem))
                                        {
                                            // Get the item ID
                                            int exampleItemId = exampleItem.Type;

                                            lastItemWasVanilla = false;

                                            chest.item[i].SetDefaults(exampleItemId);
                                            if (chest.item[i].maxStack > 1)
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
                                    else
                                    {
                                        int randomItemId = PreHardmodeItems[Main.rand.Next(PreHardmodeItems.Length)];

                                        lastItemWasVanilla = true;

                                        chest.item[i].SetDefaults(randomItemId);

                                        if (chest.item[i].maxStack > 1)
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
                            else
                            {
                                int randomItemId = PreHardmodeItems[Main.rand.Next(PreHardmodeItems.Length)];
                                chest.item[i].SetDefaults(randomItemId);

                                if (chest.item[i].maxStack > 1)
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

                #endregion

            }
        }

        public static string[] CalamityItemNames =
        {
            "GeliticBlade",
            "Aestheticus",
            "StressPills",
            "Goobow",
            "TeslaPotion",
            "Zerg Potion",
            "BlackAnurian",
            "RadiantOoze",
            "RustyBeaconPrototype",
            "DankStaff",
            "HerringStaff",
            "SlimePuppetStaff",
            "StatigelArmor",
            "StatigelGreaves",
            "StatigelHelm",
            "StatigelHeadgear",
            "StatigelCap",
            "StatigelHood", 
            "StatigelMask",
            "BloodyWormTooth",
            "OldDie",
            "TheFirstShadowflame",
            "JellyChargedBattery",
            "SandSharkToothNecklace",
            "BloodyWormScarf",
            "Grand Gelatin",
            "AmalgamatedBrain",
            "TheCommunity",
            "AngelTreads",
            "LivingDew",
            "ReaverHeadgear",
            "GazeofCrysthamyr",
            "TheCartofGods",
            "SuspiciousLookingJellyBean",
            "MarniteArchitectarmor",
            "Cosmolight",
            "Terminus",
            "AncientFossil",
            "ArchaicPowder",
            "OceanCrest",
            "SpelunkersAmulet",
        };


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
            ItemID.PirateShipMountItem,
            ItemID.LightningBoots,
            ItemID.WormScarf,
            ItemID.BerserkerGlove,
            ItemID.ObsidianShield,
            ItemID.Diamond,
            ItemID.Ruby,
            ItemID.Emerald,
            ItemID.Sapphire,
            ItemID.Amethyst,
            ItemID.TerrasparkBoots,
            ItemID.SummonerEmblem,
            ItemID.RangerEmblem,
            ItemID.SorcererEmblem,
            ItemID.WarriorEmblem,
            ItemID.SpookyWoodMountItem,
            ItemID.TeleportationPylonHallow,
            ItemID.AntiGravityHook,
            ItemID.CelestialStone,
            ItemID.CelestialShell,
            ItemID.EmeraldHook,
            ItemID.AmethystHook,
            ItemID.SapphireHook,
            ItemID.WebSlinger,
            ItemID.LightningCarrot,
            ItemID.BallOfFuseWire,
            ItemID.WispinaBottle,
            ItemID.SuspiciousLookingTentacle,
            ItemID.FairyBell,

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
            ItemID.AncientChisel,
            ItemID.CnadyCanePickaxe,
            ItemID.FossilPickaxe,
            ItemID.BonePickaxe,
            ItemID.PlatinumPickaxe,
            ItemID.ReaverShark,
            ItemID.DirtiestBlock,

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
            ItemID.InfernoPotion,
            ItemID.MiningPotion,

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
            ItemID.HallowedHood,
            ItemID.HallowedHeadgear,
            ItemID.HallowedHelmet,
            ItemID.HallowedMask,
        };
    }
}
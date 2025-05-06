using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;
using Terraria.IO;
using System;

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

            #region Randomize chests items

            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                    continue;

                //TODO:fix these checks.
                // if this isn't a gold chest or a wood chest, or is a locked gold chest then stop
                //  if (!IsVal idChest(chest))
                //     return;

                for (int i = 0; i < Chest.maxItems; i++)
                {
                    if (chest.item[i]?.type > ItemID.None)
                    {

                        //TODO:fix these checks.
                        //if it's not a modded item.
                        // if (!IsModdedItem(chest.item[i]))
                        //{
                        //remove whitelisted items
                        // var nonWhitelistedItems = GetNonWhitelistedItems(PreHardmodeItems, WhiteListedItems);
                        var nonWhitelistedItems = PreHardmodeItems;
                        //pull a random item id from the filtered list.
                        int randomItemType = nonWhitelistedItems[Main.rand.Next(nonWhitelistedItems.Length)];
                        chest.item[i].SetDefaults(randomItemType);

                        //makes large stacks happen a lot.
                        //chest.item[i].stack = (chest.item[i].maxStack > 1)
                        //    ? Main.rand.Next(1, chest.item[i].maxStack + 1)
                        //    : 1;

                        chest.item[i].stack = (chest.item[i].maxStack > 1)
                        ? Math.Min(chest.item[i].maxStack, (int)(Math.Pow(Main.rand.NextDouble(), 2) * chest.item[i].maxStack) + 1) : 1;

                        // }

                    }
                }

            }

            #endregion

        }

        bool IsModdedItem(Item item)
        {
            return item != null && item.ModItem != null;
        }

        public int[] GetNonWhitelistedItems(int[] itemsToFilter, int[] whiteListedItems)
        {
            HashSet<int> blacklist = new HashSet<int>(whiteListedItems);
            List<int> filtered = new List<int>();

            foreach (int itemID in itemsToFilter)
            {
                if (!blacklist.Contains(itemID))
                {
                    filtered.Add(itemID);
                }
            }

            return filtered.ToArray();
        }

        /// <summary>
        /// Returns true if this chest is one that we allow to be modified.  
        /// </summary>
        /// <param name="chest"></param>
        /// <returns></returns>
        public bool IsValidChest(Chest chest)
        {
            return IsGoldChest(chest) || IsWoodenChest(chest);
        }


        /// <summary>
        /// Return true if this is a gold chest.  return false if it's locked or anything else.
        /// </summary>
        /// <param name="chest"></param>
        /// <returns></returns>
        public bool IsGoldChest(Chest chest)
        {
            if (chest == null)
                return false;

            Tile tile = Framing.GetTileSafely(chest.x, chest.y);

            // Check if it's a chest tile
            if (tile.TileType != TileID.Containers)
                return false;

            int style = tile.TileFrameX / 36;

            // Gold chests are style 1
            if (style != 1)
                return false;

            // Locked gold chests have frameY >= 36
            return tile.TileFrameY < 36;
        }

        /// <summary>
        /// Return true if this is a wooden chest.
        /// </summary>
        /// <param name="chest"></param>
        /// <returns></returns>
        public bool IsWoodenChest(Chest chest)
        {
            if (chest == null) return false;

            Tile tile = Framing.GetTileSafely(chest.x, chest.y);

            // Check if it's a chest tile
            if (tile.TileType != TileID.Containers) return false;

            // Get chest style from tile frameX
            int style = tile.TileFrameX / 36;

            // Wooden chests are style 0
            return style == 0;
        }

        public static int[] WhiteListedItems =
        {
            ItemID.BalloonPufferfish,
            ItemID.Flipper,
            ItemID.FlurryBoots,
            ItemID.FrogLeg,
            ItemID.LuckyHorseshoe,
            ItemID.SailfishBoots,
            ItemID.ShinyRedBalloon,
            ItemID.WaterWalkingBoots,
            ItemID.DepthMeter,
            ItemID.Compass,
            ItemID.Radar,
            ItemID.LifeformAnalyzer,
            ItemID.TallyCounter,
            ItemID.MetalDetector,
            ItemID.Stopwatch,
            ItemID.DPSMeter,
            ItemID.MechanicalLens,
            ItemID.Ruler,
            ItemID.HandWarmer,
            ItemID.Nazar,
            ItemID.PanicNecklace,
            ItemID.Shackle,
            ItemID.SharkToothNecklace,
            ItemID.Toolbelt,
            ItemID.Toolbox,
            ItemID.PaintSprayer,
            ItemID.ExtendoGrip,
            ItemID.PortableCementMixer,
            ItemID.BrickLayer,
            ItemID.ClothierVoodooDoll,
            ItemID.GuideVoodooDoll,
            ItemID.FlowerBoots,
            ItemID.Umbrella,
            //Weapons
            ItemID.ZombieArm,
            ItemID.BreathingReed,
            ItemID.BoneSword,
            ItemID.CandyCaneSword,
            ItemID.DyeTradersScimitar,
            ItemID.FalconBlade,
            ItemID.Rally,
            ItemID.Code1,
            ItemID.Spear,
            ItemID.Trident,
            ItemID.TheRottedFork,
            ItemID.Swordfish,
            ItemID.WoodenBoomerang,
            ItemID.EnchantedBoomerang,
            ItemID.ThornChakram,
            ItemID.ChainKnife,
            ItemID.TheMeatball,
            ItemID.Handgun,
            ItemID.Revolver,
            ItemID.Musket,
            ItemID.FlintlockPistol,
            ItemID.Shuriken,
            ItemID.ThrowingKnife,
            ItemID.Snowball,
            ItemID.SpikyBall,
            ItemID.Bone,
            ItemID.RottenEgg,
            ItemID.Javelin,
            ItemID.BoneJavelin,
            ItemID.FlareGun,
            ItemID.Blowpipe,
            ItemID.Blowgun,
            ItemID.SnowballCannon,
            ItemID.Harpoon,
            ItemID.WandofSparking,
            ItemID.Vilethorn,
            ItemID.MagicMissile,
            ItemID.AquaScepter,
            ItemID.SnowballLauncher,
            //explosives
            ItemID.Grenade,
            //Crafting
            ItemID.AntlionMandible,
            ItemID.BlackInk,
            ItemID.BlackLens,
            ItemID.CyanHusk,
            ItemID.EmptyDropper,
            ItemID.Feather,
            ItemID.GiantHarpyFeather,
            ItemID.Lens,
            ItemID.PinkGel,
            ItemID.PurpleMucos,
            ItemID.RedHusk,
            ItemID.RottenChunk,
            ItemID.SharkFin,
            ItemID.Stinger,
            ItemID.Vertebrae,
            ItemID.VioletHusk,
            ItemID.WormTooth
        };

        public static int[] PreHardmodeItems =
       {
            ItemID.Aglet,
            ItemID.BalloonPufferfish,
            ItemID.AnkletoftheWind,
            ItemID.ClimbingClaws,
            ItemID.CloudinaBottle,
            ItemID.Flipper,
            ItemID.FlurryBoots,
            ItemID.FlyingCarpet,
            ItemID.FrogLeg,
            ItemID.HermesBoots,
            ItemID.IceSkates,
            ItemID.LavaCharm,
            ItemID.LuckyHorseshoe,
            ItemID.SailfishBoots,
            ItemID.SandstorminaBottle,
            ItemID.ShinyRedBalloon,
            ItemID.ShoeSpikes,
            ItemID.TsunamiInABottle,
            ItemID.WaterWalkingBoots,
            ItemID.RocketBoots,
            ItemID.DepthMeter,
            ItemID.Compass,
            ItemID.Radar,
            ItemID.LifeformAnalyzer,
            ItemID.TallyCounter,
            ItemID.MetalDetector,
            ItemID.Stopwatch,
            ItemID.DPSMeter,
            ItemID.MechanicalLens,
            ItemID.Ruler,
            ItemID.BandofRegeneration,
            ItemID.BandofStarpower,
            ItemID.CelestialMagnet,
            ItemID.Bezoar,
            ItemID.CobaltShield,
            ItemID.FeralClaws,
            ItemID.HandWarmer,
            ItemID.MagmaStone,
            ItemID.Nazar,
            ItemID.ObsidianRose,
            ItemID.PanicNecklace,
            ItemID.Shackle,
            ItemID.SharkToothNecklace,
            ItemID.PygmyNecklace,
            ItemID.Toolbelt,
            ItemID.Toolbox,
            ItemID.PaintSprayer,
            ItemID.ExtendoGrip,
            ItemID.PortableCementMixer,
            ItemID.BrickLayer,
            ItemID.HotlineFishingHook,
            ItemID.BlackCounterweight,
            ItemID.YellowCounterweight,
            ItemID.BlueCounterweight,
            ItemID.RedCounterweight,
            ItemID.PurpleCounterweight,
            ItemID.GreenCounterweight,
            ItemID.ClothierVoodooDoll,
            ItemID.GuideVoodooDoll,
            ItemID.JellyfishNecklace,
            ItemID.FlowerBoots,
            ItemID.Umbrella,
            //Weapons
            ItemID.BladedGlove,
            ItemID.ZombieArm,
            ItemID.BreathingReed,
            ItemID.BoneSword,
            ItemID.CandyCaneSword,
            ItemID.Katana,
            ItemID.IceBlade,
            ItemID.LightsBane,
            ItemID.Muramasa,
            ItemID.DyeTradersScimitar,
            ItemID.BluePhaseblade,
            ItemID.GreenPhaseblade,
            ItemID.YellowPhaseblade,
            ItemID.PurplePhaseblade,
            ItemID.WhitePhaseblade,
            ItemID.RedPhaseblade,
            ItemID.Starfury,
            ItemID.EnchantedSword,
            ItemID.BeeKeeper,
            ItemID.FalconBlade,
            ItemID.Rally,
            ItemID.CorruptYoyo,
            ItemID.CrimsonYoyo,
            ItemID.JungleYoyo,
            ItemID.Code1,
            ItemID.Valor,
            ItemID.Cascade,
            ItemID.Spear,
            ItemID.Trident,
            ItemID.TheRottedFork,
            ItemID.Swordfish,
            ItemID.DarkLance,
            ItemID.WoodenBoomerang,
            ItemID.EnchantedBoomerang,
            ItemID.BloodyMachete,
            ItemID.IceBoomerang,
            ItemID.ThornChakram,
            ItemID.Flamarang,
            ItemID.ChainKnife,
            ItemID.TheMeatball,
            ItemID.BlueMoon,
            ItemID.Sunfury,
            ItemID.HellwingBow,
            ItemID.Handgun,
            ItemID.Boomstick,
            ItemID.Revolver,
            ItemID.Sandgun,
            ItemID.TheUndertaker,
            ItemID.Musket,
            ItemID.FlintlockPistol,
            ItemID.Minishark,
            ItemID.RedRyder,
            ItemID.Shuriken,
            ItemID.ThrowingKnife,
            ItemID.Snowball,
            ItemID.SpikyBall,
            ItemID.Bone,
            ItemID.RottenEgg,
            ItemID.Javelin,
            ItemID.BoneJavelin,
            ItemID.FlareGun,
            ItemID.Blowpipe,
            ItemID.Blowgun,
            ItemID.SnowballCannon,
            ItemID.Harpoon,
            ItemID.WandofSparking,
            ItemID.Vilethorn,
            ItemID.MagicMissile,
            ItemID.AquaScepter,
            ItemID.FlowerofFire,
            ItemID.Flamelash,
            ItemID.WaterBolt,
            ItemID.BookofSkulls,
            ItemID.DemonScythe,
            ItemID.CrimsonRod,
            ItemID.SlimeStaff,
            ItemID.HornetStaff,
            ItemID.ImpStaff,
            ItemID.SnowballLauncher,
            //explosives
            ItemID.Bomb,
            ItemID.Dynamite,
            ItemID.BombFish,
            ItemID.Grenade,
            ItemID.Beenade,
            //Crafting
            ItemID.AntlionMandible,
            ItemID.BeeWax,
            ItemID.BlackInk,
            ItemID.BlackLens,
            ItemID.CyanHusk,
            ItemID.EmptyDropper,
            ItemID.Feather,
            ItemID.Hook,
            ItemID.GiantHarpyFeather,
            ItemID.Lens,
            ItemID.PinkGel,
            ItemID.PurpleMucos,
            ItemID.RedHusk,
            ItemID.RottenChunk,
            ItemID.SharkFin,
            ItemID.Stinger,
            ItemID.TatteredCloth,
            ItemID.Vertebrae,
            ItemID.VioletHusk,
            ItemID.WormTooth
        };

        public static int[] PrePlanteraItems =
      {
            ItemID.NeptunesShell,
            ItemID.LeafWings,
            ItemID.Jetpack,
            ItemID.MothronWings,
            ItemID.FestiveWings,
            ItemID.PhilosophersStone,
            ItemID.AdhesiveBandage,
            ItemID.ArmorPolish,
            ItemID.Blindfold,
            ItemID.MoonCharm,
            ItemID.CrossNecklace,
            ItemID.FastClock,
            ItemID.FleshKnuckles,
            ItemID.FrozenTurtleShell,
            ItemID.MagicQuiver,
            ItemID.Megaphone,
            ItemID.MoonStone,
            ItemID.PocketMirror,
            ItemID.PutridScent,
            ItemID.StarCloak,
            ItemID.TitanGlove,
            ItemID.TrifoldMap,
            ItemID.Vitamins,
            ItemID.ApprenticeScarf,
            ItemID.SquireShield,
            ItemID.HuntressBuckler,
            ItemID.MonkBelt,
            ItemID.HerculesBeetle,
            ItemID.NecromanticScroll,
            ItemID.YoYoGlove,
            ItemID.DiscountCard,
            ItemID.LuckyCoin,
            //Weapons
            ItemID.SlapHand,
            ItemID.IceSickle,
            ItemID.BreakerBlade,
            ItemID.Cutlass,
            ItemID.Frostbrand,
            ItemID.FetidBaghnakhs,
            ItemID.Excalibur,
            ItemID.DeathSickle,
            ItemID.PsychoKnife,
            ItemID.Keybrand,
            ItemID.TheHorsemansBlade,
            ItemID.FormatC,
            ItemID.Gradient,
            ItemID.Chik,
            ItemID.HelFire,
            ItemID.Amarok,
            ItemID.Code2,
            ItemID.Yelets,
            ItemID.ValkyrieYoyo,
            ItemID.Gungnir,
            ItemID.NorthPole,
            ItemID.FlyingKnife,
            ItemID.LightDisc,
            ItemID.Bananarang,
            ItemID.PossessedHatchet,
            ItemID.PaladinsHammer,
            ItemID.Anchor,
            ItemID.KOCannon,
            ItemID.ChainGuillotines,
            ItemID.DaoofPow,
            ItemID.FlowerPow,
            ItemID.Arkhalis,
            ItemID.ShadowFlameKnife,
            ItemID.DaedalusStormbow,
            ItemID.ShadowFlameBow,
            ItemID.PulseBow,
            ItemID.ClockworkAssaultRifle,
            ItemID.Gatligator,
            ItemID.Shotgun,
            ItemID.CoinGun,
            ItemID.Uzi,
            ItemID.VenusMagnum,
            ItemID.SniperRifle,
            ItemID.CandyCornRifle,
            ItemID.ChainGun,
            ItemID.GrenadeLauncher,
            ItemID.ProximityMineLauncher,
            ItemID.RocketLauncher,
            ItemID.NailGun,
            ItemID.Stynger,
            ItemID.JackOLanternLauncher,
            ItemID.SnowmanCannon,
            ItemID.ElectrosphereLauncher,
            ItemID.Toxikarp,
            ItemID.DartPistol,
            ItemID.DartRifle,
            ItemID.Flamethrower,
            ItemID.SkyFracture,
            ItemID.FlowerofFrost,
            ItemID.CrystalVileShard,
            ItemID.PoisonStaff,
            ItemID.RainbowRod,
            ItemID.UnholyTrident,
            ItemID.VenomStaff,
            ItemID.NettleBurst,
            ItemID.BatScepter,
            ItemID.BlizzardStaff,
            ItemID.InfernoFork,
            ItemID.ShadowbeamStaff,
            ItemID.WaspGun,
            ItemID.LeafBlower,
            ItemID.RainbowGun,
            ItemID.HeatRay,
            ItemID.LaserMachinegun,
            ItemID.BubbleGun,
            ItemID.ChargedBlasterCannon,
            ItemID.CursedFlames,
            ItemID.GoldenShower,
            ItemID.MagnetSphere,
            ItemID.IceRod,
            ItemID.ClingerStaff,
            ItemID.NimbusRod,
            ItemID.MagicDagger,
            ItemID.MedusaHead,
            ItemID.SpiritFlame,
            ItemID.ShadowFlameHexDoll,
            ItemID.MagicalHarp,
            ItemID.ToxicFlask,
            ItemID.SpiderStaff,
            ItemID.PirateStaff,
            ItemID.OpticStaff,
            ItemID.DeadlySphereStaff,
            ItemID.RavenStaff,
            ItemID.TempestStaff,
            ItemID.HolyWater,
            ItemID.UnholyWater,
            ItemID.BloodWater,
            ItemID.Cannon,
            ItemID.BunnyCannon,
            ItemID.LandMine,
            //Crafting
            ItemID.AncientCloth,
            ItemID.Bell,
            ItemID.BlackFairyDust,
            ItemID.BrokenBatWing,
            ItemID.ButterflyDust,
            ItemID.CursedFlame,
            ItemID.DarkShard,
            ItemID.ExplosivePowder,
            ItemID.FireFeather,
            ItemID.FrostCore,
            ItemID.GoldDust,
            ItemID.Harp,
            ItemID.IceFeather,
            ItemID.Ichor,
            ItemID.IllegalGunParts,
            ItemID.LightShard,
            ItemID.Nanites,
            ItemID.PixieDust,
            ItemID.SpellTome,
            ItemID.SpiderFang,
            ItemID.TatteredBeeWing,
            ItemID.TurtleShell,
            ItemID.UnicornHorn,
        };

        public static int[] PostPlanteraItems =
        {
            ItemID.Tabi,
            ItemID.SteampunkWings,
            ItemID.BetsyWings,
            ItemID.FishronWings,
            ItemID.BlackBelt,
            ItemID.PaladinsShield,
            ItemID.RifleScope,
            //Weapons
            ItemID.InfluxWaver,
            ItemID.StarWrath,
            ItemID.Kraken,
            ItemID.GolemFist,
            ItemID.Flairon,
            ItemID.Xenopopper,
            ItemID.NebulaArcanum,
            ItemID.NebulaBlaze,
            ItemID.PiranhaGun,
            ItemID.XenoStaff,
            //Crafting
            ItemID.BeetleHusk,
            ItemID.BoneFeather,
            ItemID.Ectoplasm,
            ItemID.SpookyTwig,
            ItemID.VialofVenom,
        };

    }
}
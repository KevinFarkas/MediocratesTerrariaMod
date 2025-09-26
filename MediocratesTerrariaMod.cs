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
            //for each chest
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                    continue;

                Random rand = new Random(Main.worldID);
                int amountOfItemsToAdd = rand.Next(2, 9);

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
                                chest.item[i].stack = rand.Next(2, 69); // set amount if stackable
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
            ItemID.WhiteString,
            ItemID.MagicCuffs,
            ItemID.ManaFlower,
            ItemID.MagnetFlower,
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
            ItemID.TerrasparkBoots,
            ItemID.WormScarf,
            ItemID.BundleofBalloons,
            ItemID.CloudinaBalloon,
            ItemID.ObsidianShield,
            ItemID.StingerNecklace,
            ItemID.PiggyBank,
            ItemID.Diamond,
            ItemID.Ruby,
            ItemID.Emerald,
            ItemID.Topaz,
            ItemID.Sapphire,
            ItemID.Amber,
            ItemID.Amethyst,
            //Weapons
            ItemID.QuadBarrelShotgun,
            ItemID.PhoenixBlaster,
            ItemID.HiveFive,
            ItemID.NightsEdge,
            ItemID.BladeofGrass,
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
            ItemID.Trimarang,
            ItemID.ChainKnife,
            ItemID.TheMeatball,
            ItemID.BlueMoon,
            ItemID.Sunfury,
            ItemID.HellwingBow,
            ItemID.BloodRainBow,
            ItemID.DemonBow,
            ItemID.TendonBow,     
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
            ItemID.ThornWhip,
            ItemID.ThornChakram,
            ItemID.SpikyBall,
            ItemID.Bone,
            ItemID.RottenEgg,
            ItemID.TheRottedFork,
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
            ItemID.FrostburnArrow,
            ItemID.UnholyArrow,
            ItemID.JestersArrow,
            ItemID.ShimmerArrow,
            ItemID.TungstenBullet,
            ItemID.SilverBullet,
            ItemID.VampireFrogStaff,
            ItemID.MoltenFury,
            ItemID.BeesKnees,
            ItemID.DiamondStaff,
            ItemID.MeteorShot,
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
            ItemID.WormTooth,
            ItemID.JungleSpores,
            ItemID.Vine,
            //Tools
            ItemID.DeathbringerPickaxe,
            ItemID.NightmarePickaxe,
            ItemID.ReaverShark,
            ItemID.PlatinumPickaxe,
            ItemID.BonePickaxe,
            ItemID.FossilPickaxe,
            ItemID.CnadyCanePickaxe,
            ItemID.GoldPickaxe,
            //Potions
            ItemID.IronskinPotion,
            ItemID.SwiftnessPotion,
            ItemID.RegenerationPotion,
            ItemID.EndurancePotion,
            ItemID.SummoningPotion,
            ItemID.BewitchingTable,
            ItemID.RagePotion,
            ItemID.WrathPotion,
            ItemID.HeartreachPotion,
            ItemID.SliceOfCake,
            ItemID.Sake,
            ItemID.FlaskofFire,
            ItemID.FlaskofPoison,
            ItemID.AmmoBox,
            ItemID.AmmoReservationPotion,
            ItemID.ArcheryPotion,
            ItemID.SharpeningStation,
            ItemID.CrystalBall,
            //Armor
            ItemID.CrimsonHelmet,
            ItemID.CrimsonScalemail,
            ItemID.CrimsonGreaves,
            ItemID.WizardHat,
            ItemID.DiamondRobe,
            ItemID.MeteorHelmet,
            ItemID.MeteorSuit,
            ItemID.MeteorLeggings,
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
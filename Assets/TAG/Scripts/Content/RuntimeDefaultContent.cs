using System.Collections.Generic;
using TAG.Characters;
using TAG.Core;
using TAG.Maps;
using TAG.Progression;
using UnityEngine;

namespace TAG.Content
{
    public static class RuntimeDefaultContent
    {
        public static GameDataCatalog Catalog()
        {
            var catalog = ScriptableObject.CreateInstance<GameDataCatalog>();
            catalog.creatures = new List<CreatureDefinition>
            {
                Creature("forest_hopper", "Forest Hopper", RarityTier.Common, "Bright spring mascot with huge leaf ears and a moon-hop run."),
                Creature("tiny_leafling", "Tiny Leafling", RarityTier.Common, "Shy seedling creature with tiny cheers and oversized courage."),
                Creature("moss_fox", "Moss Fox", RarityTier.Rare, "Clever trickster with mossy tail swishes and playful taunts."),
                Creature("stone_mole", "Stone Mole", RarityTier.Rare, "Chunky tunnel buddy with crystal goggles and victory belly flops."),
                Creature("glow_bat", "Glow Bat", RarityTier.Epic, "Neon cave flyer with expressive ears and light-trail emotes."),
                Creature("jungle_panda", "Jungle Panda", RarityTier.Epic, "Round vine-swinging hero with snack-themed emotes."),
                Creature("crystal_beaver", "Crystal Beaver", RarityTier.Legendary, "Builder mascot with gemstone teeth and hammer-tail victory pose."),
                Creature("golden_monkey", "Golden Monkey", RarityTier.Mythic, "Flashy rooftop acrobat with crown markings and showboat flips.")
            };
            catalog.maps = new List<MapDefinition>
            {
                Map("rooftop", "Rooftop", 1),
                Map("jungle", "Jungle", 10),
                Map("mine", "Mine", 20),
                Map("night_rooftop", "Night Rooftop", 30),
                Map("temple_jungle", "Temple Jungle", 40),
                Map("crystal_mine", "Crystal Mine", 50)
            };
            return catalog;
        }

        public static ProceduralMaterialPalette Palette()
        {
            var palette = ScriptableObject.CreateInstance<ProceduralMaterialPalette>();
            palette.rooftopConcrete = Material("Runtime Rooftop Concrete", new Color(0.38f, 0.42f, 0.48f));
            palette.rooftopGarden = Material("Runtime Rooftop Garden", new Color(0.18f, 0.58f, 0.32f));
            palette.neon = Material("Runtime Neon", new Color(0.15f, 0.82f, 1f));
            palette.jungleGround = Material("Runtime Jungle Ground", new Color(0.13f, 0.28f, 0.16f));
            palette.jungleLeaf = Material("Runtime Jungle Leaf", new Color(0.22f, 0.82f, 0.34f));
            palette.templeStone = Material("Runtime Temple Stone", new Color(0.47f, 0.46f, 0.39f));
            palette.mineRock = Material("Runtime Mine Rock", new Color(0.17f, 0.15f, 0.19f));
            palette.crystal = Material("Runtime Crystal", new Color(0.45f, 0.9f, 1f));
            palette.lava = Material("Runtime Lava", new Color(1f, 0.28f, 0.05f));
            palette.creaturePrimary = Material("Runtime Creature Primary", new Color(0.96f, 0.72f, 0.26f));
            palette.creatureAccent = Material("Runtime Creature Accent", new Color(0.38f, 1f, 0.72f));
            palette.enemy = Material("Runtime Friendly Rival", new Color(1f, 0.28f, 0.34f));
            return palette;
        }

        private static CreatureDefinition Creature(string id, string name, RarityTier rarity, string personality)
        {
            var creature = ScriptableObject.CreateInstance<CreatureDefinition>();
            creature.creatureId = id;
            creature.displayName = name;
            creature.rarity = rarity;
            creature.personality = personality;
            creature.unlockableSkinIds = new List<string> { id + "_sunset", id + "_jungle", id + "_crystal" };
            creature.emoteIds = new List<string> { "wave", "laugh", "taunt", "victory" };
            return creature;
        }

        private static MapDefinition Map(string id, string name, int level)
        {
            var map = ScriptableObject.CreateInstance<MapDefinition>();
            map.mapId = id;
            map.displayName = name;
            map.unlockLevel = level;
            return map;
        }

        private static Material Material(string name, Color color)
        {
            var material = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
            material.name = name;
            material.color = color;
            return material;
        }
    }
}

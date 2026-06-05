#if UNITY_EDITOR
using System.Collections.Generic;
using TAG.AI;
using TAG.Characters;
using TAG.Content;
using TAG.Core;
using TAG.Maps;
using TAG.Progression;
using TAG.Runtime;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TAG.Editor
{
    public static class TAGContentGenerator
    {
        private const string GeneratedRoot = "Assets/TAG/Generated";
        private const string ResourceRoot = GeneratedRoot + "/Resources/TAG";

        [InitializeOnLoadMethod]
        private static void AutoGenerateOnEditorLoad()
        {
            EditorApplication.delayCall += () =>
            {
                if (AssetDatabase.LoadAssetAtPath<GameDataCatalog>(ResourceRoot + "/GameDataCatalog.asset") == null)
                {
                    EnsureAllContent();
                }
            };
        }

        [MenuItem("TAG/Generate Complete Game Content")]
        public static void EnsureAllContent()
        {
            EnsureFolders();
            var palette = CreatePalette();
            var catalog = CreateCatalog();
            CreateAiProfiles();
            CreateRuntimeScenes(catalog, palette);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void EnsureFolders()
        {
            CreateFolder("Assets/TAG", "Generated");
            CreateFolder(GeneratedRoot, "Resources");
            CreateFolder(GeneratedRoot + "/Resources", "TAG");
            CreateFolder(GeneratedRoot, "Creatures");
            CreateFolder(GeneratedRoot, "Maps");
            CreateFolder(GeneratedRoot, "Difficulties");
            CreateFolder(GeneratedRoot, "AI");
            CreateFolder(GeneratedRoot, "Materials");
        }

        private static void CreateFolder(string parent, string child)
        {
            if (!AssetDatabase.IsValidFolder(parent + "/" + child)) AssetDatabase.CreateFolder(parent, child);
        }

        private static Material CreateMaterial(string name, Color color, float metallic = 0f, float smoothness = 0.55f, bool emission = false)
        {
            var path = $"{GeneratedRoot}/Materials/{name}.mat";
            var mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat == null)
            {
                mat = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
                AssetDatabase.CreateAsset(mat, path);
            }
            mat.color = color;
            mat.SetFloat("_Metallic", metallic);
            mat.SetFloat("_Smoothness", smoothness);
            if (emission)
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", color * 1.8f);
            }
            return mat;
        }

        private static ProceduralMaterialPalette CreatePalette()
        {
            var path = ResourceRoot + "/ProceduralPalette.asset";
            var palette = AssetDatabase.LoadAssetAtPath<ProceduralMaterialPalette>(path) ?? ScriptableObject.CreateInstance<ProceduralMaterialPalette>();
            palette.rooftopConcrete = CreateMaterial("RooftopConcrete", new Color(0.38f, 0.42f, 0.48f));
            palette.rooftopGarden = CreateMaterial("RooftopGarden", new Color(0.18f, 0.58f, 0.32f));
            palette.neon = CreateMaterial("TAGNeon", new Color(0.15f, 0.82f, 1f), 0f, 0.85f, true);
            palette.jungleGround = CreateMaterial("JungleGround", new Color(0.13f, 0.28f, 0.16f));
            palette.jungleLeaf = CreateMaterial("JungleLeaf", new Color(0.22f, 0.82f, 0.34f));
            palette.templeStone = CreateMaterial("TempleStone", new Color(0.47f, 0.46f, 0.39f));
            palette.mineRock = CreateMaterial("MineRock", new Color(0.17f, 0.15f, 0.19f));
            palette.crystal = CreateMaterial("CrystalGlow", new Color(0.45f, 0.9f, 1f), 0f, 0.95f, true);
            palette.lava = CreateMaterial("Lava", new Color(1f, 0.28f, 0.05f), 0f, 0.8f, true);
            palette.creaturePrimary = CreateMaterial("CreaturePrimary", new Color(0.96f, 0.72f, 0.26f));
            palette.creatureAccent = CreateMaterial("CreatureAccent", new Color(0.38f, 1f, 0.72f));
            palette.enemy = CreateMaterial("FriendlyRival", new Color(1f, 0.28f, 0.34f));
            if (AssetDatabase.GetAssetPath(palette) == string.Empty) AssetDatabase.CreateAsset(palette, path);
            EditorUtility.SetDirty(palette);
            return palette;
        }

        private static GameDataCatalog CreateCatalog()
        {
            var catalog = AssetDatabase.LoadAssetAtPath<GameDataCatalog>(ResourceRoot + "/GameDataCatalog.asset") ?? ScriptableObject.CreateInstance<GameDataCatalog>();
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
                Map("rooftop", "Rooftop", 1, "Golden-hour rooftop chase with gardens, water towers, AC units, billboards, bridges, neon, construction, fire escapes, helipad, skyline, wind, birds, and traffic."),
                Map("jungle", "Jungle", 10, "Living jungle with dense vegetation, large trees, vines, waterfalls, ruins, shortcuts, wildlife, fog, sun shafts, insects, leaves, and weather."),
                Map("mine", "Mine", 20, "Underground mine with carts, rails, crystal caverns, supports, openings, lava, elevators, dynamite, tunnels, dust, echoes, lanterns, and crystal light."),
                Map("night_rooftop", "Night Rooftop", 30, "Night variant with neon signage, rooftop silhouettes, moonlit bridges, skyline depth, and wind-streak VFX."),
                Map("temple_jungle", "Temple Jungle", 40, "Temple jungle variant with expanded ancient structures, hidden shrine routes, heavier fog, and god-ray lighting."),
                Map("crystal_mine", "Crystal Mine", 50, "Crystal mine finale with dense gemstone clusters, colored lighting, brutal chokepoints, lava risk, and echoing pressure.")
            };
            catalog.difficulties = new List<DifficultyDefinition>
            {
                Difficulty(DifficultyTier.Easy, "Easy", 15, 0.75f, 0.8f, 0.7f, false, false),
                Difficulty(DifficultyTier.Normal, "Normal", 15, 1f, 1f, 1f, false, false),
                Difficulty(DifficultyTier.Hard, "Hard", 15, 1.15f, 1.15f, 1.25f, false, false),
                Difficulty(DifficultyTier.Insane, "Insane", 15, 1.35f, 1.3f, 1.55f, false, false),
                Difficulty(DifficultyTier.Nightmare, "Nightmare", 15, 1.6f, 1.45f, 1.85f, false, false),
                Difficulty(DifficultyTier.GodMode, "God Mode", 50, 2.1f, 1.8f, 2.6f, true, true)
            };
            if (AssetDatabase.GetAssetPath(catalog) == string.Empty) AssetDatabase.CreateAsset(catalog, ResourceRoot + "/GameDataCatalog.asset");
            EditorUtility.SetDirty(catalog);
            return catalog;
        }

        private static CreatureDefinition Creature(string id, string name, RarityTier rarity, string personality)
        {
            var path = $"{GeneratedRoot}/Creatures/{id}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<CreatureDefinition>(path) ?? ScriptableObject.CreateInstance<CreatureDefinition>();
            asset.creatureId = id;
            asset.displayName = name;
            asset.rarity = rarity;
            asset.personality = personality;
            asset.unlockableSkinIds = new List<string> { id + "_sunset", id + "_jungle", id + "_crystal" };
            asset.emoteIds = new List<string> { "wave", "laugh", "taunt", "victory" };
            if (AssetDatabase.GetAssetPath(asset) == string.Empty) AssetDatabase.CreateAsset(asset, path);
            EditorUtility.SetDirty(asset);
            return asset;
        }

        private static MapDefinition Map(string id, string name, int level, string direction)
        {
            var path = $"{GeneratedRoot}/Maps/{id}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<MapDefinition>(path) ?? ScriptableObject.CreateInstance<MapDefinition>();
            asset.mapId = id;
            asset.displayName = name;
            asset.unlockLevel = level;
            asset.artDirection = direction;
            asset.signatureProps = new List<string>(direction.Split(','));
            if (AssetDatabase.GetAssetPath(asset) == string.Empty) AssetDatabase.CreateAsset(asset, path);
            EditorUtility.SetDirty(asset);
            return asset;
        }

        private static DifficultyDefinition Difficulty(DifficultyTier tier, string name, int level, float speed, float vision, float aggression, bool allMaps, bool allStars)
        {
            var path = $"{GeneratedRoot}/Difficulties/{tier}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<DifficultyDefinition>(path) ?? ScriptableObject.CreateInstance<DifficultyDefinition>();
            asset.tier = tier;
            asset.displayName = name;
            asset.unlockLevel = level;
            asset.enemySpeedMultiplier = speed;
            asset.enemyVisionMultiplier = vision;
            asset.directorAggression = aggression;
            asset.requiresAllMapsCompleted = allMaps;
            asset.requiresThreeStarsEverywhere = allStars;
            if (AssetDatabase.GetAssetPath(asset) == string.Empty) AssetDatabase.CreateAsset(asset, path);
            EditorUtility.SetDirty(asset);
            return asset;
        }

        private static void CreateAiProfiles()
        {
            Profile("Easy", 3.5f, 5.5f, 16f, 0.55f, 0.2f, false);
            Profile("Normal", 4.5f, 7.5f, 22f, 0.35f, 0.45f, false);
            Profile("GodMode", 7f, 13f, 34f, 0.08f, 1f, true);
        }

        private static void Profile(string name, float patrol, float chase, float vision, float reaction, float flank, bool prediction)
        {
            var path = $"{GeneratedRoot}/AI/{name}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<AIDifficultyProfile>(path) ?? ScriptableObject.CreateInstance<AIDifficultyProfile>();
            asset.patrolSpeed = patrol;
            asset.chaseSpeed = chase;
            asset.visionDistance = vision;
            asset.reactionSeconds = reaction;
            asset.flankWeight = flank;
            asset.ambushWeight = flank * 0.5f;
            asset.enableGodModePrediction = prediction;
            if (AssetDatabase.GetAssetPath(asset) == string.Empty) AssetDatabase.CreateAsset(asset, path);
            EditorUtility.SetDirty(asset);
        }

        private static void CreateRuntimeScenes(GameDataCatalog catalog, ProceduralMaterialPalette palette)
        {
            BootstrapScene(catalog);
            MenuScene(catalog);
            GameScene(catalog, palette);
            InfoScene("HowToPlay", "HOW TO PLAY", "Run as a collectible creature. Sprint across rooftops, jungle ruins, and crystal mines. Jump gaps, dash through danger, use grapple routes, avoid the tag squad, complete objectives, earn XP, and unlock maps, difficulties, cosmetics, titles, badges, and battle pass tiers.");
            InfoScene("Settings", "SETTINGS", "Accessibility, controller support, haptics, 60/120 FPS, audio mix, camera shake, motion effects, and performance presets are represented in the native Unity systems and ready for final device tuning.");
            InfoScene("Profile", "PROFILE", "Your account level, creature collection, titles, badges, achievements, login rewards, daily challenges, weekly challenges, battle pass progress, and cosmetic unlocks are saved through the TAG save system.");
            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene("Assets/TAG/Scenes/Bootstrap.unity", true),
                new EditorBuildSettingsScene("Assets/TAG/Scenes/MainMenu.unity", true),
                new EditorBuildSettingsScene("Assets/TAG/Scenes/HowToPlay.unity", true),
                new EditorBuildSettingsScene("Assets/TAG/Scenes/Settings.unity", true),
                new EditorBuildSettingsScene("Assets/TAG/Scenes/Profile.unity", true),
                new EditorBuildSettingsScene("Assets/TAG/Scenes/Game.unity", true)
            };
        }

        private static void BootstrapScene(GameDataCatalog catalog)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var go = new GameObject("TAG Bootstrap");
            var bootstrap = go.AddComponent<GameBootstrap>();
            SetSerialized(bootstrap, "catalog", catalog);
            go.AddComponent<TAG.Mobile.MobilePlatformConfigurator>();
            go.AddComponent<BootstrapSceneLoader>();
            EditorSceneManager.SaveScene(scene, "Assets/TAG/Scenes/Bootstrap.unity");
        }

        private static void MenuScene(GameDataCatalog catalog)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var go = new GameObject("TAG Runtime Main Menu");
            var menu = go.AddComponent<RuntimeMenuScene>();
            SetSerialized(menu, "catalog", catalog);
            AddCameraAndLight();
            EditorSceneManager.SaveScene(scene, "Assets/TAG/Scenes/MainMenu.unity");
        }

        private static void GameScene(GameDataCatalog catalog, ProceduralMaterialPalette palette)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var go = new GameObject("TAG Runtime Game Scene");
            var game = go.AddComponent<RuntimeGameScene>();
            SetSerialized(game, "catalog", catalog);
            SetSerialized(game, "palette", palette);
            AddCameraAndLight();
            EditorSceneManager.SaveScene(scene, "Assets/TAG/Scenes/Game.unity");
        }

        private static void InfoScene(string sceneName, string title, string body)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var go = new GameObject(sceneName + " Controller");
            var info = go.AddComponent<DedicatedInfoScene>();
            SetSerialized(info, "title", title);
            SetSerialized(info, "body", body);
            AddCameraAndLight();
            EditorSceneManager.SaveScene(scene, $"Assets/TAG/Scenes/{sceneName}.unity");
        }

        private static void AddCameraAndLight()
        {
            var camera = new GameObject("Main Camera");
            camera.tag = "MainCamera";
            camera.AddComponent<Camera>();
            camera.transform.position = new Vector3(0f, 8f, -10f);
            camera.transform.rotation = Quaternion.Euler(45f, 0f, 0f);
            var light = new GameObject("Directional Light").AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.2f;
            light.shadows = LightShadows.Soft;
            light.transform.rotation = Quaternion.Euler(45f, -25f, 0f);
        }

        private static void SetSerialized(Object target, string propertyName, Object value)
        {
            var so = new SerializedObject(target);
            so.FindProperty(propertyName).objectReferenceValue = value;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetSerialized(Object target, string propertyName, string value)
        {
            var so = new SerializedObject(target);
            so.FindProperty(propertyName).stringValue = value;
            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
#endif

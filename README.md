# TAG — Native Android Unity Game

TAG is now implemented as a **native Unity Android game project**. The browser prototype is only a reference file; the playable game, menus, systems, generated assets, Android build path, and runtime content are Unity/C#.

## No manual Unity content work required

You do **not** need to do the previous setup list manually. The project now creates it for you:

- ScriptableObject `.asset` instances are generated automatically.
- Creature prefabs are generated automatically and assigned to creature definitions.
- Rooftop, Jungle, Mine, Night Rooftop, Temple Jungle, and Crystal Mine map prefabs are generated automatically.
- NavMesh data assets are baked by the editor generator for every generated map.
- AI difficulty profiles are generated for Easy, Normal, and God Mode baselines.
- Commercial rendering/post-effect tuning assets are generated automatically.
- Cosmetic reward, battle pass, analytics, accessibility, economy, cloud-save adapter, haptic, and mobile performance services are added to the bootstrap scene automatically.
- Dedicated Bootstrap, MainMenu, Game, HowToPlay, Settings, and Profile scenes are generated automatically.

## One-command generation/build options

Inside Unity, use either menu item:

- `TAG > Generate Complete Game Content`
- `TAG > Build > Android APK`

The Android build command calls the generator first, then configures package ID `com.tagstudio.tag`, min SDK 23, automatic target SDK resolution, IL2CPP, and ARMv7/ARM64 output.

For command-line CI/build machines with Unity installed, run:

```bash
Unity -batchmode -quit -projectPath . -executeMethod TAG.Editor.TAGContentGenerator.EnsureAllContent
Unity -batchmode -quit -projectPath . -executeMethod TAG.Editor.AndroidBuild.BuildAndroidApk
```

## What the generator creates

- **Playable menu:** Play, locked/unlocked map selection, character collection, battle pass rewards, and profile/settings/how-to-play navigation.
- **Playable game:** generated map, generated player creature, generated rival taggers, camera follow, HUD, survival timer, keyboard/touch movement, sprint, jump, and dash.
- **Creatures:** Forest Hopper, Tiny Leafling, Moss Fox, Stone Mole, Glow Bat, Jungle Panda, Crystal Beaver, and Golden Monkey with rarity, personality, skins, emotes, and generated prefabs.
- **Maps:** Rooftop city chase, living jungle, underground mine, and three level-gated variants with props, lighting, themed materials, atmosphere hooks, and generated prefabs/NavMesh assets.
- **Meta:** XP/account level, achievements, daily and weekly challenges, login rewards, battle pass, cosmetics, titles, badges, collection, and profile customization data.
- **Mobile polish:** 60/120 FPS targeting, haptics, accessibility settings, analytics events, cloud-save adapter, ethical cosmetic economy, dynamic music scaffolding, footstep surfaces, camera shake, motion/depth-effect flags, and soft-shadow lighting.

## Main implementation files

```text
Assets/TAG/Editor/TAGContentGenerator.cs        # Generates assets, prefabs, maps, NavMesh data, scenes, rewards, rendering profile
Assets/TAG/Editor/AndroidBuild.cs               # Regenerates content and builds Android APK
Assets/TAG/Scripts/Runtime/RuntimeMenuScene.cs  # Native Unity menu, collection, map select, battle pass
Assets/TAG/Scripts/Runtime/RuntimeGameScene.cs  # Playable 3D TAG scene builder and input/HUD
Assets/TAG/Scripts/Content/ProceduralMapBuilder.cs
Assets/TAG/Scripts/Content/ProceduralCreatureFactory.cs
Assets/TAG/Scripts/Services/CloudSaveService.cs
Assets/TAG/Scripts/Services/AnalyticsService.cs
Assets/TAG/Scripts/Services/EconomyService.cs
Assets/TAG/Scripts/Services/AccessibilitySettingsService.cs
```

## Controls

- Keyboard: WASD/arrow keys move, Shift sprint, Space jump, E or Left Ctrl dash.
- Touch: first touch acts as a simple directional control for Android/mobile smoke testing.

## Conflict-resolution status

The conflicted project files keep the native Unity implementation and the automated content-generation path:

- `Assets/TAG/Editor/AndroidBuild.cs` keeps the generator-first Android build and now ensures the APK output folder exists before building.
- `Assets/TAG/Scripts/Core/GameBootstrap.cs` keeps generated catalog fallback, save loading, cloud-save hydration/upload, and analytics startup hooks.
- `Packages/manifest.json` keeps the Unity packages needed for URP, Input System, Cinemachine, Addressables, Analytics, Mobile Notifications, TextMeshPro, Timeline, and AI Navigation.
- `README.md` keeps the no-manual-Unity-work instructions and documents the one-command generation/build flow.

Verification for this conflict pass checked that the repository has no unmerged paths and that these files contain no Git conflict markers.

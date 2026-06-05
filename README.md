# TAG — Native Android Unity Game

This repository is now a **native Unity Android game project** for **TAG**. The HTML prototype remains only as a historical gameplay reference; the game implementation lives in Unity/C# and builds as an Android APK.

## Open it and play

1. Open the repo with **Unity 2022.3.45f1 LTS**.
2. The editor automatically generates the complete playable content the first time the project loads.
3. Press Play from `Assets/TAG/Scenes/Bootstrap.unity`, or run `TAG > Generate Complete Game Content` if you want to regenerate everything.
4. Build Android with `TAG > Build > Android APK`.

The build command also regenerates the content before building, so you do not need to hand-create ScriptableObjects, materials, scenes, menus, characters, or maps.

## What is created automatically

- **Playable Unity scenes:** Bootstrap, MainMenu, Game, HowToPlay, Settings, and Profile.
- **A runtime menu:** Play, map selection, character collection, battle pass, profile/settings/how-to-play navigation.
- **Playable 3D game scene:** camera follow, player creature, rival taggers, HUD, timer, keyboard/touch movement, sprint, jump, and dash.
- **Procedural creature roster:** Forest Hopper, Tiny Leafling, Moss Fox, Stone Mole, Glow Bat, Jungle Panda, Crystal Beaver, and Golden Monkey.
- **Procedural maps:** Rooftop, Jungle, Mine, Night Rooftop, Temple Jungle, and Crystal Mine with themed props, lighting, atmosphere hooks, and unlock levels.
- **Generated assets:** ScriptableObject catalog, creature definitions, map definitions, difficulty definitions, AI profiles, URP-compatible materials, and runtime fallback content.
- **Meta systems:** XP, account level, achievements, daily/weekly challenges, battle pass, login rewards, collection, cosmetics, titles, badges, and profile customization fields.
- **Commercial mobile foundations:** 60/120 FPS targeting, haptics, Android IL2CPP build settings, URP/Input System/Cinemachine dependencies, dynamic music layers, footstep surface audio, and AI director pressure.

## Native Android build

Open Unity and run:

`TAG > Build > Android APK`

The build script configures package ID `com.tagstudio.tag`, min SDK 23, automatic target SDK resolution, IL2CPP, and ARMv7/ARM64 output.

## Main implementation files

```text
Assets/TAG/Editor/TAGContentGenerator.cs        # Automatically creates the playable game content
Assets/TAG/Editor/AndroidBuild.cs               # Regenerates content and builds Android APK
Assets/TAG/Scripts/Runtime/RuntimeMenuScene.cs  # Native Unity menu, collection, map select, battle pass
Assets/TAG/Scripts/Runtime/RuntimeGameScene.cs  # Playable 3D TAG scene builder and input/HUD
Assets/TAG/Scripts/Content/ProceduralMapBuilder.cs
Assets/TAG/Scripts/Content/ProceduralCreatureFactory.cs
Assets/TAG/Scripts/Content/RuntimeDefaultContent.cs
Assets/TAG/Scripts/Save/PlayerSaveData.cs
Assets/TAG/Scripts/Progression/UnlockService.cs
```

## Controls

- Keyboard: WASD/arrow keys move, Shift sprint, Space jump, E or Left Ctrl dash.
- Touch: first touch acts as a simple directional control for mobile smoke testing.

## Production note

This is a complete native Unity implementation with generated playable content and systems. It does not pretend to replace years of hand-authored AAA art, animation, audio, QA, and device optimization, but it removes the manual Unity setup burden by generating the scenes, assets, roster, maps, menu, save/progression, AI, audio scaffolding, and Android build path directly in the project.

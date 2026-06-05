# TAG — Native Android Unity Conversion

This repository now contains a native Unity Android scaffold for **TAG**. The original HTML prototype remains as a gameplay reference only; the production direction is a 3D Unity mobile game with Android build support, C# systems, ScriptableObject-driven tuning, dedicated scenes, and mobile-first polish.

## What changed

- **No web app/PWA/canvas conversion.** Unity folders, Android project settings, C# gameplay systems, and an editor Android build command have been added.
- **Creature-first character direction.** Launch roster data covers Forest Hopper, Tiny Leafling, Moss Fox, Stone Mole, Glow Bat, Jungle Panda, Crystal Beaver, and Golden Monkey.
- **Progression and unlocks.** Map unlocks follow Level 1/10/20/30/40/50, difficulty unlocks at Level 15, and God Mode requires Level 50 plus completion/3-star mastery.
- **Menu layering fix.** `ScreenManager` uses an explicit navigation stack and moves active screens to the top sibling, preventing How To Play from appearing behind map selection UI.
- **Meta systems.** Save data includes XP, account level, achievements, daily/weekly challenges, battle pass, cosmetics, titles, badges, collection, and profile customization hooks.
- **AAA mobile systems foundation.** Character controller, AI director, dynamic music, footstep surfaces, map definitions, haptics, 60/120 FPS targets, IL2CPP Android build configuration, and URP/Cinemachine/Input System dependencies are in place.

## Unity version

Target Unity version: **2022.3.45f1 LTS**.

## Android build

Open the project in Unity and run:

`TAG > Build > Android APK`

The build script configures Android package ID `com.tagstudio.tag`, min SDK 23, automatic target SDK resolution, IL2CPP, and ARMv7/ARM64 output.

## Folder structure

```text
Assets/TAG/
  Art/                  # Character, map, UI, particles, material asset folders
  Audio/                # Music, SFX, voices, ambience folders
  Editor/               # Android build automation
  Scenes/               # Bootstrap, MainMenu, HowToPlay, Settings, Profile, Game
  ScriptableObjects/    # Data/tuning assets and launch roster JSON
  Scripts/
    AI/                 # Director and enemy squad behavior
    Audio/              # Dynamic music and surface footsteps
    Characters/         # Creature definitions and controller
    Core/               # Bootstrap, constants, catalog
    Maps/               # Map definitions
    Meta/               # Achievements, challenges, battle pass
    Mobile/             # FPS and haptics configuration
    Progression/        # Difficulty and unlock services
    Save/               # JSON save model/system
    UI/                 # Navigation and map selection views
```

## Production next steps

1. Open in Unity and let packages import.
2. Create actual `.asset` instances from the ScriptableObject definitions.
3. Replace placeholder scenes with authored 3D environments for Rooftop, Jungle, Mine, Night Rooftop, Temple Jungle, and Crystal Mine.
4. Build character prefabs using the creature definitions, animator controllers, emote clips, skin variants, and portrait art.
5. Bake NavMeshes for each map and tune `AIDifficultyProfile` assets per difficulty.
6. Add URP renderer assets, post-processing volumes, shadows, motion/depth effects, and map-specific VFX.
7. Wire Android cloud save, analytics events, ethical cosmetic economy, battle pass rewards, and accessibility settings.

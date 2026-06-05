# TAG Architecture Review And Refactor Notes

## Executive Summary

The original `TAG_v6.html` had a polished menu layer, but the gameplay simulation was fundamentally screen-space and arcade-scale. It could not support the requested Titanfall / Mirror's Edge / Apex / Dead by Daylight chase fantasy because the world, camera, movement, grapple, AI, and maps were all coupled to the current viewport.

The refactor in `TAG_AAA_refactor.html` replaces the play layer with a world-space canvas game prototype. It adds a large world, camera simulation, velocity-driven player controller, sprint, slide, double jump, wall run, dash, physical grapple, navigation-grid AI with A* pathfinding, flanking roles, map-specific spaces, particles, trails, lighting, and a cleaner competitive HUD.

## Major Problems In The Original Build

1. Screen-space map

The map was generated from `window.innerWidth`, `window.innerHeight`, HUD height, and ability-bar height. That made the viewport the actual game world. Reaching screen edges was unavoidable and immersion-breaking.

2. No real camera

Rendering used raw canvas coordinates. There was no world-to-screen transform, no follow target, no camera lag, no zoom model, and no shake. This prevented speed sensation and made the game feel like a fixed-screen mobile arena.

3. Direct-position movement

Runner movement applied input directly to `x` and `y`. There was no acceleration curve, no persistent velocity, no momentum, and no real friction model. Speeds also scaled with screen width, so game feel changed by device.

4. Ability design fought the movement fantasy

Blink and grapple were effectively random teleports/repositions. The old grapple did not shoot a hook, connect to geometry, pull physically, preserve velocity, or create swing-like motion.

5. AI was obstacle feeler steering only

Chasers moved mostly toward the player with local feelers. They had no real navigation representation, no route planning, no predictive intercept, no squad roles, and no credible flanking or search behavior.

6. Maps were obstacle arrangements, not spaces

Rooftop, jungle, and mine were sets of screen-relative rectangles. They did not create long routes, risk/reward crossings, alternate paths, vertical tools, hidden routes, or chase-readable chokepoints.

7. Rendering did not separate simulation from presentation

Gameplay bounds, map generation, HUD dimensions, collision, and rendering were tangled together. This made it hard to scale the world, cull objects, or add systems without regressions.

8. Performance model was limited

The original was small enough to get away with brute force checks, but it did not have a scalable world model, culling, fixed-step simulation, or particle pooling for larger maps.

## Refactored Architecture

### World Space

The new prototype defines fixed world dimensions:

- `WORLD_WIDTH = 5600`
- `WORLD_HEIGHT = 3600`
- `camera.x`
- `camera.y`
- `camera.zoom`

All actors, solids, anchors, vents, particles, pickups, and AI paths exist in world coordinates. The renderer applies a world-to-screen camera transform, so maps are several thousand units wide and the player no longer feels trapped inside the screen.

### Camera System

The new camera follows the player with smoothing, velocity lead, dynamic zoom, and shake:

- Zooms out as player speed increases.
- Zooms in as threat rises.
- Leads the player in the direction of travel.
- Adds shake on dash, grapple latch, hard landings, slide, and tag impacts.
- Clamps to world bounds so the world remains spatially coherent.

### Movement Controller

The player now uses persistent velocity:

- `player.vx`
- `player.vy`
- `player.acceleration`
- `player.maxSpeed`
- `player.friction`

Movement is built from acceleration, friction, max-speed clamping, air control, stamina, and cooldowns. The controller now supports:

- Hold sprint with stamina drain and recovery.
- Slide from high speed with momentum preservation and low friction.
- Double jump with air control and directional impulse.
- Wall run from high-speed wall-adjacent movement.
- Dash with short burst, cooldown, stamina cost, and impact particles.
- Vertical state through `player.z` and `player.vz` for jump height, landing, and wall-run lift.

### Physical Grapple

The grapple now has three states:

- `idle`
- `flying`
- `latched`

The hook projectile travels through the world, connects to grapple anchors or solid geometry, and then applies pull forces to the player. It preserves existing velocity, allows tangential motion to carry through, and releases on timeout or close distance instead of teleporting.

### AI Rewrite

The chaser system now includes:

- Navigation grid built from world solids.
- A* pathfinding.
- Path following with waypoint advancement.
- Line-of-sight checks.
- Cover-aware pursuit.
- Predictive target leading.
- Flanking roles.
- Interceptor role.
- Squad separation.
- Obstacle avoidance steering.
- Threat scaling over time.

The result is still a prototype, but the enemies now behave like a chase squad instead of circles moving directly at the player.

### Map Redesign

Each map is now a large gameplay space rather than a screen-relative layout.

Rooftop:

- HVAC blocks, towers, shafts, bridges, vents, elevated-feeling routes, and grapple anchors.
- Long sprint lanes mixed with wall-run surfaces and risky high-speed crossings.

Jungle:

- Dense ruins, cover zones, vine anchors, root boost routes, hidden breaks, and visibility denial.
- Cover can interfere with AI line-of-sight behavior.

Mine:

- Maze-like walls, carts, chokepoints, alternate tunnels, mine-cart boosts, and escape shafts.
- Stronger route planning pressure from A* chasers.

### Visual Upgrade

The renderer now includes:

- Camera-space world rendering.
- Culling through camera view bounds.
- Gradient floors and grid depth.
- Map-themed palettes.
- Radial lighting.
- Obstacle shadows and rim highlights.
- Particle pool.
- Runner and chaser motion trails.
- Grapple rope rendering.
- Speed-line screen effects.
- Threat vignette and danger border.
- Energy shard effects.

### HUD Redesign

The HUD is smaller and more competitive:

- Compact survival timer.
- Map/status pill.
- Threat meter.
- Stamina meter.
- Grapple readiness.
- Four compact ability/cooldown cells.
- Mobile touch joystick and action buttons.

## Verification Performed

- Extracted the JavaScript from `TAG_AAA_refactor.html`.
- Ran syntax validation with the bundled Node runtime.
- Served the output through localhost.
- Loaded the game in the in-app browser.
- Clicked into a live run.
- Verified canvas rendering was nonblank.
- Checked browser console errors: none found.
- Smoke-tested action inputs for dash, jump, and grapple: no browser errors.
- Patched a HUD overlap discovered during screenshot verification.

## Remaining Recommendations For Commercial Quality

1. Split the single HTML file into modules.

Move toward TypeScript modules with separate packages for input, camera, physics, collision, AI, map data, rendering, UI, and tuning data.

2. Build a deterministic movement test harness.

AAA-feeling movement comes from repeatable tuning. Add automated tests for sprint acceleration, slide duration, dash impulse, grapple latch timing, jump height, and wall-run exit velocity.

3. Add authored level metrics.

Track route time, chase distance, sightline length, wall-run chain length, grapple anchor spacing, and escape-loop fairness per map.

4. Upgrade collision and navigation.

Use spatial hashing or a broadphase structure for solids, and move from a coarse grid to nav meshes or annotated route graphs for more expressive AI.

5. Add animation and audio.

Movement feel will not reach the target without footstep timing, slide scrapes, grapple tension audio, wall-run sparks, chaser stingers, heartbeat/threat mix, and animation-state feedback.

6. Add an AI director.

Dead by Daylight-style tension needs pacing. Spawn pressure, flanking aggression, cooldown pressure, and recovery windows should be orchestrated by a director, not only distance-based threat.

7. Add controller-first tuning.

The current build supports keyboard and mobile touch. A Steam target needs gamepad support, aim-assist rules for grapple, remapping, accessibility, and input latency measurement.

8. Consider a 2.5D or 3D production path.

This refactor pushes the browser canvas prototype much closer to the intended feel, but the full Titanfall / Mirror's Edge fantasy eventually wants authored verticality, animation blending, collision volumes, and camera language that are easier to realize in a 3D engine.


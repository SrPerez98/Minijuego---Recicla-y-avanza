# Contributions stage 3 - Angel Carrasquilla
*Role:* Programmer

## What I did
- Developed and integrated the core minigame mechanics in Unity, including player movement, dashing, jumping, melee attacks, a health system, and item collection.

- Implemented behavior for melee and ranged enemies, including chasing, shooting, taking/dealing damage, health management, animations, and death logic.

- Set up the system for "good" and "bad" waste items, including their randomized spawning, collection mechanics, and their impact on the progress bar.

- Implemented an upgrade system that activates when the collection bar is filled, providing benefits such as item magnetism, an increased chance of spawning "good" waste, and an expanded collection radius.

- Integrated the game interface, including the main menu, "Game Over" screen, upgrade panel, collection progress bar, health (hearts), and a dash cooldown indicator.

- Integrated player and enemy animations using spritesheets, Animation Clips, and Animator Controllers.

- Added sound effects for correct/incorrect item collection, level completion, taking damage, death, dashing, jumping, shooting, projectile impacts, selecting upgrades, UI button interactions, and ambient audio.

- Configured prefabs, colliders, Rigidbody2D components, AudioSources, scenes, and Inspector references.

- Conducted testing and debugging to ensure all mechanics functioned correctly together.

## In progress
- Continuing the integration of new enemies, visual effects, and other assets provided by team members.

- Making final adjustments based on QA testing results.

- Reviewing and optimizing the gameplay experience before finalizing the build.

## Files uploaded
- Programming scripts for the player, enemies, projectiles, collection system, upgrades, UI, and game managers.

- Configured prefabs for the player, enemies, projectiles, and waste items.

- "MainMenu" and "MainArena" scenes. - Animation Clips and Animator Controllers used for the characters.

- Integrated assets for audio, user interface, and visual elements.

- Development-related files located in the `Docs/Programacion` folder, along with the project structure in Unity.

## Key Takeaways
I learned how to integrate various Unity systems, working with scripts, prefabs, animations, audio, user interfaces, collisions, and scene transitions. I also reinforced the importance of testing each mechanic incrementally, maintaining an organized project structure, and coordinating the integration of assets developed by other team members.



## Stage 4 contribution 

*What I did*
- Replaced the player sprites and integrated the new directional animations in Unity.
- Improved the movement system using Blend Trees for running, rolling, jumping and attacking.
- Updated the main menu and upgrade screens while keeping the existing button logic.
- Improved button interaction by adding visual highlight feedback on hover.
- Modified the Game Over system so the gameplay completely pauses when the player loses.
- Added a configurable lifetime to collectibles so they disappear automatically if they are not collected.
- Tested the implemented changes and corrected integration problems in Unity.

*What I learned*
- I learned how to connect new visual assets to existing gameplay systems without rebuilding the whole project.
- Blend Trees make directional animations cleaner and easier to manage.
- Small gameplay improvements, such as pausing on Game Over or removing old collectibles, help make the game feel more complete.
- Testing each change separately helps avoid breaking systems that were already working.

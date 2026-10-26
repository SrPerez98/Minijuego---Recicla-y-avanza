# Recicla y Avanza

> A 2D recycling game built in Unity where every item you collect teaches you where it belongs.

Developed for the **Programación para Videojuegos (213027A)** course at **UNAD** (Universidad Nacional Abierta y a Distancia).

---

## Table of Contents
- [About the Game](#about-the-game)
- [Gameplay](#gameplay)
- [Controls](#controls)
- [Levels](#levels)
- [How to Run](#how-to-run)
- [Repository Structure](#repository-structure)
- [Team](#team)
- [AI Usage](#ai-usage)
- [Version](#version)

---

## About the Game
**Recicla y Avanza** is a single-screen 2D action game about good recycling practices. The player controls a character who must collect waste items that appear in the scene, choosing only the ones that belong to the current level, while dodging enemy attacks.

The goal is to learn, in a fun way, which type of waste goes where.

## Gameplay
1. Waste items appear on the screen and the player collects them.
2. **Correct waste** (matching the current level) fills the **collection bar**.
3. **Wrong waste** (not matching the level) decreases the collection bar.
4. **Enemies** shoot projectiles at the player. Getting hit costs **lives**.
5. When the collection bar is full, the player **chooses an ability**.
6. Then the player faces the **final boss** of the level.

## Controls
| Action | Key |
|--------|-----|
| Move | `W` `A` `S` `D` or Arrow keys |
| Jump | `Space` |
| Dash | `Shift` |
| Collect waste | Automatic |
| Attack | Automatic |

## Levels
| Level | Waste type | Status |
|-------|------------|--------|
| 1 | Organic | In progress |
| 2 | Coming soon | Planned |
| 3 | Coming soon | Planned |

## How to Run
**Requirements:** Unity **6.6**

1. Clone or download this repository: `git clone https://github.com/SrPerez98/Minijuego---Recicla-y-avanza.git`
2. Open **Unity Hub** and click **Add project from disk**.
3. Select the cloned folder and open it with Unity 6.6.
4. Open the main scene inside `Assets/Scenes`.
5. Press the **Play** button.

## Repository Structure
| Folder | Content |
|--------|---------|
| `Assets/` | Game content (art, audio, scripts, scenes, prefabs, UI) |
| `Packages/` | Unity package dependencies |
| `ProjectSettings/` | Unity project configuration |
| `Docs/Diseno/` | Original design assets and designer contributions |
| `Docs/Audio/` | Original audio files and sound designer contributions |
| `Docs/QA/` | Test reports and QA contributions |
| `Docs/Programacion/` | Programmer contributions |

## Team
| Member | Role |
|--------|------|
| Ángel David Carrasquilla Aroca| Programmer |
| Yeycol David Aristizabal Torres| Designer |
| Joseph Andrés Núñez Padilla| Sound Designer |
| Elias Pérez | QA Tester |

Each member documented their individual work in their own folder inside `Docs/` (file `aportes-NAME.md`).

## AI Usage
Generative AI tools were used ethically and declared. The tool, version, prompts, generated results and manual modifications are documented in the AI Usage Declaration included in the `Docs/Diseno` folder.

## Version
**v1.0**

---

*Universidad Nacional Abierta y a Distancia (UNAD) - Programación para Videojuegos (213027A)*

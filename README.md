# Losing_Your_Marbles

Team Members: Collin Graves, Sahana Rao, and Cameron Simmons
Demo Video YouTube Link: https://youtu.be/70SjKs1e6XY
Final Report: https://docs.google.com/document/d/18WclnYj3ohKbKbP0Zq7i-l76rDNQov7lM4pHMFjb-Gg/edit?usp=sharing
Short Description:
You have lost your marbles in the forest! Now you have to get them back! But watch out, there are monsters that roam the forest!
This is a first person shooter game in a procedurally generated forest-maze environment. The goal of the game is to explore the forest and collect the marbles roaming around the forest. The number of marbles can be set by the player. In addition, there are monsters roaming around the forest that will attack the player. Each hit from a monster causes the player to lose 1 of their 5 health points. The player can also shoot at the monsters if they have ammo for their gun. Scattered around the forest are item boxes that give the player items such as speed boost, invisibility, marble traps, marble/monster freeze, and health potions. These can be stored and used at any time during the game. Once all of the marbles are collected, the player wins, but if the player’s health reaches 0 before then, they lose. 

Credits:

Cameron Simmons - 
  Procedural Map Generation,
  Connected maze generation,
  Spawn of marbles enemies and forest (trees and bushes),
  Runtime NavMesh bake,
  Grassy Terrain,
  Main Menu (Except instructions UI),
  Audio (Music and sound effects),
  MiniMap UI,
  Loading Scene Management,
  Player movement and gun aim,
  Invisibility and speed boost power up

Collin Graves:
  Enemy AI,
  Enemy animations,
  Idle, walking, attack, death,
  Gun scripts,
  Enemy death,
  Enemy freeze,
  Health UI

Sahana Rao:
  Marble AI/rolling,
  Marble traps,
  Freezing marbles,
  Inventory UI,
  Inventory scripts,
  Health potion,
  Instructions UI

Full Game-Play:
This is a first person shooter game where the object of the game is for the player to collect all the marbles that are roaming the forest before being overwhelmed by the monsters. 

Controls
Arrow keys or WASD keys to move around
Hold R (with Arrow Keys/WASD) to run 
Spacebar to jump
G to gather a marble that is close to you
1-6 keys to switch between items in the inventory
Left click to use the selected item
Left shift to aim the gun
Esc to release the mouse in main menu
P to pause 
P, then Q to quit to main menu

Environment
The environment is a procedurally generated forest-maze environment using a cellular automata algorithm. The settings in the main menu allow custom generation of the map.
Map Width & Map Height: changes the dimensions of the map. 
Number of Marbles: changes the number of marbles spawned within the map. 
Enemy Cap: Max number of spawned enemies 
Random Fill Percent: Affects the number of rooms in the map (more fill → less rooms)
Passage Width: Width of passage between rooms.
Forest Density: Affects many trees and bushes are spawned within the map.
Wall and Room Threshold Size: Minimum cluster size of the wall and rooms respectively (i.e. Setting this to a high number will only allow big rooms, setting it low allows for smaller rooms)
Seed: using the same seed generates the same maze (does not affect tree & bushes, marble, and enemy spawns)
For a better look a the map generation algorithm used, see the following tutorial: https://youtu.be/v7yyZZjF1z4?list=PLFt_AvWsXl0eZgMK_DT5_biRkWXftAOf9
This algorithm was modified to better fit our needs. 

Marbles
The marbles spawn randomly around the forest at the beginning of the game and are randomly roaming around. They also move away from the player when the player approaches them. The number of marbles can be controlled in the settings on the main menu. This determines how many marbles must be collected to win the game. The player needs to be within a radius of 2 to gather the marble when pressing G.

Monsters
The monsters spawn randomly around the forest and continue to spawn throughout the game. They follow the player and can attack the player when close. The monsters have a walking animation, attacking animation, idle animation, and death animation. Each attack by a monster causes the player to lose a health point. If the player goes down to a health of 0, the game is over. The player can also shoot at the monsters using the gun with the ammo in their inventory.

Items
The player starts with 5 health, 50 ammo, 20 freeze gun ammo, and 1 of each of the other items in their inventory. There are mystery item boxes scattered around the forest that give the player a random item in their inventory when they grab them. The mystery boxes continue to spawn throughout the game. The mystery boxes could give one of any of the items, or for the case of ammo, somewhere between 15-35. The ammo can be used to shoot monsters with the gun, and causes the monsters to die when shot. The traps can be laid down where the player is and can trap a marble on it. If a monster runs into a trap or it is laid down where the monster is, the monster will die. The traps also show up on the mini map so they can be returned to. They only trap a marble, and the marbles still need to be gathered by pressing G. The invisibility causes the player to be “invisible” for 30 seconds. This invisibility prevents the monsters from seeing the player. The speed boost increases the player’s run speed times 5 for 30 seconds. The freeze gun can be used to freeze monsters for 5 seconds or freeze marbles in place infinitely. The health potion restores the player’s health to 5 when used. 

External Resources:
Using raycast to have a gun shot detect an enemy - https://unity3d.com/learn/tutorials/projects/survival-shooter/harming-enemies?playlist=17144
Mixamo for enemy model and animations - https://mixamo.com
Inventory UI - https://www.youtube.com/watch?v=-xB4xEmGtCY
Maze Generation Algorithm- https://youtu.be/v7yyZZjF1z4?list=PLFt_AvWsXl0eZgMK_DT5_biRkWXftAOf9
We modified the algorithm from this tutorial to better fit our wants for a maze-forest.
Damage audio asset- https://assetstore.unity.com/packages/audio/sound-fx/voices/attack-jump-hit-damage-human-sounds-32785
Background music - https://assetstore.unity.com/packages/audio/music/orchestral/the-night-sky-35720
Gun sound effects - https://assetstore.unity.com/packages/audio/sound-fx/weapons/fog-of-war-gun-sound-fx-free-66100
Marble textures - https://assetstore.unity.com/packages/2d/textures-materials/floors/magic-ground-textures-69049
Marble texures - https://assetstore.unity.com/packages/3d/environments/fantasy/translucent-crystals-106274
Terrain Grass - https://assetstore.unity.com/packages/3d/environments/fantasy/translucent-crystals-106274
Forest Bushes - https://assetstore.unity.com/packages/3d/vegetation/plants/yughues-free-bushes-13168
Rock Walls - https://assetstore.unity.com/packages/3d/environments/yughues-free-rocks-13568
Modified these rocks to build a our map wall structure
Player movement - https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-32351
Runtime navmesh bake - https://github.com/Unity-Technologies/NavMeshComponents/tree/master/Assets/Examples/Scripts
Gun - https://assetstore.unity.com/packages/3d/props/guns/scifi-gun-collection-56350
Trap texture - https://assetstore.unity.com/packages/2d/textures-materials/yughues-free-grids-nets-materials-13004


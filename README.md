# Dungeon Gourmet
# Members
ai22m025 - Lukas Hartinger  
ai22m012 - Nico Winter  
ai22m007 - Philipp Lakheshar  


__Summary:__ Dungeon Gourmet is a roguelike action RPG implemented in the Unreal engine 5.2. In the game a meowster chef, armed with a giant cleaver and his throwing forks, embarks on a quest for the ultimate recipe. However, to his surprise, he encounters never-ending hordes of enemies, in which each wave he survives grants him one of three bonuses that make him stronger on his further journey.  

__Zusammenfassung:__ Dungeon Gourmet ist ein roguelike Action-RPG, das in der Unreal Engine 5.2 umgesetzt wurde. In dem Spiel begibt sich ein miausterhafter Koch, bewaffnet mit einem riesigen Hackbeil und seinen Wurfgabeln, auf die Suche nach dem ultimativen Rezept. Zu seiner Überraschung trifft er jedoch auf nicht enden wollende Horden von Feinden, wobei ihm jede überlebte Welle einen von drei Boni gewährt, die ihn auf seiner weiteren Reise stärker machen.

# Original Concept
![Moodboard](img/moodboard.PNG)

## Idea
Our original idea was a rougelite action RPG with two gameplay loops, an inner (main) gameplay loop that lets the player slay enemies in an outdoor dungeon, with the player receiving loop-instanced upgrades after each wave, and a persistent outer loop that allows the player to combine and use loot obtained in the main loop to obtain permanent power-ups or change the overall gameplay. Due to time constraints, we have not implemented the outer game loop, but the game can still be expanded to include it.

## Genre & Target Audience
Roguelike Action RPG with top-down view. We want to appeal to casual players with the accessible; child-friendly art style and non-casual players with the restrictive combat system. We aim for a medium difficulty level that neither discourages nor bores either group. 

## Gameplay Feeling
Heavy, calculated gameplay where every move counts. The weight of the sword and the body type of the cat chef should be reflected in the gaming experience.

## Art Style
Low-poly assets with warm colors. Outside dungeons rather than indoors, but still quite mysterious and with high contrast. The main character is
different from the world around him due to the strong contrast and different color scheme
around him. The world is 3D, low-poly and cartoony. See the moodboard.

## Platform
Windows

## Development Environment
The game was implemented in Unreal 5.2.  
A possible switch to Unity was considered, but ultimately discarded due to time constraints. If we were to start from scratch, Unity seems more suitable in retrospect.

# Implemented Features
## Basic Movement & Camera Settings
__Location:__ DungeonGourmet/Content/Characters/MainCharacter/BP_ThirdPersonCharacter

### Movement
The basic movement is based on the position and orientation of the camera. The player can use the W, A, S, D keys to move on a plane parallel to the XY plane. We simply use the xy parts of the camera's forward vector and normalize them to get the direction we want our character to move when we make the up/down and left/right inputs. Everything else is done automatically by UE. We initially implemented this to only go in +/- x/y direction, but when we changed the camera perspective, it was pretty obvious that the previous implementation wouldn't be enough.

### Camera
As we want to have a top-down roguelike, we had to move the camera in a fairly specific way. It is centered a bit away, centered on our main character and supports a fairly wide FOV, as the world surrounding our character could not be seen otherwise.  
We also used the camera's post-processing options to give our game more of the look and color scheme suggested in our GDD's mood board. To do this, we applied gamma correction, additional gain and color offset. This initially resulted in a more earthy, green look, which was desirable as we were aiming for a more nature-like appearance, but it proved to be hard on the eyes, so after some trial and error we adjusted the parameters to a more neutral and not too contrasty look.

## Level
The level consists of a single large playing field, with a more or less safe starting area. The level is designed to be close to nature and should depict a forrest next to a mountain range. 

__Location:__ DungeonGourmet/Content/Levels/Level1;  DungeonGourmet/Content/LevelPrototyping/** (assets);
### Landscape
The landscape has been designed using the landscape editor of unreal engine. The "textures" are 4 different materials, which all are just flat colors: Grass, Path (brown), Mountain and Snow. The material has a normal and tangent map based on the X and Y coordinates crossed, to give the blocky visual effect. 
![Landscape](img/Level.png)
![Normals and tangents](img/levelshading.png)

For the entry area, invisible walls are used, as otherwise the player could leave the playing area. Placing trees there would not have made sense, as otherwise there would be no way for the meowsterful chef to enter or leave the area in which the game takes place.

### Landscape details
The trees, rocks and bushes have mainly been placed using UE5s mesh paint tool. Most big assets cannot be walked through by the player, ensuring a solid playing area. Some rocks and other assets (like the well), mainly the bigger ones, have been placed by hand. The assets have been taken from itch.io and are free to use. 

![Treeassets](img/trees.png) 
![Foliage](img/foliage.png) 

## Main Character Model & Main Weapon
![MainCharacter](img/MCMesh.PNG)

## Main Character Animations
## Throwing Knives
## Dodge Roll
## Slash Attack

## Enemies
__Location:__ DungeonGourmet/Content/Characters/Enemy/**; Models/Enemy (.fbx, .blend files);  
We have implemented two enemy classes. In the game, these classes serve as the basis for the spawned enemies, which are further modified by the enemy spawner to be bigger/stronger or weaker/smaller. Both enemies use the same enemy AI controller with a behaviour tree, which specifies on which conditions the enemy pawn shall roam around, wait, chase the player/attack, and die. Futhermore those behaviours are defined in the associated behaviuor task blueprint. In order to detect the player we used a pawn sensing component. Enemies have Aside from appearance (mesh) and animations, both enemies differ in the way their attack is implemented. Each enemy has its unique idle, walk, attack, and death animation, which behaviours are defined in their animation blueprint classes, whereby the particular animations are in the Animation folder. We used a blendspace for the transition between idle and walk. On attack we play the corresponding animation in which we defined notifications, in order to know when to apply the attack.
### Skeleton
![Skeleton](img/Skeleton.PNG)  
On _Apply Attack_ we use sphere tracing in the range of the attack raduis in order to check if the attack hit and if so apply the damage to the player.
### Slime
![Slime](img/Slime.PNG)  
Slime inherits the EnemyAI (Skeleton) class and overrides the _Apply Attack_ function to be empty, as the slime applies damage on overlap with the player character. Its attack animation is a forward thrust, which combined with the behaviour in the animation blueprint results in a dash ingame (increased velocity, no collision with pawns). Furthermore the slime glows on attack.


## Enemy waves
The enemy waves have been designed to teach the players the enemies first, before becoming a bigger challenge. The skeletons are introduced first, one skeleton at a time. In the second wave, the player must battle against several enemies at once, learning to deal with that specific situation. The slimes are introduced afterwards, once again with a single slime at first, then a single strong slime, and afterwards 2 slimes. 

Having learned the different enemy types, players must now face combinations of the two, with different themes to each fight. There is a wave consisting of many small enemies, a wave consisting of 2 massive slimes, 2 big random enemies and finally a battle against 10 strong enemies where 5 enemies can be on the screen at one time. 

## Enemy Wave Spawner
Enemys are spawned at predetermined locations. The logic is that each wave has a pool of enemys and they are randomly distributed to the spawn location. 
There are some parameters that can be set for the waves:
![WavePoolStructure](img/wavepool.PNG)

- A map for the enemy type and the number of these enemys
- maximum enemies on the screen 
- and a damage minimum and maximum multiplier to the base damage range


## Power Up System
Players start the game with basic stats, which can be improved after each round. They have the option to choose from three different Powerups, each affecting certain stats in either a positive or negative way. To keep the system easy to manage, all powerups are stored in a JSON file, simplifying the process of balancing new powerups. When adding new stats or a new skill image, the existing powerups are not compromised.
## Audio
## UI Widgets
The UI Widgets are Preset pages that are created seperrately and are faded in and out whenever needed.
### Ingame UI 
The functionality and the resources behind the ingame overlay can be found inside Content/UI/IngameOverlay.

The overlay consists of two elements which show the cooldown of the dodgroll and the amount of HP the character has. Both of them implemented using progress bars.

The dodge roll UI element receives time when the roll is available again and the cooldown time of the roll and calculates the percentage of how far the progress bar should be filled. The display is not perfectly accurate, as this leads to a better gamefeel.

There are also two counters displayed for the current Wave and the enemies remaining.

The Power Ups are randomised before they are filled into the Button element after a wave is completed. 
![InGameWidgets](img/ingameui.PNG)



## Visual Effects
The visual effects on the camera, which were present during the last milestone of the game have been removed, as they did not match the wanted visuals. The main color scheme of the game is now determined by the materials of the assets, as well as the Skybox. This leads to the warm colors seen in the game. 
### Bloodsplosion
### Damage Numbers
### Player Damage Effect

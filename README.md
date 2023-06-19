# Dungeon Gourmet

# Features

## Basic Movement
The functionality behind the basic movement is implemented on "BP_ThirdPersonCharacter" inside Content/Characters/MainCharacter. 

Basic movement is based around the position and orientation of the camera. As our character only moves on a plane parallel to the XY plane, we can simply use the xy parts of the forward vector of the camera and normalize them to get the direction our character should move given an input to go up/down and left/right. Everything else is automatically done by UE.

Time spent: 10h

## Main Character Model
The main character has been modeled using blender. We thought that having a toon/low-poly look would work well for our current level of artistic knowledge. The model is supposed to look like a cat in a white cooking robe. The full model and blender file can be found under models/mainCharacter. The character was mainly created by  using the knife and extrusion tools and shoving vertices around. 

We also tried to rig the character manually, which actually worked pretty well. We did not however wish to create all animations by ourselves, and as mixamo does not like having a model with bones, we had them removed afterwards.

Time spent: 15h

## Main Character Animations
The animations of the main character all come from mixamo. We chose to use the "Greatsword pack" which contains many animations which would be relevant to our game. All animation files can be found inside unreal under Content/characters/mainCharacter/animations. 

The roll and attack animation are played using a montage, as they are typically only played once and should override all other animations. 

The idle-walk-run animations are contained in a blendspace "BS_Walk", to allow smooth blending of the animations given the speed of the main character. 

The animations are merged in "ABP_MainCharacter". Here, we have a fullBodySlot for the roll animation and an upperBodySlot for the attack animation. This allows the character to roll with the whole body and to attack while walking around. The base pose that is supplied is what is output by BS_Walk.

Time spent: 15h

## Camera Settings
Our camera sits on our BP_ThirdPersonCharacter in Content/Characters/MainCharacter.  
As we want to have a top-down roguelike, we had to move the camera in a fairly specific way. It is centered a bit away, centered on our main character and supports a fairly wide FOV, as the world surrounding our character could not be seen otherwise. 

We also used the Post Processing options on the camera to give our game more of the look and the colorscheme suggested in the moodboard of our GDD. This is done using some gamma correction, some extra gain and some offset of the colors. This results in a more earthy, green look, therefore suggesting some woody or very nature related world. 

Time spent: 10h

## Dodge Roll
The functionality behind the dodge roll is implemented on "BP_ThirdPersonCharacter" inside Content/Characters/MainCharacter. 
The dodge is triggered when pressing the space key on keyboard. 

The logic first checks whether the dodge is on cooldown and whether another animation is currently playing. If neither is the case, the dodge animation montage is played and the dodge cooldown is set. During the dodge animation, a timeline is used to propell the character forward by changing its velocity. 

Time spent: 10h

## Slash Attack
The functionality behind the slash attack is implemented on "BP_ThirdPersonCharacter" inside Content/Characters/MainCharacter. The slash is triggered when pressing 1 on the keyboard. 

The logic behind the slash merely checks whether another animation is currently playing, and if not, it uses a doOnce node to trigger the slash animation once. The animation has a notify called "Hit_Slash_1" which is linked to trigger the "CheckHit" event. This is also implemented in the "BP_ThirdPersonCharacter". 

The check hit looks up all enemy AI actors and checks for each whether they are in some variable range from a socket on the sword of our main character. (The sword can be found under Content/Characters/MainCharacter/Weapon). If an enemy is hit, a sound is played and the ApplyDamage method is called for that enemy.

Time spent: 10h

## Ingame UI
The functionality and the resources behind the ingame overlay can be found inside Content/UI/IngameOverlay.

The overlay consists of two elements which show the cooldown of the dodgroll and the amount of HP the character has. Both of them implemented using progress bars. 

The dodge roll UI element receives time when the roll is available again  and the cooldown time of the roll and calculates the percentage of how far the progress bar should be filled. The display is not perfectly accurate, as this leads to a better gamefeel. 

The icons are from https://game-icons.net/ and have been edited to fit our needs. 

Time spent: 10h

## Pause UI
Explored various mockup designs with food themes, such as a round menu for pizza and a cake with a fork serving as a pointer.
Initially attempted to incorporate the 3D model of the sword into the Pause Menu, but settled for a 2D variation of the design and included the sprite on the button.

## Weapon To Character
The weapon the of main character is located in Content/Characters/MainCharacter/Weapons. This weapon, as well as the main character and the throwing knives, were all modelled by us using blender.

The main character has a socket on his right arm to hold the sword. Sadly, as the arms of our character are currently too short, he does not hold the sword. Due to the camera perspective this is barely visible however. 

During BeginPlay inside BP_ThirdPersonCharacter the sword is created and put onto the socket. The sword is saved to be used during the attack calculations.

Time spent: 10h


## Enemy
The functionality behind the enemy is implemented in "Enemy_AI" inside Content/Characters/Enemy, the resources are also located in that folder.

As the enemy follows our character, it needs to look towards the character, which we implemented ourselves by using the locations of the enemy and the player. This however does not work properly yet and needs to be improved, we therefore stayed with the implementation of UE. This however means, that the enemies sometimes rotate to quickly.

The enemy also has animations: Idle, Walking, Attack and Death. Walking and Idle are contained in a blendspace as with the main character, attack and death are played using montages. Once the enemy dies, a particle system is created to have the enemy disappear behind it. 

The enemy has a Pawn Sensing node which senses if the main character reaches into the field of view. This lets the enemy follow the player until it is killed or the player is too far away. If the enemy is in a certain attack range the hit animation and an attack trace function is executed to hit the player.

Time spent: 25h

## Audio 
We attempted to play snippets of the Background Music and make them overlapping with a fade in/out to achieve a looping effect. However, we were unsuccessful because it is not possible to set delays depending on the length of an audio file dynamically, they have to be hard coded in the cue file. Then we tried to implement the fade in and out effect in the level Blueprint. Although there are nodes that could be used, we did not receive any audio feedback when using the Audio Component. We will revisit this issue next semester. Currently, we have implemented a feature where the audio starts when the Level begins and pauses when the pause UI Component appears. The game Audio is paused until the UI element is closed.

Time spent:

## Particle System (Death Animation)
Implemented an explosion with Niagara, by creating a custom material to get different particles. There is also the possibility to change the base colors of the explosions to make the System modular for future Enemies.+

Time spent:
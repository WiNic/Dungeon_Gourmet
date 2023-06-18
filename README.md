# Dungeon Gourmet

# Features

## Basic Movement
The functionality behind the basic movement is implemented on "BP_ThirdPersonCharacter" inside Content/Characters/MainCharacter. 

Basic movement is based around the position and orientation of the camera. As our character only moves on a plane parallel to the XY plane, we can simply use the xy parts of the forward vector of the camera and normalize them to get the direction our character should move given an input to go up/down and left/right. Everything else is automatically done by UE.

## Main Character Model
The main character has been modeled using blender. We thought that having a toon/low-poly look would work well for our current level of artistic knowledge. The model is supposed to look like a cat in a white cooking robe. The full model and blender file can be found under models/mainCharacter. The character was mainly created by  using the knife and extrusion tools and shoving vertices around. 

We also tried to rig the character manually, which actually worked pretty well. We did not however wish to create all animations by ourselves, and as mixamo does not like having a model with bones, we had them removed afterwards.

## Main Character Animations
The animations of the main character all come from mixamo. We chose to use the "Greatsword pack" which contains many animations which would be relevant to our game. All animation files can be found inside unreal under Content/characters/mainCharacter/animations. 

The roll and attack animation are played using a montage, as they are typically only played once and should override all other animations. 

The idle-walk-run animations are contained in a blendspace "BS_Walk", to allow smooth blending of the animations given the speed of the main character. 

The animations are merged in "ABP_MainCharacter". Here, we have a fullBodySlot for the roll animation and an upperBodySlot for the attack animation. This allows the character to roll with the whole body and to attack while walking around. The base pose that is supplied is what is output by BS_Walk.

## Camera Settings
Our camera sits on our BP_ThirdPersonCharacter in Content/Characters/MainCharacter.  
As we want to have a top-down roguelike, we had to move the camera in a fairly specific way. It is centered a bit away, centered on our main character and supports a fairly wide FOV, as the world surrounding our character could not be seen otherwise. 

We also used the Post Processing options on the camera to give our game more of the look and the colorscheme suggested in the moodboard of our GDD. This is done using some gamma correction, some extra gain and some offset of the colors. This results in a more earthy, green look, therefore suggesting some woody or very nature related world. 

## Dodge Roll
The functionality behind the dodge roll is implemented on "BP_ThirdPersonCharacter" inside Content/Characters/MainCharacter. 
The dodge is triggered when pressing the space key on keyboard. 

The logic first checks whether the dodge is on cooldown and whether another animation is currently playing. If neither is the case, the dodge animation montage is played and the dodge cooldown is set. During the dodge animation, a timeline is used to propell the character forward by changing its velocity. 

## Slash Attack
The functionality behind the slash attack is implemented on "BP_ThirdPersonCharacter" inside Content/Characters/MainCharacter. The slash is triggered when pressing 1 on the keyboard. 

The logic behind the slash merely checks whether another animation is currently playing, and if not, it uses a doOnce node to trigger the slash animation once. The animation has a notify called "Hit_Slash_1" which is linked to trigger the "CheckHit" event. This is also implemented in the "BP_ThirdPersonCharacter". 

The check hit looks up all enemy AI actors and checks for each whether they are in some variable range from a socket on the sword of our main character. (The sword can be found under Content/Characters/MainCharacter/Weapon). If an enemy is hit, a sound is played and the ApplyDamage method is called for that enemy. 

## Game UI

## Pause UI
Explored various mockup designs with food themes, such as a round menu for pizza and a cake with a fork serving as a pointer.
Initially attempted to incorporate the 3D model of the sword into the Pause Menu, but settled for a 2D variation of the design and included the sprite on the button.

## Weapon To Character

## Enemy
The enemy has a Pawn Sensing node and if the Main Character reaches into the field of view of the enemy it starts following the player until it is killed. If the enemy is in a certain attack range the hit animation and an attack trace function is executed.

## Audio 
We attempted to play snippets of the Background Music and make them overlapping with a fade in/out to achieve a looping effect. However, we were unsuccessful because it is not possible to set delays depending on the length of an audio file dynamically, they have to be hard coded in the cue file. Then we tried to implement the fade in and out effect in the level Blueprint. Although there are nodes that could be used, we did not receive any audio feedback when using the Audio Component. We will revisit this issue next semester. Currently, we have implemented a feature where the audio starts when the Level begins and pauses when the pause UI Component appears. The game Audio is paused until the UI element is closed.

## Particle System (Death Animation)
Implemented an explosion with Niagara, by creating a custom matirial to get different particles. There is also the possibility to change the base colors of the explosions to make the System modular for future Enemys.
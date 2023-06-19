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

## Level Streaming
Levels are located in Content/Levels. The streaming functionality of the levels is implemented in their blueprints and the current level setup consists of 3 levels - Persistent, StartWorld and DungeonRoom1, where Persistent is the underlying base level that contains components that are static in all levels (currently only lighting). At the BeginPlay event, this level loads the StartWorld level, which is the actual starting point for our player. Currently, the player can switch from StartWorld to DungeonRoom1 and vice versa. In the respective blueprints, the streaming functionality is indicated by the comment sections "Level Setup" and "Load Another Level". Levels are loaded when the player triggers the associated trigger volume. This deactivates the player's input, automatically moves the character to a destination point behind the volume, and plays a camera fade-out effect for a visually smooth transition. Inside the other level, the player is teleported to the destination point that marks the beginning of the level before player input is activated and faded back in. In future development, we plan to use this approach for all level transitions.

One of the more time-consuming sources of errors during implementation was the RecastNavMesh component, where "Runtime Generation" must be set to the "Dynamic" option, otherwise the NavMesh will not be adapted to the new level and the enemy AI will not be able to move after loading another level.

Next steps/further improvements: Wrap the level streaming functionality into two functions with sensible parameters to avoid clutter in the blueprints of further levels.

Time spent: 20h

## Throwing Knives
Knives (or rather forks in our case) can be thrown with a right mouse click. They have an internal cooldown of 2 seconds, but the corresponding widget has not been implemented yet. The knives are thrown in the character's facing direction.
The spawning/throwing functionality has been implemented in the main character's blueprint in Content/Characters/MainCharacter/BP_ThirdPersonCharacter, with the projectile itself implemented as an Actor in Content/Characters/MainCharacter/Weapons/BP_Knife.

__BP_ThirdPersonCharacter:__  
The main character has 3 invisible and collision-free spheres KnifeRef[LMR] (visible and adjustable in the viewport of BP_ThirdPersonCharacter), which are used as references where to spawn the knives and in which direction to throw them. The implementation is called on the "InputAction Attack_2" event and consists of the controlflow that checks/sets the cooldown and obtains/releases the animation lock (only one animation at a time), and the actual spawning of the knives. To throw them in line of sight, the world rotation of the capsule component is specified. For the adjustment of the velocity rotation from the character to a specific knife instance, the relative rotation of the reference is provided, as this functionality is implemented in the Actor class BP_Knife. After spawning, the local rotation (for mesh & collision box) of the knives is adjusted to the rotation of their velocity, i.e. their flight direction. With the activation of their projectile movement component the knives are thrown.

__BP_Knife:__  
The BP_Knife Actor-Class offers two possibilities to adjust the velocity rotation. Either from the spawning actor (emitter) to the target actor (not used in our project yet, but may be useful later) or by supplied rotator (event BeginPlay). Also, the velocity is adjusted by adding the velocity of the emitter to get an authentic throwing feeling. The collision of the projectile is set to collide with everything except pawns, for which overlapping events are generated. (This distinction may be useful later for piercing projectiles.) When overlapping with the enemy AI, it inflicts damage, whereby the projectile is first deactivated as well as made invisible and destroyed after a short delay to give the application enough time to display the damage numbers, as the projectile's speed is needed for their impulse.

Premature deletion of the projectile actor proved to be a source of many errors during implementation. Unfortunately, many error messages were misleading, as they usually pointed to another part of the blueprint that was working well. Also, in one of the first versions, the projectile threw enemies back and stunned them (which was implemented in the enemy AI blueprint), but this functionality kept crashing the game randomly. We have removed this part for now, as we could not find the underlying cause yet, but we plan to re-implement this functionality in the knife actor class.

Next steps/further improvements: Move the projectile/mesh rotation to BP_Knife and rotate together with the velocity. Add knockback and stun functionality as an effect the Actor Knife applies to the enemy (analogous to damage).

Time spent: 35h

## Damage Numbers
When the enemy takes damage, a text is displayed in the center of the enemy AI indicating the damage amount. After a short period of time, the displayed text fades out and moves upward. Note that when the AI is hit with a projectile, the displayed text is shot out of the enemy actor with a dampened impulse from the hitting projectile. This functionality is implemented under Content/UI/FloatingCombatText and consists of the following 3 parts:

__FloatingCombatText:__
The widget that contains the combat text. It is set to be always visible in screen space and contains a "FloatUpFadeOut" animation that starts linearly and transitions to a smooth curve (visible in the curve editor), with the displayed text gradually fading out and moving up. The text itself is set as a variable, since we want to define it dynamically at runtime. This can be done using the "Initialize" event defined in the blueprint graph. In addition, the playback speed is adjustable and a custom event is called after the widget animation is finished. This is needed in order to know when the associated actor can be deleted.

__BP_FloatingCombatText:__
The actor associated with the widget that is needed for the physics simulation. It is configured to be invisible and ignore all collisions. In addition to initializing the widget, it binds its destructor to the previously defined custom event and takes a velocity as input that is used as impulse for its movement. Since we don't want to shoot the displayed text too far away from the opponent, the length of the velocity vector is divided by 1000, multiplied by the input variable AbsLinearDampingFactor, and then set as a linear damping factor which allows us to adjust the distance it will traverse.

__BPC_DamageDisplay:__
The blueprint for a component that binds to its owner's damage event and creates a FloatingCombatText actor at the owner's position when it is triggered. This represents the final component added to the enemy AI blueprint.

This modular implementation style allows the combat text actor to be reused for displays other than damage, which can then be defined as another component analogous to BPC_DamageDisplay, which in turn can be added simply by adding the component to the respective actor.

Next steps/further improvements: Add the possibility for other colors for the FloatingCombatText widget and implement more variations, such as red for main character damage, green for main character healing, gold for enemy AI critical hits, etc.

Time spent: 25h
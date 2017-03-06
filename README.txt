---------------------------------------------------------------------------------------------------------------------------------------------------------

My mindset when working on this project was to build an architecture that I feel could grow in to a much more complicated game. Given this priority the textures and obstacles are not exactly polished. The controls could also be improved to get rid of the "spinning on ice" effect. However the lack of polish with regards to graphics and the "spinning" effect could be solved without the need of a terrible overall of how the code is organized. 

---------------------------------------------------------------------------------------------------------------------------------------------------------

Controls:

	W: rotate forward
	A: rotate left
	S: rotate backward
	D: rotate right
	Space: Jump!

Directions:

Try to collect X blue blocks. Beware the red blocks. They will explode on contact removing half of your collected objects and also damaging your health. If you health falls to 0 you lose the game!

---------------------------------------------------------------------------------------------------------------------------------------------------------

The main objects in this game are:
	Player
	Attachable
	Destructable

Each of these objects are controlled by a custom Controller class. PlayerController, AttachableController, DestructableController.

This game is quite simple and these classes by themselves would probably be enough. But when the game grows in complexity the  monolithic controller scheme will end up becoming cumbersome. In order to address this the behavior in some cases are delegated  to States of the systems.

As an example the AttachableController has a AttachedState and a FreeState. Enter/Exit calls come for free in the state system making it easy to trigger behavior/sounds during state transitions.

The PlayerController has an GroundedState (for moving) a JumpState (Jump OnEnter, then do not allow input. Transition to Grounded on a collision with the ground), an IdleState (for GameOver)

The lifecyle of the game is controlled by a GameController, who also has states (GameOn, GameOver). The GameController observes the player and responds accordingly (transitions to a win/loss state). The GameController is also responsible for creating all of the objects. This would likely be moved to another system as the game grows in complexity.

MonoBehaviourSingeton is class that helps create Singletons and attach them to a SingletonsParent object. Things like ParticleControllers and SoundControllers are good candidates for being Singletons in this sense.

AudioClips are attached to a GameSounds script. Other scripts can locate this and use its AudioClips to play sounds via the SoundController singleton.

The GameOverDialog is a simple dialog that inherits from DialogController. In the past I have used a more complicated DialogController that maintains the stack of all DialogControllers currently being displayed. For now this implementation simple Shows/Dismisses the dialog.

Destroy and Instiantiate only appear in ObjectPoolController. Eventually we would want to fully create an Object Pool. For now the ObjectPoolController does not handle pooling, but since all calls go though this currently we should get Object Pooling very easily in the future.

---------------------------------------------------------------------------------------------------------------------------------------------------------

Folder Structure:
	Art: 
		Contains the materials, sounds files, and textures

	Base:
		Contains a few classes I wrote and used while working on a previous project Chrono Strike (worked on this with Jason Strickland)

	Downloaded Assets: 
		A bunch of mostly unused assets. I wanted to put a simple texture on the ground

	Game:
		Contains all of the gameplay code

	Resources:
		Contains the prefabs and physics materials

---------------------------------------------------------------------------------------------------------------------------------------------------------

Known Bugs:
	Light gets dimmer on restarts
	Detached attachables have trouble going back to there correct scale

---------------------------------------------------------------------------------------------------------------------------------------------------------

Caveats:
	A glass shader is used and was taking from 
		http://wiki.unity3d.com/index.php/Glass_Shader




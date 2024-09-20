ASSIGNMENT SPACE SHOOTER GAME

UNITY DOTS


Project Initialization

I Created a new 2D Standard Unity project.
Enabled parallel import in Player Settings to accelerate the import process.

ECS Setup
I installed the Entities package from the Unity Registry in the Package Manager.
Implemented an ECS Bootstrapper script to serve as the central game manager.

Core Systems and Components
Developed the following systems, each with corresponding components:
Enemy Spawner System
Shooting System
Spaceship Controller System
Spaceship Input System
Collision Detection System (for projectile-enemy and player-enemy interactions)

I created data container components for:
The Enemy
The Projectile
And for the spaceship Input

Implemented the logic for each system using the appropriate components.

Scene Configuration
Setted up a subscene containing the player game object.
Created objects to house the spawner script and made the necessary authoring scripts.
Configured the enemy ship references and adjust parameters like speed and spawn time.

Final Steps
Ensured all references were properly set and values were fine.



From the editor if you open it from there, in the main scene you should check "V" the test scene before starting.

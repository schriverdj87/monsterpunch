# Monster Punch
Punch your way through an endless wave of monsters in this simple game.

# Where's the EXE File?
bin/Debug/MonsterPunch.exe

# How Do I Play
Press start for a new game. 
Click or press space to launch the fist when it is aligned with a monster.
The game ends when one reaches the bottom of the screen.

# Main File Breakdown
Main.cs - The main handling form that swaps the game screens around and runs the required functions that need to be run about 30 times per second.
MonsterPunchMenu.cs - First screen shown by Main.cs, holds the play button and the title image.
Form1.cs - The presentation layer of the main game. Responsible for audio, visuals, and relaying input from the player to the main game.
Player.cs - A custom picturebox used by Form1.cs to display the fist.
CoreSlideAndLaunch.cs - The main C# game that runs independently of any visual aspect.
CoreSlideAndLaunchNME.cs - Used by CoreSlideAndLaunch.cs for the enemies. 

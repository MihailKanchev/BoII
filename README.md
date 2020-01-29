# A very simple, bare-bones PG algorithm for the Binding of Isaac level generation.

The Binding of Isaac has procedural generation in quite a few layers of the game. From the very structure of a floor with its many levels to the props and enemies, generated live, in-game.
With the current integration I try to recreate the structure of the game's rooms.
I am yet to define the type of algorithm (it took great inspiration from Prim's algorithm), but it works in a couple of very simple steps.

The entire integration is built in Unity, making use of C# and Unity libraries.
The algorithm executes every frame, but with a single concern- it can never be fully random just because of how System.Random operates and the number of operations that have to be executed.

1. The algorithm takes in a 2d array and fills it up with 0s.

2. estimates te center of the mesh by deviding both of its axes.

3. After the center of the array is found, a 1 is written there to indicate the position of the starting room.

4. The algorithm then generates a random x and y values, checks if the room coresponding to these values has any neighbors and if it does, the algorithm writes a 2 in its spot, indicating the existance of a room there.

You can influence the structure of the level by dictating a limit of how many neighbors a room should have.

I have left a value maxRooms which serves as a limit to how many rooms the algorithm should generate.

I have also given the ability to switch the mesh size, making it the size you would like it to be.

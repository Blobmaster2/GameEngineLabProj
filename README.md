Scott Murphy - 100826964

### Project
My project is called "Ultra Guy: Sandwich Fiend", and it is a basic 2D platformer. 
You must scale the treacherous terrain and avoid hangry guys that are fiending for your sandwich. 

### Diagram - Singleton
```mermaid
flowchart
	n1@{ label: "Rectangle" }
	n2@{ label: "Rectangle" }
	n1["Game Manager (Singleton)"]
	n2["Check for existing instance"]
	style n1 color:#000000,fill:#7ED957
	n3@{ label: "Rectangle" }
	n2
	n3["Start Timer"]
	n4["Every 1 second:"]
	n3 --- n4
	n5@{ label: "Rectangle" }
	n4
	n5
	n5["Update UI"]
	n4
	n6["Remove 1 second from timer"]
	n4 --- n6
	n6 --- n5
	n5["Update Timer UI"] --- n4
	n7@{ label: "Rectangle" }
	n1 --- n7["Awake()"]
	n7 --- n3
	n7 --- n2
	n8["Destroy if duplicate instance"]
	n2["Check for existing instance?"] --- n8
	n1 --- n9["Update()"]
	n9 --- n10["Check if player is falling too low"]
	n11["Enemy kills player"]
	n11 --- n12["Game Manager (Singleton)"]
	style n12 color:#000000,fill:#7ED957
	n12
	n13["Removes 1 life"]
	n13 ---|"false"| n14["Reset Scene"]
	n13["if below 0 lives"] ---|"true"| n15["Show game over scene"]
	n12 --- n16["Removes 1 life"]
	n16 --- n13["if below 0 lives?"]
	n17["Player reaches goal"]
	n18@{ label: "Rectangle" }
	n17 --- n18["Game Manager (Singleton)"]
	style n18 color:#000000,fill:#7ED957
	n18 --- n19["Shows win  scene"]
	style n2 color:#000000,fill:#FFBD59
	style n13 color:#000000,fill:#FFBD59
	style n4 color:#000000,fill:#CB6CE6
	n14 --- n20["Update Life UI"]
```

### Answers - Singleton:
I chose to use a Singleton to create a Game Manager that manages aspects of the game. I decided to do a Game Manager class as a singleton because there are only supposed to be one of them, and I want it to persist across scenes to save data. 

I think that this is a beneficial way to do a Singleton because it will allow me to build it out when I add more features into the game. The Game Manager should be able to be accessed from anywhere, and it should be able to persist across scenes to save it's data since it is the object that is controlling some of the game states.


### Diagram - Factory
```
flowchart
	n1["Factory"]
	n1["ObjectFactory"]
	n2["ISpawnable"]
	n1 --- n3["CreateObject<T>(position) where T : ISpawnable"]
	n3 --- n4["Spawn the object."]
	n2 --- n5["Spawn() --> spawns the object"]
	n2 --- n6["Initialize() --> initializes any properties I might want with the object"]
	n6 --- n7["in this case, starts the object with a Vector2 force applied"]
	n2 --- n8["Despawn() --> despawns the object"]
	n5
	n9["Cannon Class"]
	n9["Cannon Class (Example spawner)"] --- n10["Uses ObjectFactory to spawn SpikeBall"]
	n10 --- n11["Initializes Spikeball speed through ISpawnable.Initialize()"]
```

### Answers - Week 2:

I decided to go with a factory that could be used to spawn any number of objects, but for the sake of the question, mostly enemy objects. I used a static ObjectFactory class with a CreateObject<T>() method that can spawn objects that have the ISpawnable interface implemented. It calls Spawn() on the object, then Initialize() to initialize any properties that I would want to spawn on the object.

The factory is a good choice for spawning these objects because there can be a need for any number of them in the game at any given time. With the way I created it, it is able to spawn any object that has the ISpawnable interface on it with custom properties that I can implement later on.
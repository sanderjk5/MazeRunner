using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MainScript : MonoBehaviour
{
    //The width of the maze.
    public static int Width { get; set; }
    //The height of the maze.
    public static int Height { get; set; }
    //The number of nodes of the maze (width * height).
    public static int NumberOfNodes { get; set; }
    //The number of edges of the maze.
    public static int NumberOfEdges { get; set; }
    //The number of states.
    public static int NumberOfStates { get; set; }
    //The number of buttons.
    public static int NumberOfButtons { get; set; }
    //Contains all nodes of the maze.
    public static Dictionary<int, NodeController> AllNodes { get; set; }
    //Contains all edges of the maze.
    public static List<EdgeController> AllEdges { get; set; }
    //Contains all buttons of the maze.
    public static List<ButtonController> AllButtons { get; private set; }
    //The colors of the obstacles and corresponding buttons.
    public static List<Color> Colors { get; set; }
    //The current state of the game.
    public static int CurrentState { get; set; }
    //The current number of steps of the player.
    public static int CurrentStepCount { get; set; }
    //The optimal path between start and end
    public static List<NodeController> ShortestPath { get; private set; }
    //Scales the maze (1: 18X10, 0.5f: 36X20)
    public static float ScaleMazeSize { get; set; }
    //The current level of the level game modus.
    public static int CurrentLevelCount { get; set; }
    //Contains all gameobjects. Deletes them after every level.
    public static List<GameObject> GarbageCollectorGameObjects { get; set; }
    //The optimal amount of steps to exit of the maze.
    public static int OptimalStepCount { get; set; }
    //Enables/Disables the user input.
    public static bool EnableUserInput { get; set; }

    public static bool IsBattleGameMode { get; set; }

    //The prefab of the walls.
    public GameObject createWallsPrefab;
    //The prefab of the nodes.
    public GameObject nodePrefab;
    //The prefab of the edges.
    public GameObject edgePrefab;
    //The prefab of the Aldous Broder Algorithm
    public GameObject aldousBroderAlgorithmPrefab;
    //The prefab of dijkstra.
    public GameObject modifiedDijkstraAlgorithmPrefab;
    //The prefab of button.
    public GameObject buttonPrefab;
    //The prefab of generating the obstacles
    public GameObject obstacleGenerationPrefab;

    // Start is called before the first frame update. Calls the MazeGeneration and the CreateAllWalls method.
    void Start()
    {
        CountdownController.GameStarted = false;
        //Initializes the NumberOfButtons and the ScaleMazeSize
        if (gameObject.scene.name.Equals("LevelGameScene"))
        {
            //Level game modus
            CurrentLevelCount = 0;
            GarbageCollectorGameObjects = new List<GameObject>();
            NumberOfButtons = 0;
            ScaleMazeSize = 1;
            IsBattleGameMode = false;          
            InitializeGame();
        }
        else if(gameObject.scene.name.Equals("GameScene"))
        {
            //Normal game modus
            CurrentLevelCount = -1;
            ApplyDifficulty();
            IsBattleGameMode = false;
            InitializeGame();
        }
        else if (gameObject.scene.name.Equals("BattleGameScene"))
        {
            CurrentLevelCount = -1;
            NumberOfButtons = 4;
            ScaleMazeSize = 0.5f;
            IsBattleGameMode = true;
            InitializeGame();
        }
    }

    /**
     * <summary>Initalizes the game. Creates the maze and adds the obstacles.</summary>
     */
    public void InitializeGame()
    {
        EnableUserInput = false;
        //Initializes the static variables of the game.
        CurrentState = 0;
        CurrentStepCount = 0;
        UpdateStepCounter();
        AllNodes = new Dictionary<int, NodeController>();
        AllEdges = new List<EdgeController>();
        AllButtons = new List<ButtonController>();

        //Set all possible colors (at least as many as NumberOfObstacles)
        Colors = new List<Color>
        {
            new Color(0, 1, 0),
            new Color(0, 0, 1),
            new Color(1, 0, 0),
            new Color(1, 1, 0)
        };

        //Initializes the number of states.
        NumberOfStates = (int)Math.Pow(2, NumberOfButtons);

        //Moves the player to the start point.
        if (!IsBattleGameMode)
        {
            GameObject.Find("Ruby").GetComponent<RubyController>().SetPositionAndScale();
        }
        

        //Generates the labyrinth
        GameObject gameObject = Instantiate(aldousBroderAlgorithmPrefab);
        AldousBroderAlgorithm a = gameObject.GetComponent<AldousBroderAlgorithm>();
        a.Initialize((int)Math.Floor(1 / ScaleMazeSize * 18), (int)Math.Floor(1 / ScaleMazeSize * 10));
        if (CurrentLevelCount != -1) GarbageCollectorGameObjects.Add(gameObject);

        //Generates all obstacles
        gameObject = Instantiate(obstacleGenerationPrefab);
        ObstacleGeneration obstacleGeneration = gameObject.GetComponent<ObstacleGeneration>();
        obstacleGeneration.InsertObstacles();
        if (CurrentLevelCount != -1) GarbageCollectorGameObjects.Add(gameObject);


        if (!IsBattleGameMode)
        {
            //Calculates the optimal path and distance.
            gameObject = Instantiate(modifiedDijkstraAlgorithmPrefab);
            ModifiedDijkstraAlgorithm dijkstra = gameObject.GetComponent<ModifiedDijkstraAlgorithm>();
            if (ScaleMazeSize == 0.5f)
            {
                dijkstra.Initialize(AllNodes[0], AllNodes[719]);
            }
            else
            {
                dijkstra.Initialize(AllNodes[0], AllNodes[179]);
            }
            dijkstra.CalculateModifiedDijkstraAlgorithm();
            GameObject stepCounterText = GameObject.Find("OptimalSteps");
            stepCounterText.GetComponent<TextMeshProUGUI>().text = "Optimal: " + dijkstra.ShortestDistance;
            OptimalStepCount = dijkstra.ShortestDistance;
            ShortestPath = dijkstra.ShortestPath;

            if (CurrentLevelCount != -1) GarbageCollectorGameObjects.Add(gameObject);
        }

        //Creates all walls of the maze.
        GameObject createWallsObject = Instantiate(createWallsPrefab);
        CreateWalls createWallsScript = createWallsObject.GetComponent<CreateWalls>();
        createWallsScript.CreateAllWalls();
        if (CurrentLevelCount != -1) GarbageCollectorGameObjects.Add(createWallsObject);

        if (IsBattleGameMode)
        {
            GameObject.Find("Opponent").GetComponent<OpponentController>().InitializeOpponent();
        }
        //Enables the user input.
        EnableUserInput = true;
    }

    /**
     * <summary>Loads the next level of the level game modus.</summary>
     */
    public void LoadNextLevel()
    {
        //Increases the level count.
        CurrentLevelCount++;
        GameObject levelCounterText = GameObject.Find("LevelCounter");
        levelCounterText.GetComponent<TextMeshProUGUI>().text = "Level: " + (CurrentLevelCount + 1);

        //Destroys all gameobjects of the previous level.
        foreach (GameObject gameObject in GarbageCollectorGameObjects)
        {
            Destroy(gameObject);
        }

        //Sets the new number of buttons and the scale of the maze.
        if(CurrentLevelCount < 4)
        {
            NumberOfButtons = CurrentLevelCount;
            ScaleMazeSize = 1;
            InitializeGame();
        } else
        {
            NumberOfButtons = CurrentLevelCount - 3;
            ScaleMazeSize = 0.5f;
            InitializeGame();
        }
        
    }

    /**
     * Updates the step counter UI-Element.
    */
    public static void UpdateStepCounter()
    {
        //Finds the game object.
        GameObject stepCounterText = GameObject.Find("StepCounter");
        if (IsBattleGameMode)
        {
            stepCounterText.GetComponent<TextMeshProUGUI>().text = "Your Steps: " + CurrentStepCount;
        }
        else {
            stepCounterText.GetComponent<TextMeshProUGUI>().text = "Steps: " + CurrentStepCount;
        }
            
    }

    /**
     * Adjusts scale and number of buttons of the maze to the difficulty selected in main menu.
     */
    public void ApplyDifficulty()
    {
        switch (SliderText.DifficultyValue)
        {
            case 1:
                ScaleMazeSize = 1f;
                NumberOfButtons = 0;
                break;
            case 2:
                ScaleMazeSize = 1f;
                NumberOfButtons = 1;
                break;
            case 3:
                ScaleMazeSize = 1f;
                NumberOfButtons = 2;
                break;
            case 4:
                ScaleMazeSize = 1f;
                NumberOfButtons = 3;
                break;
            case 5:
                ScaleMazeSize = 0.5f;
                NumberOfButtons = 1;
                break;
            case 6:
                ScaleMazeSize = 0.5f;
                NumberOfButtons = 2;
                break;
            case 7:
                ScaleMazeSize = 0.5f;
                NumberOfButtons = 3;
                break;
            case 8:
                ScaleMazeSize = 0.5f;
                NumberOfButtons = 4;
                break;
            default:
                break;
        }
    }
}

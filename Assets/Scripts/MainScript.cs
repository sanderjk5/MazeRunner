using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;

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
    //The colors of the obstacles and corresponding buttons.
    public static List<Color> Colors { get; set; }
    //The current state of the game.
    public static int CurrentState { get; set; }
    //The current number of steps of the player.
    public static int CurrentStepCount { get; set; }
    public static float ScaleMazeSize { get; set; }
    public static int CurrentLevelCount { get; set; }
    public static List<GameObject> GarbageCollectorGameObjects { get; set; }
    public static int OptimalStepCount { get; set; }
    public static bool enableUserInput { get; set; }

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
        //LoadMaze();
        //if (SliderText.DifficultyValue == 0) return;
        enableUserInput = false;
        if (gameObject.scene.name.Equals("LevelGameScene"))
        {
            CurrentLevelCount = 0;
            GarbageCollectorGameObjects = new List<GameObject>();
            NumberOfButtons = 0;
            ScaleMazeSize = 1;
            InitializeGame();
        }
        else
        {
            CurrentLevelCount = -1;
            ApplyDifficulty();
            InitializeGame();
        }
    }

    public void InitializeGame()
    {
        //Initializes the static variables of the game.
        CurrentState = 0;
        CurrentStepCount = 0;
        AllNodes = new Dictionary<int, NodeController>();
        AllEdges = new List<EdgeController>();
        //Set all possible colors (at least as many as NumberOfObstacles)
        Colors = new List<Color>
        {
            new Color(0, 1, 0),
            new Color(0, 0, 1),
            new Color(1, 0, 0),
            new Color(1, 1, 0)
        };

        //Initializes number of obstacles/buttons and the scale of the maze.
        NumberOfStates = (int)Math.Pow(2, NumberOfButtons);
        GameObject.Find("Ruby").GetComponent<RubyController>().SetPositionAndScale();

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

        // Dijkstra test
        gameObject = Instantiate(modifiedDijkstraAlgorithmPrefab);
        ModifiedDijkstraAlgorithm dijkstra1 = gameObject.GetComponent<ModifiedDijkstraAlgorithm>();
        if (ScaleMazeSize == 0.5f)
        {
            dijkstra1.Initialize(AllNodes[0], AllNodes[719]);
        }
        else
        {
            dijkstra1.Initialize(AllNodes[0], AllNodes[179]);
        }
        dijkstra1.CalculateModifiedDijkstraAlgorithm();
        GameObject stepCounterText = GameObject.Find("OptimalSteps");
        stepCounterText.GetComponent<UnityEngine.UI.Text>().text = "Optimal : " + dijkstra1.ShortestDistance;
        MainScript.OptimalStepCount = dijkstra1.ShortestDistance;
        Debug.Log("Distance after inserting obstacles: " + dijkstra1.ShortestDistance);
        if (CurrentLevelCount != -1) GarbageCollectorGameObjects.Add(gameObject);

        //Creates all walls of the maze.
        GameObject createWallsObject = Instantiate(createWallsPrefab);
        CreateWalls createWallsScript = createWallsObject.GetComponent<CreateWalls>();
        createWallsScript.CreateAllWalls();
        if (CurrentLevelCount != -1) GarbageCollectorGameObjects.Add(createWallsObject);
        enableUserInput = true;
    }

    public void LoadNextLevel()
    {
        CurrentLevelCount++;
        GameObject levelCounterText = GameObject.Find("LevelCounter");
        levelCounterText.GetComponent<UnityEngine.UI.Text>().text = "Level : " + CurrentLevelCount;
        foreach (GameObject gameObject in GarbageCollectorGameObjects)
        {
            Destroy(gameObject);
        }
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
        stepCounterText.GetComponent<UnityEngine.UI.Text>().text = "Steps : " + CurrentStepCount;
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
               Debug.Log("No Valid Difficulty Found");
                break;
        }
    }
}

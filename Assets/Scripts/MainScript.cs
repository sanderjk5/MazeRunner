using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        LoadMaze();
        //Initializes the static variables of the game.
        CurrentState = 0;
        CurrentStepCount = 0;
        AllNodes = new Dictionary<int, NodeController>();
        AllEdges = new List<EdgeController>();
        //Set all possible colors (at least as many as NumberOfObstacles)
        Colors = new List<Color>
        {
            new Color(0, 255, 0),
            new Color(0, 0, 255),
            new Color(255, 0, 0),
        };

        //Generates the labyrinth
        AldousBroderAlgorithm a = Instantiate(aldousBroderAlgorithmPrefab).GetComponent<AldousBroderAlgorithm>();
        a.Initialize(18, 10, 3);

        // Dijkstra test
        ModifiedDijkstraAlgorithm dijkstra = Instantiate(modifiedDijkstraAlgorithmPrefab).GetComponent<ModifiedDijkstraAlgorithm>();
        dijkstra.Initialize(AllNodes[0], AllNodes[179]);
        dijkstra.CalculateModifiedDijkstraAlgorithm();
        Debug.Log("Distance before inserting obstacles: " + dijkstra.ShortestDistance);

        //Generates all obstacles
        ObstacleGeneration obstacleGeneration = Instantiate(obstacleGenerationPrefab).GetComponent<ObstacleGeneration>();
        obstacleGeneration.InsertObstacles(3);

        // Dijkstra test
        ModifiedDijkstraAlgorithm dijkstra1 = Instantiate(modifiedDijkstraAlgorithmPrefab).GetComponent<ModifiedDijkstraAlgorithm>();
        dijkstra1.Initialize(AllNodes[0], AllNodes[179]);
        dijkstra1.CalculateModifiedDijkstraAlgorithm();
        GameObject stepCounterText = GameObject.Find("OptimalSteps");
        stepCounterText.GetComponent<UnityEngine.UI.Text>().text = "Optimal : " + dijkstra1.ShortestDistance;
        Debug.Log("Distance after inserting obstacles: " + dijkstra1.ShortestDistance);

        //Creates all walls of the maze.
        GameObject createWallsObject = Instantiate(createWallsPrefab);
        CreateWalls createWallsScript = createWallsObject.GetComponent<CreateWalls>();
        createWallsScript.CreateAllWalls();
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

    public void LoadMaze()
    {
        if(SliderText.difficultyValue != 1)
        {
            Debug.Log("Application Quit");
            Application.Quit();
        }
    }
}

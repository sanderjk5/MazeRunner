using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static List<Color> Colors { get; set; }
    public static int CurrentState { get; set; }

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
        
        //Initializes the static variables of the game.
        AllNodes = new Dictionary<int, NodeController>();
        AllEdges = new List<EdgeController>();
        Colors = new List<Color>
        {
            new Color(0, 255, 0),
            new Color(0, 0, 255),
            new Color(255, 0, 0),
        };

        AldousBroderAlgorithm a = Instantiate(aldousBroderAlgorithmPrefab).GetComponent<AldousBroderAlgorithm>();
        a.Initialize(18, 10, 3);

        // Dijkstra test
        ModifiedDijkstraAlgorithm dijkstra = Instantiate(modifiedDijkstraAlgorithmPrefab).GetComponent<ModifiedDijkstraAlgorithm>();
        dijkstra.Initialize(AllNodes[0], AllNodes[179]);
        dijkstra.CalculateModifiedDijkstraAlgorithm();
        Debug.Log("Distance before inserting obstacles: " + dijkstra.ShortestDistance);

        ObstacleGeneration obstacleGeneration = Instantiate(obstacleGenerationPrefab).GetComponent<ObstacleGeneration>();
        obstacleGeneration.InsertObstacle(3);

        // Dijkstra test
        ModifiedDijkstraAlgorithm dijkstra1 = Instantiate(modifiedDijkstraAlgorithmPrefab).GetComponent<ModifiedDijkstraAlgorithm>();
        dijkstra1.Initialize(AllNodes[0], AllNodes[179]);
        dijkstra1.CalculateModifiedDijkstraAlgorithm();
        Debug.Log("Distance after inserting obstacles: " + dijkstra1.ShortestDistance);

        //Creates all walls of the maze.
        GameObject createWallsObject = Instantiate(createWallsPrefab);
        CreateWalls createWallsScript = createWallsObject.GetComponent<CreateWalls>();
        createWallsScript.CreateAllWalls();
    }
}

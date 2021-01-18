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


    // Start is called before the first frame update. Calls the MazeGeneration and the CreateAllWalls method.
    void Start()
    {
        
        //Initializes the static variables of the game.
        NumberOfNodes = 0;
        NumberOfEdges = 0;
        NumberOfStates = 0;
        AllNodes = new Dictionary<int, NodeController>();
        AllEdges = new List<EdgeController>();
        Colors = new List<Color>
        {
            new Color(255, 0, 0),
            new Color(0, 255, 0),
            new Color(0, 0, 255)
        };

        AldousBroderAlgorithm a = Instantiate(aldousBroderAlgorithmPrefab).GetComponent<AldousBroderAlgorithm>();
        a.Initialize(18, 10, 0);
        // this.CreateFakeData();
        //CreateExampleMaze();

        // Dijkstra test
        ModifiedDijkstraAlgorithm dijkstra = Instantiate(modifiedDijkstraAlgorithmPrefab).GetComponent<ModifiedDijkstraAlgorithm>();
        dijkstra.Initialize(AllNodes[18], AllNodes[58]);
        dijkstra.CalculateModifiedDijkstraAlgorithm();

        //this.CreateFakeData();
        //Creates all walls of the maze.
        GameObject createWallsObject = Instantiate(createWallsPrefab);
        CreateWalls createWallsScript = createWallsObject.GetComponent<CreateWalls>();
        createWallsScript.CreateAllWalls();
    }

    /**
     * An example of creating a small maze.
     */
    private void CreateFakeData()
    {
        //Sets the variables.
        NumberOfNodes = 4;
        NumberOfEdges = 3;
        NumberOfStates = 1;
        NumberOfButtons = 0;
        Width = 2;
        Height = 2;

        //Creates all nodes.
        NodeController node0 = Instantiate(nodePrefab, new Vector3(-0.5f, 0.5f, -1), Quaternion.identity).GetComponent<NodeController>();
        node0.Initialize(0, null, -1);
        AllNodes.Add(0, node0);

        NodeController node1 = Instantiate(nodePrefab, new Vector3(-0.5f, -0.5f, -1), Quaternion.identity).GetComponent<NodeController>();
        node1.Initialize(1, null, -1);
        AllNodes.Add(1, node1);

        NodeController node2 = Instantiate(nodePrefab, new Vector3(0.5f, 0.5f, -1), Quaternion.identity).GetComponent<NodeController>();
        node2.Initialize(2, null, -1);
        AllNodes.Add(2, node2);

        NodeController node3 = Instantiate(nodePrefab, new Vector3(0.5f, -0.5f, -1), Quaternion.identity).GetComponent<NodeController>();
        node3.Initialize(3, null, -1);
        AllNodes.Add(3, node3);

        //Creates all edges.
        EdgeController edge0 = Instantiate(edgePrefab, new Vector3(0, 0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge0.Initialize(node0, node2, null, -1);
        node0.OutgoingEdges.Add(edge0);
        node2.OutgoingEdges.Add(edge0);
        AllEdges.Add(edge0);

        EdgeController edge1 = Instantiate(edgePrefab, new Vector3(0.5f, 0, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge1.Initialize(node2, node3, null, -1);
        node2.OutgoingEdges.Add(edge1);
        node3.OutgoingEdges.Add(edge1);
        AllEdges.Add(edge1);

        EdgeController edge2 = Instantiate(edgePrefab, new Vector3(0, -0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge2.Initialize(node1, node3, null, -1);
        node1.OutgoingEdges.Add(edge2);
        node3.OutgoingEdges.Add(edge2);
        AllEdges.Add(edge2);
    }

    private void CreateExampleMaze()
    {
        // Set the variables
        NumberOfNodes = 60;
        NumberOfEdges = 118;
        NumberOfStates = 2;
        NumberOfButtons = 1;
        Width = 10;
        Height = 6;

        float x = -4.5f;
        int counter = 0;
        for (int i = 0; i < Width; i++)
        {
            float y = 2.5f;
            for (int j = 0; j < Height; j++)
            {
                int[] states = new int[2] { 0, 1 };
                int button = -1;
                if (i == 7 && j == 0)
                {
                    states = new int[2] { 1, 0 };
                    button = 0;
                }
                NodeController node = Instantiate(nodePrefab, new Vector3(x, y, -1), Quaternion.identity).GetComponent<NodeController>();
                node.Initialize(counter, states, button);
                AllNodes.Add(counter, node);
                counter++;
                y--;
            }
            x++;
        }

        

        // Create all edges
        EdgeController edge0 = Instantiate(edgePrefab, new Vector3(-4.5f, 2f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge0.Initialize(AllNodes[0], AllNodes[1], null, -1);
        AllNodes[0].OutgoingEdges.Add(edge0);
        AllNodes[1].OutgoingEdges.Add(edge0);
        AllEdges.Add(edge0);

        EdgeController edge01 = Instantiate(edgePrefab, new Vector3(-4.5f, 1f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge01.Initialize(AllNodes[1], AllNodes[2], null, -1);
        AllNodes[1].OutgoingEdges.Add(edge01);
        AllNodes[2].OutgoingEdges.Add(edge01);
        AllEdges.Add(edge01);

        EdgeController edge02 = Instantiate(edgePrefab, new Vector3(-4.5f, 0f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge02.Initialize(AllNodes[2], AllNodes[3], null, -1);
        AllNodes[2].OutgoingEdges.Add(edge02);
        AllNodes[3].OutgoingEdges.Add(edge02);
        AllEdges.Add(edge02);

        EdgeController edge03 = Instantiate(edgePrefab, new Vector3(-4f, 0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge03.Initialize(AllNodes[2], AllNodes[8], null, -1);
        AllNodes[2].OutgoingEdges.Add(edge03);
        AllNodes[8].OutgoingEdges.Add(edge03);
        AllEdges.Add(edge03);

        // Obstacle, prefab wrong
        EdgeController edge04 = Instantiate(edgePrefab, new Vector3(-4f, -0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge04.Initialize(AllNodes[3], AllNodes[9], new int[2] { 10, 1 }, 0);
        AllNodes[3].OutgoingEdges.Add(edge04);
        AllNodes[9].OutgoingEdges.Add(edge04);
        AllEdges.Add(edge04);
        edge04.ChangeColorOfObstacle(0);
        Instantiate(buttonPrefab, new Vector3(2.5f, 2.5f, 0), Quaternion.identity).GetComponent<ButtonController>().Initialize(edge04, MainScript.AllNodes[42], 0);

        EdgeController edge05 = Instantiate(edgePrefab, new Vector3(-4.5f, 2f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge05.Initialize(AllNodes[4], AllNodes[5], null, -1);
        AllNodes[4].OutgoingEdges.Add(edge05);
        AllNodes[5].OutgoingEdges.Add(edge05);
        AllEdges.Add(edge05);

        EdgeController edge06 = Instantiate(edgePrefab, new Vector3(-4f, -1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge06.Initialize(AllNodes[4], AllNodes[10], null, -1);
        AllNodes[4].OutgoingEdges.Add(edge06);
        AllNodes[10].OutgoingEdges.Add(edge06);
        AllEdges.Add(edge06);

        EdgeController edge07 = Instantiate(edgePrefab, new Vector3(-4f, -2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge07.Initialize(AllNodes[5], AllNodes[11], null, -1);
        AllNodes[5].OutgoingEdges.Add(edge07);
        AllNodes[11].OutgoingEdges.Add(edge07);
        AllEdges.Add(edge07);

        EdgeController edge08 = Instantiate(edgePrefab, new Vector3(-3.5f, 2f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge08.Initialize(AllNodes[6], AllNodes[7], null, -1);
        AllNodes[6].OutgoingEdges.Add(edge08);
        AllNodes[7].OutgoingEdges.Add(edge08);
        AllEdges.Add(edge08);

        EdgeController edge09 = Instantiate(edgePrefab, new Vector3(-3f, 2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge09.Initialize(AllNodes[6], AllNodes[12], null, -1);
        AllNodes[6].OutgoingEdges.Add(edge09);
        AllNodes[12].OutgoingEdges.Add(edge09);
        AllEdges.Add(edge09);

        EdgeController edge10 = Instantiate(edgePrefab, new Vector3(-3f, 1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge10.Initialize(AllNodes[7], AllNodes[13], null, -1);
        AllNodes[7].OutgoingEdges.Add(edge10);
        AllNodes[13].OutgoingEdges.Add(edge10);
        AllEdges.Add(edge10);

        EdgeController edge11 = Instantiate(edgePrefab, new Vector3(-3f, 0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge11.Initialize(AllNodes[8], AllNodes[14], null, -1);
        AllNodes[8].OutgoingEdges.Add(edge11);
        AllNodes[14].OutgoingEdges.Add(edge11);
        AllEdges.Add(edge11);

        EdgeController edge12 = Instantiate(edgePrefab, new Vector3(-3.5f, -1f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge12.Initialize(AllNodes[9], AllNodes[10], null, -1);
        AllNodes[9].OutgoingEdges.Add(edge12);
        AllNodes[10].OutgoingEdges.Add(edge12);
        AllEdges.Add(edge12);

        EdgeController edge13 = Instantiate(edgePrefab, new Vector3(-3f, -2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge13.Initialize(AllNodes[11], AllNodes[17], null, -1);
        AllNodes[11].OutgoingEdges.Add(edge13);
        AllNodes[17].OutgoingEdges.Add(edge13);
        AllEdges.Add(edge13);

        EdgeController edge14 = Instantiate(edgePrefab, new Vector3(-2f, 2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge14.Initialize(AllNodes[12], AllNodes[18], null, -1);
        AllNodes[12].OutgoingEdges.Add(edge14);
        AllNodes[18].OutgoingEdges.Add(edge14);
        AllEdges.Add(edge14);

        EdgeController edge15 = Instantiate(edgePrefab, new Vector3(-2f, 1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge15.Initialize(AllNodes[13], AllNodes[19], null, -1);
        AllNodes[13].OutgoingEdges.Add(edge15);
        AllNodes[19].OutgoingEdges.Add(edge15);
        AllEdges.Add(edge15);

        EdgeController edge16 = Instantiate(edgePrefab, new Vector3(-2.5f, 0f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge16.Initialize(AllNodes[14], AllNodes[15], null, -1);
        AllNodes[14].OutgoingEdges.Add(edge16);
        AllNodes[15].OutgoingEdges.Add(edge16);
        AllEdges.Add(edge16);

        EdgeController edge17 = Instantiate(edgePrefab, new Vector3(-2f, 0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge17.Initialize(AllNodes[14], AllNodes[20], null, -1);
        AllNodes[14].OutgoingEdges.Add(edge17);
        AllNodes[20].OutgoingEdges.Add(edge17);
        AllEdges.Add(edge17);

        EdgeController edge18 = Instantiate(edgePrefab, new Vector3(-2.5f, -1f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge18.Initialize(AllNodes[15], AllNodes[16], null, -1);
        AllNodes[15].OutgoingEdges.Add(edge18);
        AllNodes[16].OutgoingEdges.Add(edge18);
        AllEdges.Add(edge18);

        EdgeController edge19 = Instantiate(edgePrefab, new Vector3(-2f, -1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge19.Initialize(AllNodes[16], AllNodes[22], null, -1);
        AllNodes[16].OutgoingEdges.Add(edge19);
        AllNodes[22].OutgoingEdges.Add(edge19);
        AllEdges.Add(edge19);

        EdgeController edge20 = Instantiate(edgePrefab, new Vector3(-2f, -2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge20.Initialize(AllNodes[17], AllNodes[23], null, -1);
        AllNodes[17].OutgoingEdges.Add(edge20);
        AllNodes[23].OutgoingEdges.Add(edge20);
        AllEdges.Add(edge20);

        EdgeController edge21 = Instantiate(edgePrefab, new Vector3(-1f, 1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge21.Initialize(AllNodes[19], AllNodes[25], null, -1);
        AllNodes[19].OutgoingEdges.Add(edge21);
        AllNodes[25].OutgoingEdges.Add(edge21);
        AllEdges.Add(edge21);

        EdgeController edge22 = Instantiate(edgePrefab, new Vector3(-1f, 0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge22.Initialize(AllNodes[20], AllNodes[26], null, -1);
        AllNodes[20].OutgoingEdges.Add(edge22);
        AllNodes[26].OutgoingEdges.Add(edge22);
        AllEdges.Add(edge22);

        EdgeController edge23 = Instantiate(edgePrefab, new Vector3(-1f, -0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge23.Initialize(AllNodes[21], AllNodes[27], null, -1);
        AllNodes[21].OutgoingEdges.Add(edge23);
        AllNodes[27].OutgoingEdges.Add(edge23);
        AllEdges.Add(edge23);

        EdgeController edge24 = Instantiate(edgePrefab, new Vector3(-1f, -1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge24.Initialize(AllNodes[22], AllNodes[28], null, -1);
        AllNodes[22].OutgoingEdges.Add(edge24);
        AllNodes[28].OutgoingEdges.Add(edge24);
        AllEdges.Add(edge24);

        EdgeController edge25 = Instantiate(edgePrefab, new Vector3(-1f, -2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge25.Initialize(AllNodes[23], AllNodes[29], null, -1);
        AllNodes[23].OutgoingEdges.Add(edge25);
        AllNodes[29].OutgoingEdges.Add(edge25);
        AllEdges.Add(edge25);

        EdgeController edge26 = Instantiate(edgePrefab, new Vector3(0f, 2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge26.Initialize(AllNodes[24], AllNodes[30], null, -1);
        AllNodes[24].OutgoingEdges.Add(edge26);
        AllNodes[30].OutgoingEdges.Add(edge26);
        AllEdges.Add(edge26);

        EdgeController edge27 = Instantiate(edgePrefab, new Vector3(0f, 1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge27.Initialize(AllNodes[25], AllNodes[31], null, -1);
        AllNodes[25].OutgoingEdges.Add(edge27);
        AllNodes[31].OutgoingEdges.Add(edge27);
        AllEdges.Add(edge27);

        EdgeController edge28 = Instantiate(edgePrefab, new Vector3(-0.5f, 0f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge28.Initialize(AllNodes[26], AllNodes[27], null, -1);
        AllNodes[26].OutgoingEdges.Add(edge28);
        AllNodes[27].OutgoingEdges.Add(edge28);
        AllEdges.Add(edge28);

        EdgeController edge29 = Instantiate(edgePrefab, new Vector3(0f, -1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge29.Initialize(AllNodes[28], AllNodes[34], null, -1);
        AllNodes[28].OutgoingEdges.Add(edge29);
        AllNodes[34].OutgoingEdges.Add(edge29);
        AllEdges.Add(edge29);

        EdgeController edge30 = Instantiate(edgePrefab, new Vector3(0f, -2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge30.Initialize(AllNodes[29], AllNodes[35], null, -1);
        AllNodes[29].OutgoingEdges.Add(edge30);
        AllNodes[35].OutgoingEdges.Add(edge30);
        AllEdges.Add(edge30);

        EdgeController edge31 = Instantiate(edgePrefab, new Vector3(0.5f, 2f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge31.Initialize(AllNodes[30], AllNodes[31], null, -1);
        AllNodes[30].OutgoingEdges.Add(edge31);
        AllNodes[31].OutgoingEdges.Add(edge31);
        AllEdges.Add(edge31);

        EdgeController edge32 = Instantiate(edgePrefab, new Vector3(1f, 2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge32.Initialize(AllNodes[30], AllNodes[36], null, -1);
        AllNodes[30].OutgoingEdges.Add(edge32);
        AllNodes[36].OutgoingEdges.Add(edge32);
        AllEdges.Add(edge32);

        EdgeController edge33 = Instantiate(edgePrefab, new Vector3(0.5f, 0f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge33.Initialize(AllNodes[32], AllNodes[33], null, -1);
        AllNodes[32].OutgoingEdges.Add(edge33);
        AllNodes[33].OutgoingEdges.Add(edge33);
        AllEdges.Add(edge33);

        EdgeController edge34 = Instantiate(edgePrefab, new Vector3(1f, 0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge34.Initialize(AllNodes[32], AllNodes[38], null, -1);
        AllNodes[32].OutgoingEdges.Add(edge34);
        AllNodes[38].OutgoingEdges.Add(edge34);
        AllEdges.Add(edge34);

        EdgeController edge35 = Instantiate(edgePrefab, new Vector3(1f, -0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge35.Initialize(AllNodes[33], AllNodes[39], null, -1);
        AllNodes[33].OutgoingEdges.Add(edge35);
        AllNodes[39].OutgoingEdges.Add(edge35);
        AllEdges.Add(edge35);

        EdgeController edge36 = Instantiate(edgePrefab, new Vector3(1f, -1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge36.Initialize(AllNodes[34], AllNodes[40], null, -1);
        AllNodes[34].OutgoingEdges.Add(edge36);
        AllNodes[40].OutgoingEdges.Add(edge36);
        AllEdges.Add(edge36);

        EdgeController edge37 = Instantiate(edgePrefab, new Vector3(1f, -2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge37.Initialize(AllNodes[35], AllNodes[41], null, -1);
        AllNodes[35].OutgoingEdges.Add(edge37);
        AllNodes[41].OutgoingEdges.Add(edge37);
        AllEdges.Add(edge37);

        EdgeController edge38 = Instantiate(edgePrefab, new Vector3(1.5f, 2f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge38.Initialize(AllNodes[36], AllNodes[37], null, -1);
        AllNodes[36].OutgoingEdges.Add(edge38);
        AllNodes[37].OutgoingEdges.Add(edge38);
        AllEdges.Add(edge38);

        EdgeController edge39 = Instantiate(edgePrefab, new Vector3(2f, 1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge39.Initialize(AllNodes[37], AllNodes[43], null, -1);
        AllNodes[37].OutgoingEdges.Add(edge39);
        AllNodes[43].OutgoingEdges.Add(edge39);
        AllEdges.Add(edge39);

        EdgeController edge40 = Instantiate(edgePrefab, new Vector3(2f, 0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge40.Initialize(AllNodes[38], AllNodes[44], null, -1);
        AllNodes[38].OutgoingEdges.Add(edge40);
        AllNodes[44].OutgoingEdges.Add(edge40);
        AllEdges.Add(edge40);

        EdgeController edge41 = Instantiate(edgePrefab, new Vector3(1.5f, -1f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge41.Initialize(AllNodes[39], AllNodes[40], null, -1);
        AllNodes[39].OutgoingEdges.Add(edge41);
        AllNodes[40].OutgoingEdges.Add(edge41);
        AllEdges.Add(edge41);

        EdgeController edge42 = Instantiate(edgePrefab, new Vector3(2f, -2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge42.Initialize(AllNodes[41], AllNodes[47], null, -1);
        AllNodes[41].OutgoingEdges.Add(edge42);
        AllNodes[47].OutgoingEdges.Add(edge42);
        AllEdges.Add(edge42);

        EdgeController edge43 = Instantiate(edgePrefab, new Vector3(3f, 2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge43.Initialize(AllNodes[42], AllNodes[48], null, -1);
        AllNodes[42].OutgoingEdges.Add(edge43);
        AllNodes[48].OutgoingEdges.Add(edge43);
        AllEdges.Add(edge43);

        EdgeController edge44 = Instantiate(edgePrefab, new Vector3(3f, 1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge44.Initialize(AllNodes[43], AllNodes[49], null, -1);
        AllNodes[43].OutgoingEdges.Add(edge44);
        AllNodes[49].OutgoingEdges.Add(edge44);
        AllEdges.Add(edge44);

        EdgeController edge45 = Instantiate(edgePrefab, new Vector3(3f, 0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge45.Initialize(AllNodes[44], AllNodes[50], null, -1);
        AllNodes[44].OutgoingEdges.Add(edge45);
        AllNodes[50].OutgoingEdges.Add(edge45);
        AllEdges.Add(edge45);

        EdgeController edge46 = Instantiate(edgePrefab, new Vector3(2.5f, -1f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge46.Initialize(AllNodes[45], AllNodes[46], null, -1);
        AllNodes[45].OutgoingEdges.Add(edge46);
        AllNodes[46].OutgoingEdges.Add(edge46);
        AllEdges.Add(edge46);

        EdgeController edge47 = Instantiate(edgePrefab, new Vector3(3f, -1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge47.Initialize(AllNodes[46], AllNodes[52], null, -1);
        AllNodes[46].OutgoingEdges.Add(edge47);
        AllNodes[52].OutgoingEdges.Add(edge47);
        AllEdges.Add(edge47);

        EdgeController edge48 = Instantiate(edgePrefab, new Vector3(3f, -2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge48.Initialize(AllNodes[47], AllNodes[53], null, -1);
        AllNodes[47].OutgoingEdges.Add(edge48);
        AllNodes[53].OutgoingEdges.Add(edge48);
        AllEdges.Add(edge48);

        EdgeController edge49 = Instantiate(edgePrefab, new Vector3(4f, 2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge49.Initialize(AllNodes[48], AllNodes[54], null, -1);
        AllNodes[48].OutgoingEdges.Add(edge49);
        AllNodes[54].OutgoingEdges.Add(edge49);
        AllEdges.Add(edge49);

        EdgeController edge50 = Instantiate(edgePrefab, new Vector3(4f, 1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge50.Initialize(AllNodes[49], AllNodes[55], null, -1);
        AllNodes[49].OutgoingEdges.Add(edge50);
        AllNodes[55].OutgoingEdges.Add(edge50);
        AllEdges.Add(edge50);

        EdgeController edge51 = Instantiate(edgePrefab, new Vector3(3.5f, 0f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge51.Initialize(AllNodes[50], AllNodes[51], null, -1);
        AllNodes[50].OutgoingEdges.Add(edge51);
        AllNodes[51].OutgoingEdges.Add(edge51);
        AllEdges.Add(edge51);

        EdgeController edge52 = Instantiate(edgePrefab, new Vector3(4f, -0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge52.Initialize(AllNodes[51], AllNodes[57], null, -1);
        AllNodes[51].OutgoingEdges.Add(edge52);
        AllNodes[57].OutgoingEdges.Add(edge52);
        AllEdges.Add(edge52);

        EdgeController edge53 = Instantiate(edgePrefab, new Vector3(4f, -1.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge53.Initialize(AllNodes[52], AllNodes[58], null, -1);
        AllNodes[52].OutgoingEdges.Add(edge53);
        AllNodes[58].OutgoingEdges.Add(edge53);
        AllEdges.Add(edge53);

        EdgeController edge54 = Instantiate(edgePrefab, new Vector3(4f, -2.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge54.Initialize(AllNodes[53], AllNodes[59], null, -1);
        AllNodes[53].OutgoingEdges.Add(edge54);
        AllNodes[59].OutgoingEdges.Add(edge54);
        AllEdges.Add(edge54);

        EdgeController edge55 = Instantiate(edgePrefab, new Vector3(4.5f, 2f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge55.Initialize(AllNodes[54], AllNodes[55], null, -1);
        AllNodes[54].OutgoingEdges.Add(edge55);
        AllNodes[55].OutgoingEdges.Add(edge55);
        AllEdges.Add(edge55);

        EdgeController edge56 = Instantiate(edgePrefab, new Vector3(4.5f, 1f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge56.Initialize(AllNodes[55], AllNodes[56], null, -1);
        AllNodes[55].OutgoingEdges.Add(edge56);
        AllNodes[56].OutgoingEdges.Add(edge56);
        AllEdges.Add(edge56);

        EdgeController edge57 = Instantiate(edgePrefab, new Vector3(4.5f, 0f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge57.Initialize(AllNodes[56], AllNodes[57], null, -1);
        AllNodes[56].OutgoingEdges.Add(edge57);
        AllNodes[57].OutgoingEdges.Add(edge57);
        AllEdges.Add(edge57);

        EdgeController edge58 = Instantiate(edgePrefab, new Vector3(4.5f, -2f, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge58.Initialize(AllNodes[58], AllNodes[59], null, -1);
        AllNodes[58].OutgoingEdges.Add(edge58);
        AllNodes[59].OutgoingEdges.Add(edge58);
        AllEdges.Add(edge58);
    }
}

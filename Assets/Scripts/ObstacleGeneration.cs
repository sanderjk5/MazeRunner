using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleGeneration : MonoBehaviour
{
    //The prefab of the modified dijkstra algorithm.
    public GameObject modifiedDijkstraAlgorithmPrefab;
    //The prefab of the button.
    public GameObject buttonPrefab;

    /**
     * Inserts all Obstacles of the maze.
     * <param name="numberOfObstacles">The number of generated obstacles.</param>
     */
    public void InsertObstacles()
    {
        //Inserts each obstacle.
        for (int i = 0; i < MainScript.NumberOfButtons; i++)
        {
            while(true)
            {
                //Generates randomly the location of the obstacle (0: path between start and target node, 1: not the path between start and target node)
                int obstacleLocation = Random.Range(0, 2);

                //Calculates the shortest distance before adding the obstacle.
                GameObject algorithmObject = Instantiate(modifiedDijkstraAlgorithmPrefab);
                if (MainScript.CurrentLevelCount != -1) MainScript.GarbageCollectorGameObjects.Add(algorithmObject);
                ModifiedDijkstraAlgorithm algorithm = algorithmObject.GetComponent<ModifiedDijkstraAlgorithm>();
                if(obstacleLocation == 0)
                {
                    //path between start and target node
                    algorithm.Initialize(MainScript.AllNodes[0], MainScript.AllNodes[MainScript.NumberOfNodes - 1]);

                } else
                {
                    //path between the node in the upper right corner and the node in the lower left corner
                    algorithm.Initialize(MainScript.AllNodes[(MainScript.Width - 1) * MainScript.Height], MainScript.AllNodes[MainScript.Height - 1]);
                }
                algorithm.CalculateModifiedDijkstraAlgorithm();
                List<NodeController> shortestPath = algorithm.ShortestPath;

                //Gets randomly a node of the shortest path as obstacle.
                int randomNumber = Random.Range((int) Math.Floor((double)shortestPath.Count / 4), (int) (shortestPath.Count - Math.Floor((double)shortestPath.Count / 10)));
                NodeController startNode = shortestPath[randomNumber];
                NodeController targetNode = shortestPath[randomNumber + 1];

                //Sets the new values of the obstacle
                EdgeController edge = startNode.GetEdgeToNode(targetNode);
                if (edge.Obstacle != -1) continue;
                TransformEdge(edge, i);

                //Calculates the shortest distance after adding the obstacle.
                GameObject algorithmObject1 = Instantiate(modifiedDijkstraAlgorithmPrefab);
                if (MainScript.CurrentLevelCount != -1) MainScript.GarbageCollectorGameObjects.Add(algorithmObject1);
                ModifiedDijkstraAlgorithm algorithm1 = algorithmObject1.GetComponent<ModifiedDijkstraAlgorithm>();
                if (obstacleLocation == 0)
                {
                    algorithm1.Initialize(MainScript.AllNodes[0], MainScript.AllNodes[MainScript.NumberOfNodes - 1]);
                }
                else
                {
                    algorithm1.Initialize(MainScript.AllNodes[(MainScript.Width - 1) * MainScript.Height], MainScript.AllNodes[MainScript.Height - 1]);
                }
                algorithm1.CalculateModifiedDijkstraAlgorithm();
                int shortestDistanceWithObstacle = algorithm1.ShortestDistance;
                
                //Tries to insert a valid button
                if (InsertButton(shortestDistanceWithObstacle, startNode, shortestPath, i, obstacleLocation, edge))
                {
                    Destroy(algorithmObject);
                    Destroy(algorithmObject1);
                    //Changes the color of the obstacle to its initial value.
                    edge.ChangeColorOfObstacle(0);
                    break;
                } else
                {
                    //Resets the edge and tries to find another location for the obstacle.
                    Destroy(algorithmObject);
                    Destroy(algorithmObject1);
                    ResetEdge(edge);
                }
            }
        }
    }

    /**
     * Tries to insert a button for the corresponding obstacle.
     * <param name="shortestDistance">The shortest distance of the path after adding the obstacle.</param>
     * <param name="nodeAtObstacle">The node in front of the obstacle.</param>
     * <param name="optimalPathWithoutObstacle">The optimal path before adding the obstacle.</param>
     * <param name="buttonId">The id of the button.</param>
     * <param name="obstacleLocation">The location flag of the obstacle.</param>
     * <param name="obstacle">The corresponding obstacle.</param>
     * <returns>Whether the operation was successful.</returns>
     */
    private bool InsertButton(int shortestDistance, NodeController nodeAtObstacle, List<NodeController> optimalPathWithoutObstacle, int buttonId, int obstacleLocation, EdgeController obstacle)
    {
        //Counts the number of tested nodes.
        int counter = 0;
        while(true)
        {
            //Breaks if it checked too much nodes. Chooses after that another obstacle.
            if (counter == MainScript.NumberOfNodes) return false;

            //Gets a random node as button.
            NodeController node = GetRandomNode();
            //Checks the conditions of the node.
            if (optimalPathWithoutObstacle.Contains(node) || node.Button != -1) continue;
            //Sets the values of the new button.
            node.Button = buttonId;
            node.States = SetStates(buttonId);

            //Calculates the shortest distance after adding the button.
            GameObject algorithmObject = Instantiate(modifiedDijkstraAlgorithmPrefab);
            if (MainScript.CurrentLevelCount != -1) MainScript.GarbageCollectorGameObjects.Add(algorithmObject);
            ModifiedDijkstraAlgorithm algorithm = algorithmObject.GetComponent<ModifiedDijkstraAlgorithm>();
            if (obstacleLocation == 0)
            {
                algorithm.Initialize(MainScript.AllNodes[0], MainScript.AllNodes[MainScript.NumberOfNodes - 1]);
            }
            else
            {
                algorithm.Initialize(MainScript.AllNodes[(MainScript.Width - 1) * MainScript.Height], MainScript.AllNodes[MainScript.Height - 1]);
            }
            algorithm.CalculateModifiedDijkstraAlgorithm();
            int newShortestDistance = algorithm.ShortestDistance;

            //Calculates the distance between button and obstacle.
            GameObject algorithmObject1 = Instantiate(modifiedDijkstraAlgorithmPrefab);
            if (MainScript.CurrentLevelCount != -1) MainScript.GarbageCollectorGameObjects.Add(algorithmObject1);
            ModifiedDijkstraAlgorithm algorithm1 = algorithmObject1.GetComponent<ModifiedDijkstraAlgorithm>();
            algorithm1.Initialize(node, nodeAtObstacle);
            algorithm1.CalculateModifiedDijkstraAlgorithm();
            int distanceBetweenButtonAndObstacle = algorithm1.ShortestDistance;

            //Checks the conditions.
            if(newShortestDistance < shortestDistance && distanceBetweenButtonAndObstacle > (shortestDistance / 10)){
                Destroy(algorithmObject);
                Destroy(algorithmObject1);
                //Adds the button at the choosen node.
                GameObject gameObject = Instantiate(buttonPrefab, node.transform.position, Quaternion.identity);
                ButtonController button = gameObject.GetComponent<ButtonController>();
                button.Initialize(obstacle, node, buttonId);
                MainScript.AllButtons.Add(button);
                button.gameObject.transform.localScale = new Vector3(0.25f * MainScript.ScaleMazeSize, 0.25f * MainScript.ScaleMazeSize);
                if (MainScript.CurrentLevelCount != -1) MainScript.GarbageCollectorGameObjects.Add(gameObject);
                break;
            }
            //Resets the values and checks another node.
            Destroy(algorithmObject);
            Destroy(algorithmObject1);
            node.Button = -1;
            node.States = null;
            counter++;
        }
        return true;
    }

    /**
     * Gets a random node of the maze.
     * <returns>The choosen node.</returns>
     */
    private NodeController GetRandomNode()
    {
        int randomNumber = Random.Range(0, MainScript.NumberOfNodes);
        return MainScript.AllNodes[randomNumber];
    }

    /**
     * Creates the cost array of the obstacle.
     * <param name="numberOfObstacles">The number of obstacles of the maze.</param>
     * <param name="buttonId">The id of the current obstacle.</param>
     * <param name="lengthOfObstacle">The length of the obstacle if it is activated.</param>
     * <returns>The costs array.</returns>
     */
    private int[] SetCosts(int buttonId, int lengthOfObstacle)
    {
        int[] costs = new int[(int)Math.Pow(2, MainScript.NumberOfButtons)];
        int counter = 0;
        bool obstacleLength = true;
        while(counter < MainScript.NumberOfStates)
        {
            for(int i = 0; i < Math.Pow(2, buttonId); i++)
            {
                if (obstacleLength)
                {
                    costs[counter] = lengthOfObstacle;
                } else
                {
                    costs[counter] = 1;
                }
                counter += 1;
            }
            obstacleLength = !obstacleLength;
        }
        return costs;
    }

    /**
     * Creates the states array of the button.
     * <param name="numberOfObstacles">The number of obstacles of the maze.</param>
     * <param name="buttonId">The id of the current obstacle.</param>
     * <returns>The states array.</returns>
     */
    private int[] SetStates(int buttonId)
    {
        int[] states = new int[(int)Math.Pow(2, MainScript.NumberOfButtons)];
        int counter = 0;
        bool removeObstacle = true;
        while(counter != MainScript.NumberOfStates)
        {
            for(int i = 0; i < Math.Pow(2, buttonId); i++)
            {
                if (removeObstacle)
                {
                    states[counter] = counter + (int) Math.Pow(2, buttonId);
                } else
                {
                    states[counter] = counter - (int)Math.Pow(2, buttonId);
                }
                counter += 1;
            }
            removeObstacle = !removeObstacle;
        }
        return states;
    }

    /**
     * The length of an obstacle if it is activated.
     */
    private int GetLengthOfObstacle()
    {
        if(MainScript.ScaleMazeSize == 0.5f)
        {
            return 50;
        } else
        {
            return 25;
        }
        
    }

    /**
     * Transforms the edge to a obstacle.
     * <param name="edge">The edge which should be transformed.</param>
     * <param name="buttonId">The id of the corresponding button.</param>
     */
    private void TransformEdge(EdgeController edge, int buttonId)
    {
        edge.Obstacle = buttonId;
        edge.Costs = SetCosts(buttonId, GetLengthOfObstacle());
    }

    /**
     * Resets an obstacle to a normal edge.
     * <param name="edge">The edge which should be reseted.</param>
     */
    private void ResetEdge(EdgeController edge)
    {
        edge.Obstacle = -1;
        edge.Costs = null;
    }
}

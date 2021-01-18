using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleGeneration : MonoBehaviour
{
    public GameObject modifiedDijkstraAlgorithmPrefab;
    public GameObject buttonPrefab;

    public void InsertObstacle(int numberOfObstacles)
    {
        for(int i = 0; i < numberOfObstacles; i++)
        {
            while(true)
            {
                int obstacleLocation = Random.Range(0, 2);
                GameObject algorithmObject = Instantiate(modifiedDijkstraAlgorithmPrefab);
                ModifiedDijkstraAlgorithm algorithm = algorithmObject.GetComponent<ModifiedDijkstraAlgorithm>();
                if(obstacleLocation == 0)
                {
                    algorithm.Initialize(MainScript.AllNodes[0], MainScript.AllNodes[MainScript.NumberOfNodes-1]);
                } else
                {
                    algorithm.Initialize(MainScript.AllNodes[(MainScript.Width-1)*MainScript.Height], MainScript.AllNodes[MainScript.Height - 1]);
                }
                algorithm.CalculateModifiedDijkstraAlgorithm();
                List<NodeController> shortestPath = algorithm.ShortestPath;
                int randomNumber = Random.Range((int) Math.Floor((double)shortestPath.Count / 4), (int) (shortestPath.Count - Math.Floor((double)shortestPath.Count / 10)));
                NodeController startNode = shortestPath[randomNumber];
                NodeController targetNode = shortestPath[randomNumber + 1];
                EdgeController edge = startNode.GetEdgeToNode(targetNode);
                if (edge.Obstacle != -1) continue;
                transformEdge(edge, i);

                GameObject algorithmObject1 = Instantiate(modifiedDijkstraAlgorithmPrefab);
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
                
                if (insertButton(shortestDistanceWithObstacle, startNode, shortestPath, i, obstacleLocation, edge))
                {
                    Destroy(algorithmObject);
                    Destroy(algorithmObject1);
                    edge.ChangeColorOfObstacle(0);
                    break;
                } else
                {
                    Destroy(algorithmObject);
                    Destroy(algorithmObject1);
                    resetEdge(edge);
                }
            }
        }
    }

    private bool insertButton(int shortestDistance, NodeController nodeAtObstacle, List<NodeController> optimalPathWithoutObstacle, int buttonId, int obstacleLocation, EdgeController obstacle)
    {
        int counter = 0;
        while(true)
        {
            if (counter == MainScript.NumberOfNodes) return false;
            NodeController node = getRandomNode();
            if (optimalPathWithoutObstacle.Contains(node) || node.Button != -1) continue;
            node.Button = buttonId;
            node.States = setStates(MainScript.NumberOfButtons, buttonId);

            GameObject algorithmObject = Instantiate(modifiedDijkstraAlgorithmPrefab);
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

            GameObject algorithmObject1 = Instantiate(modifiedDijkstraAlgorithmPrefab);
            ModifiedDijkstraAlgorithm algorithm1 = algorithmObject1.GetComponent<ModifiedDijkstraAlgorithm>();
            algorithm1.Initialize(node, nodeAtObstacle);
            algorithm1.CalculateModifiedDijkstraAlgorithm();
            int distanceBetweenButtonAndObstacle = algorithm1.ShortestDistance;

            //TODO: Place button and change colour of edge
            if(newShortestDistance < shortestDistance && distanceBetweenButtonAndObstacle > (shortestDistance / 10)){
                Destroy(algorithmObject);
                Destroy(algorithmObject1);
                Instantiate(buttonPrefab, node.transform.position, Quaternion.identity).GetComponent<ButtonController>().Initialize(obstacle, node, buttonId);
                break;
            }
            Destroy(algorithmObject);
            Destroy(algorithmObject1);
            node.Button = -1;
            node.States = null;
            counter++;
        }
        return true;
    }

    private NodeController getRandomNode()
    {
        int randomNumber = Random.Range(0, MainScript.NumberOfNodes);
        return MainScript.AllNodes[randomNumber];
    }

    private int[] setCosts(int numberOfObstacles, int buttonId, int lengthOfObstacle)
    {
        int[] costs = new int[(int)Math.Pow(2, numberOfObstacles)];
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

    private int[] setStates(int numberOfObstacles, int buttonId)
    {
        int[] states = new int[(int)Math.Pow(2, numberOfObstacles)];
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

    private int getLengthOfObstacle()
    {
        return 25;
    }

    private void transformEdge(EdgeController edge, int buttonId)
    {
        edge.Obstacle = buttonId;
        edge.Costs = setCosts(MainScript.NumberOfButtons, buttonId, getLengthOfObstacle());
    }

    private void resetEdge(EdgeController edge)
    {
        edge.Obstacle = -1;
        edge.Costs = null;
    }
}

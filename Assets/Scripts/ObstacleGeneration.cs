using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleGeneration : MonoBehaviour
{
    public void Initialize()
    {

    }

    public void insertObstacle(int numberOfObstacles)
    {
        for(int i = 0; i < numberOfObstacles; i++)
        {
            while(true)
            {
                int obstacleLocation = Random.Range(0, 2);
                ModifiedDijkstraAlgorithm algorithm;
                if(obstacleLocation == 0)
                {
                    algorithm = new ModifiedDijkstraAlgorithm();
                } else
                {
                    algorithm = new ModifiedDijkstraAlgorithm();
                }
                //TODO: Calculate shortest path
                //TODO: Calculate random node on path
                //TODO: Get nodes and edge
                //TODO: Transform edge

                if (obstacleLocation == 0)
                {
                    algorithm = new ModifiedDijkstraAlgorithm();
                }
                else
                {
                    algorithm = new ModifiedDijkstraAlgorithm();
                }
                //TODO: Calculate new distance

                //TODO: insert button and break or reset edge
                break;
            }
        }
    }

    public bool insertButton(int shortestDistance, NodeController nodeAtObstacle, List<NodeController> optimalPathWithoutObstacle, int buttonId, int obstacleLocation)
    {
        int counter = 0;
        while(true)
        {
            if (counter == MainScript.NumberOfNodes) return false;
            NodeController node = getRandomNode();
            if (optimalPathWithoutObstacle.Contains(node) || node.Button != -1) continue;
            node.Button = buttonId;
            node.States = setStates(MainScript.NumberofButtons, buttonId);

            ModifiedDijkstraAlgorithm algorithm;
            if (obstacleLocation == 0)
            {
                algorithm = new ModifiedDijkstraAlgorithm();
            }
            else
            {
                algorithm = new ModifiedDijkstraAlgorithm();
            }
            //TODO: Get new shortest distance.

            ModifiedDijkstraAlgorithm algorithm2 = new ModifiedDijkstraAlgorithm();
            //TODO: Get distance between button and obstacle

            //TODO: If all constraints are valid break
            //TODO: Place button and change colour of edge
            break;
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
        while(counter < MainScript.NumberofStates)
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
        while(counter != MainScript.NumberofStates)
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
        edge.Costs = setCosts(MainScript.NumberofButtons, buttonId, getLengthOfObstacle());
    }

    private void resetEdge(EdgeController edge)
    {
        edge.Obstacle = -1;
        edge.Costs = null;
    }
}

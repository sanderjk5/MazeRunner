using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateWalls : MonoBehaviour
{
    //The prefab of the walls.
    public GameObject wallPrefab;

    /**
     * Creates all walls of the labyrinth.
     */
    public void CreateAllWalls()
    {
        Vector3 newScale;
        if(MainScript.ScaleMazeSize == 0.5)
        {
            newScale = new Vector3(MainScript.ScaleMazeSize, 0.025f);
        } else
        {
            newScale = new Vector3(MainScript.ScaleMazeSize, 0.03f);
        }
        
        //Upper and lower bound of the labyrinth.
        for (int i = -MainScript.Width / 2; i < MainScript.Width / 2; i++)
        {
            Instantiate(wallPrefab, new Vector3((i + 0.5f) * MainScript.ScaleMazeSize, MainScript.Height / 2 * MainScript.ScaleMazeSize, 0), Quaternion.identity).transform.localScale = newScale;
            if(i != (MainScript.Width / 2) - 1) Instantiate(wallPrefab, new Vector3((i + 0.5f) * MainScript.ScaleMazeSize, -MainScript.Height / 2 * MainScript.ScaleMazeSize, 0), Quaternion.identity).transform.localScale = newScale;
        }
        //Right and left bound of the labyrinth.
        for (int i = -MainScript.Height / 2; i < MainScript.Height / 2; i++)
        {
            Instantiate(wallPrefab, new Vector3(-MainScript.Width / 2 * MainScript.ScaleMazeSize, (i + 0.5f) * MainScript.ScaleMazeSize, 0), Quaternion.Euler(0, 0, 90)).transform.localScale = newScale;
            Instantiate(wallPrefab, new Vector3(MainScript.Width / 2 * MainScript.ScaleMazeSize, (i + 0.5f) * MainScript.ScaleMazeSize, 0), Quaternion.Euler(0, 0, 90)).transform.localScale = newScale;
        }

        //All horizontal walls.
        for (int i = -MainScript.Width / 2; i < MainScript.Width / 2; i++)
        {
            int lastNode = (i + MainScript.Width / 2) * MainScript.Height;
            int currentNode = lastNode + 1;
            for (int j = MainScript.Height / 2 - 1; j > -MainScript.Height / 2; j--)
            {
                //Checks if a edge exits between these nodes.
                if (MainScript.AllNodes[lastNode].GetEdgeToNode(MainScript.AllNodes[currentNode]) == null) Instantiate(wallPrefab, new Vector3((i + 0.5f) * MainScript.ScaleMazeSize, j * MainScript.ScaleMazeSize, 0), Quaternion.identity).transform.localScale = newScale;
                lastNode = currentNode;
                currentNode += 1;
            }
        }
        //All vertical walls.
        for (int j = MainScript.Height / 2; j > -MainScript.Height / 2; j--)
        {
            int lastNode = -j + MainScript.Height / 2;
            int currentNode = lastNode + MainScript.Height;
            for (int i = -MainScript.Width / 2 + 1; i < MainScript.Width / 2; i++)
            {
                //Checks if a edge exits between these nodes.
                if (MainScript.AllNodes[lastNode].GetEdgeToNode(MainScript.AllNodes[currentNode]) == null) Instantiate(wallPrefab, new Vector3(i * MainScript.ScaleMazeSize, (j - 0.5f) * MainScript.ScaleMazeSize, 0), Quaternion.Euler(0, 0, 90)).transform.localScale = newScale;
                lastNode = currentNode;
                currentNode += MainScript.Height;
            }
        }
    }
}

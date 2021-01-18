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
        //Upper and lower bound of the labyrinth.
        for (int i = -MainScript.Width / 2; i < MainScript.Width / 2; i++)
        {
            Instantiate(wallPrefab, new Vector3(i + 0.5f, MainScript.Height / 2, 0), Quaternion.identity);
            if(i != (MainScript.Width / 2) - 1) Instantiate(wallPrefab, new Vector3(i + 0.5f, -MainScript.Height / 2, 0), Quaternion.identity);
        }
        //Right and left bound of the labyrinth.
        for (int i = -MainScript.Height / 2; i < MainScript.Height / 2; i++)
        {
            Instantiate(wallPrefab, new Vector3(-MainScript.Width / 2, i + 0.5f, 0), Quaternion.Euler(0, 0, 90));
            Instantiate(wallPrefab, new Vector3(MainScript.Width / 2, i + 0.5f, 0), Quaternion.Euler(0, 0, 90));
        }

        //All horizontal walls.
        for (int i = -MainScript.Width / 2; i < MainScript.Width / 2; i++)
        {
            int lastNode = (i + MainScript.Width / 2) * MainScript.Height;
            int currentNode = lastNode + 1;
            for (int j = MainScript.Height / 2-1; j > -MainScript.Height / 2; j--)
            {
                //Checks if a edge exits between these nodes.
                if (MainScript.AllNodes[lastNode].GetEdgeToNode(MainScript.AllNodes[currentNode]) == null) Instantiate(wallPrefab, new Vector3(i + 0.5f, j, 0), Quaternion.identity);
                lastNode = currentNode;
                currentNode += 1;
            }
        }
        //All vertical walls.
        for (int j = MainScript.Height / 2; j > -MainScript.Height / 2; j--)
        {
            int lastNode = -j + MainScript.Height / 2;
            int currentNode = lastNode + MainScript.Height;
            for (int i = -MainScript.Width / 2+1; i < MainScript.Width / 2; i++)
            {
                //Checks if a edge exits between these nodes.
                if (MainScript.AllNodes[lastNode].GetEdgeToNode(MainScript.AllNodes[currentNode]) == null) Instantiate(wallPrefab, new Vector3(i, j - 0.5f, 0), Quaternion.Euler(0, 0, 90));
                lastNode = currentNode;
                currentNode += MainScript.Height;
            }
        }
    }
}

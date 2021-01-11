using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateWalls : MonoBehaviour
{
    //The prefab of the walls.
    public GameObject wallPrefab;

    //public List<Tuple<int, int>> Edges { get; set; }

    //private void CreateEdges()
    //{
    //    Edges = new List<Tuple<int, int>>
    //    {
    //        new Tuple<int, int>(61, 62), new Tuple<int, int>(51, 61), new Tuple<int, int>(50, 51), new Tuple<int, int>(40, 50), new Tuple<int, int>(30, 40), new Tuple<int, int>(20, 30), 
    //        new Tuple<int, int>(10, 20), new Tuple<int, int>(0, 10), new Tuple<int, int>(10, 11), new Tuple<int, int>(1, 11), new Tuple<int, int>(20, 21), new Tuple<int, int>(1, 2),
    //        new Tuple<int, int>(11, 12), new Tuple<int, int>(2, 3), new Tuple<int, int>(3, 4), new Tuple<int, int>(4, 5), new Tuple<int, int>(5, 6), new Tuple<int, int>(6, 16), 
    //        new Tuple<int, int>(16, 17), new Tuple<int, int>(17, 27), new Tuple<int, int>(27, 37), new Tuple<int, int>(27, 28), new Tuple<int, int>(18, 28), new Tuple<int, int>(7, 17), 
    //        new Tuple<int, int>(7, 8), new Tuple<int, int>(28, 29), new Tuple<int, int>(29, 39), new Tuple<int, int>(39, 49), new Tuple<int, int>(49, 59), new Tuple<int, int>(38, 39), 
    //        new Tuple<int, int>(38, 48), new Tuple<int, int>(36, 37), new Tuple<int, int>(35, 36), new Tuple<int, int>(25, 35), new Tuple<int, int>(25, 26), new Tuple<int, int>(18, 19), 
    //        new Tuple<int, int>(9, 19), new Tuple<int, int>(24, 25), new Tuple<int, int>(23, 24), new Tuple<int, int>(21, 31), new Tuple<int, int>(22, 32), new Tuple<int, int>(34, 35), 
    //        new Tuple<int, int>(34, 44), new Tuple<int, int>(33, 34), new Tuple<int, int>(32, 42), new Tuple<int, int>(42, 52), new Tuple<int, int>(52, 53), new Tuple<int, int>(43, 53), 
    //        new Tuple<int, int>(14, 24), new Tuple<int, int>(14, 15), new Tuple<int, int>(35, 45), new Tuple<int, int>(45, 55), new Tuple<int, int>(41, 42), new Tuple<int, int>(50, 60), 
    //        new Tuple<int, int>(62, 72), new Tuple<int, int>(71, 72), new Tuple<int, int>(71, 81), new Tuple<int, int>(81, 91), new Tuple<int, int>(91, 92), new Tuple<int, int>(82, 92), 
    //        new Tuple<int, int>(91, 101), new Tuple<int, int>(101, 111), new Tuple<int, int>(111, 121), new Tuple<int, int>(121, 122), new Tuple<int, int>(122, 132), new Tuple<int, int>(122, 123), 
    //        new Tuple<int, int>(123, 133), new Tuple<int, int>(133, 134), new Tuple<int, int>(134, 144), new Tuple<int, int>(113, 123), new Tuple<int, int>(103, 113), new Tuple<int, int>(103, 104), 
    //        new Tuple<int, int>(93, 103), new Tuple<int, int>(82, 83), new Tuple<int, int>(83, 84), new Tuple<int, int>(84, 94), new Tuple<int, int>(104, 105), new Tuple<int, int>(72, 73), 
    //        new Tuple<int, int>(73, 74), new Tuple<int, int>(74, 75), new Tuple<int, int>(75, 76), new Tuple<int, int>(65, 75), new Tuple<int, int>(65, 66), new Tuple<int, int>(66, 67), 
    //        new Tuple<int, int>(67, 68), new Tuple<int, int>(58, 68), new Tuple<int, int>(57, 58), new Tuple<int, int>(56, 57), new Tuple<int, int>(76, 77), new Tuple<int, int>(75, 85), 
    //        new Tuple<int, int>(85, 86), new Tuple<int, int>(86, 96), new Tuple<int, int>(96, 106), new Tuple<int, int>(95, 96), new Tuple<int, int>(106, 116), new Tuple<int, int>(116, 117), 
    //        new Tuple<int, int>(116, 126), new Tuple<int, int>(125, 126), new Tuple<int, int>(115, 116), new Tuple<int, int>(114, 115), new Tuple<int, int>(86, 87), new Tuple<int, int>(87, 88), 
    //        new Tuple<int, int>(78, 88), new Tuple<int, int>(78, 79), new Tuple<int, int>(79, 89), new Tuple<int, int>(89, 99), new Tuple<int, int>(98, 99), new Tuple<int, int>(69, 79), 
    //        new Tuple<int, int>(46, 56), new Tuple<int, int>(44, 54), new Tuple<int, int>(54, 64), new Tuple<int, int>(63, 64), new Tuple<int, int>(46, 47), new Tuple<int, int>(80, 81), 
    //        new Tuple<int, int>(70, 80), new Tuple<int, int>(114, 124), new Tuple<int, int>(120, 121), new Tuple<int, int>(110, 120), new Tuple<int, int>(120, 130), new Tuple<int, int>(130, 131), 
    //        new Tuple<int, int>(112, 122), new Tuple<int, int>(102, 112), new Tuple<int, int>(125, 135), new Tuple<int, int>(135, 145), new Tuple<int, int>(145, 146), new Tuple<int, int>(146, 147), 
    //        new Tuple<int, int>(147, 157), new Tuple<int, int>(137, 147), new Tuple<int, int>(127, 137), new Tuple<int, int>(127, 128), new Tuple<int, int>(128, 129), new Tuple<int, int>(128, 138), 
    //        new Tuple<int, int>(138, 139), new Tuple<int, int>(139, 149), new Tuple<int, int>(149, 159), new Tuple<int, int>(158, 159), new Tuple<int, int>(148, 158), new Tuple<int, int>(118, 128), 
    //        new Tuple<int, int>(107, 117), new Tuple<int, int>(108, 118), new Tuple<int, int>(119, 129), new Tuple<int, int>(108, 109), new Tuple<int, int>(136, 137), new Tuple<int, int>(96, 97), 
    //        new Tuple<int, int>(80, 90), new Tuple<int, int>(90, 100), new Tuple<int, int>(142, 143), new Tuple<int, int>(130, 140), new Tuple<int, int>(140, 150), new Tuple<int, int>(150, 151), 
    //        new Tuple<int, int>(150, 160), new Tuple<int, int>(160, 170), new Tuple<int, int>(170, 171), new Tuple<int, int>(161, 171), new Tuple<int, int>(161, 162), new Tuple<int, int>(162, 172), 
    //        new Tuple<int, int>(172, 173), new Tuple<int, int>(162, 163), new Tuple<int, int>(153, 163), new Tuple<int, int>(142, 152), new Tuple<int, int>(141, 151), new Tuple<int, int>(145, 155), 
    //        new Tuple<int, int>(154, 155), new Tuple<int, int>(154, 164), new Tuple<int, int>(173, 174), new Tuple<int, int>(174, 175), new Tuple<int, int>(165, 175), new Tuple<int, int>(165, 166), 
    //        new Tuple<int, int>(166, 176), new Tuple<int, int>(155, 156), new Tuple<int, int>(158, 168), new Tuple<int, int>(168, 178), new Tuple<int, int>(178, 179), new Tuple<int, int>(168, 169), 
    //        new Tuple<int, int>(167, 168), new Tuple<int, int>(176, 177), new Tuple<int, int>(21, 22), new Tuple<int, int>(143, 144), new Tuple<int, int>(13, 23)
    //    };
    //}

    /**
     * Creates all walls of the labyrinth.
     */
    public void CreateAllWalls()
    {
        //Upper and lower bound of the labyrinth.
        for (int i = -MainScript.Width / 2; i < MainScript.Width / 2; i++)
        {
            Instantiate(wallPrefab, new Vector3(i + 0.5f, MainScript.Height / 2, 0), Quaternion.identity);
            Instantiate(wallPrefab, new Vector3(i + 0.5f, -MainScript.Height / 2, 0), Quaternion.identity);
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
                //if (!Edges.Contains(new Tuple<int, int>(lastNode, currentNode))) Instantiate(wallPrefab, new Vector3(i + 0.5f, j, 0), Quaternion.identity);
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
                //if (!Edges.Contains(new Tuple<int, int>(lastNode, currentNode))) Instantiate(wallPrefab, new Vector3(i, j - 0.5f, 0), Quaternion.Euler(0, 0, 90));
                //Checks if a edge exits between these nodes.
                if (MainScript.AllNodes[lastNode].GetEdgeToNode(MainScript.AllNodes[currentNode]) == null) Instantiate(wallPrefab, new Vector3(i, j - 0.5f, 0), Quaternion.Euler(0, 0, 90));
                lastNode = currentNode;
                currentNode += MainScript.Height;
            }
        }
    }
}

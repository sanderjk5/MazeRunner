using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    //The prefab of the modified dijkstra algorithm.
    public GameObject modifiedDijkstraAlgorithmPrefab;

    public int ShortestDistance { get; set; }
    public List<NodeController> ShortestPath { get; set; }

    public NodeController CurrentNodePosition { get; set; }

    public int CurrentPositionInShortestPath { get; set; }

    public void InitializeOpponent()
    {
        GameObject algorithmObject = Instantiate(modifiedDijkstraAlgorithmPrefab);
        ModifiedDijkstraAlgorithm algorithm = algorithmObject.GetComponent<ModifiedDijkstraAlgorithm>();
        algorithm.Initialize(MainScript.AllNodes[160], MainScript.AllNodes[719]);
        algorithm.CalculateModifiedDijkstraAlgorithm();
        ShortestDistance = algorithm.ShortestDistance;
        ShortestPath = algorithm.ShortestPath;
        CurrentNodePosition = MainScript.AllNodes[160];
        CurrentPositionInShortestPath = 0;
        Destroy(algorithmObject);
        StartCoroutine(MoveOpponent());
    }

    IEnumerator MoveOpponent()
    {
        while (!CountdownController.GameStarted)
        {
            yield return new WaitForSeconds(0.5f);
        }

        while(CurrentNodePosition.Id != 719)
        {
            CurrentPositionInShortestPath++;
            CurrentNodePosition = ShortestPath[CurrentPositionInShortestPath];
            gameObject.transform.position = CurrentNodePosition.gameObject.transform.position;
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.transform.position = new Vector3(8.75f, -5.25f);
        Debug.Log("Finished");
    }
}

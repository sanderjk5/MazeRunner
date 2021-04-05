using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    //The prefab of the modified dijkstra algorithm.
    public GameObject modifiedDijkstraAlgorithmPrefab;
    public GameObject endBattleGameController;
    private float intermediateSteps;
    private float stepDuration;
    
    public float OpponentsTime { get; private set; }
    public int StepCounter { get; set; }
    public int ShortestDistance { get; set; }
    public List<NodeController> ShortestPath { get; set; }

    public NodeController CurrentNodePosition { get; set; }

    public int CurrentPositionInShortestPath { get; set; }

    public void InitializeOpponent()
    {
        GameObject algorithmObject = Instantiate(modifiedDijkstraAlgorithmPrefab);
        ModifiedDijkstraAlgorithm algorithm = algorithmObject.GetComponent<ModifiedDijkstraAlgorithm>();
        algorithm.Initialize(MainScript.AllNodes[700], MainScript.AllNodes[19]);
        algorithm.CalculateModifiedDijkstraAlgorithm();
        ShortestDistance = algorithm.ShortestDistance;
        ShortestPath = algorithm.ShortestPath;
        CurrentNodePosition = MainScript.AllNodes[700];
        CurrentPositionInShortestPath = 0;
        Destroy(algorithmObject);
        intermediateSteps = 60;
        stepDuration = 0.25f;
        StartCoroutine(MoveOpponent());
    }

    IEnumerator MoveOpponent()
    {
        while (!CountdownController.GameStarted)
        {
            yield return new WaitForSeconds(stepDuration);
        }

        while(CurrentNodePosition.Id != 19)
        {
            CurrentPositionInShortestPath++;
            CurrentNodePosition = ShortestPath[CurrentPositionInShortestPath];
            bool moveHorizontal;
            float fixedPositionValue;
            float differenceBetweenVariablePositionValues;
            float variablePositionValueLastNode;
            if (gameObject.transform.position.y == CurrentNodePosition.gameObject.transform.position.y)
            {
                moveHorizontal = true;
                fixedPositionValue = gameObject.transform.position.y;
                variablePositionValueLastNode = gameObject.transform.position.x;
                differenceBetweenVariablePositionValues = CurrentNodePosition.gameObject.transform.position.x - variablePositionValueLastNode;
            } 
            else
            {
                moveHorizontal = false;
                fixedPositionValue = gameObject.transform.position.x;
                variablePositionValueLastNode = gameObject.transform.position.y;
                differenceBetweenVariablePositionValues = CurrentNodePosition.gameObject.transform.position.y - variablePositionValueLastNode;
            }
            for(float i = 1; i <= intermediateSteps; i++)
            {
                float newValue = variablePositionValueLastNode + (i / intermediateSteps * differenceBetweenVariablePositionValues);
                if (moveHorizontal)
                {
                    gameObject.transform.position = new Vector3(newValue, fixedPositionValue);
                }
                else
                {
                    gameObject.transform.position = new Vector3(fixedPositionValue, newValue);
                }
                yield return new WaitForSeconds(1/intermediateSteps * stepDuration);
            }
            //StepCounter++;
            //GameObject.Find("OpponnentStepCounter").GetComponent<TextMeshProUGUI>().text = "Opponnents Steps: " + StepCounter;
        }
        for (float i = 1; i <= intermediateSteps; i++)
        {
            float newValue = -4.75f + (i / intermediateSteps * -0.5f);
            gameObject.transform.position = new Vector3(-8.75f, newValue);
            yield return new WaitForSeconds(1 / intermediateSteps * stepDuration);
        }
        EndBattleGameMenu.OpponentFinished = true;
        OpponentsTime = endBattleGameController.GetComponent<EndBattleGameController>().timer;
    }

    public void UpdateStepCounter()
    {
        GameObject.Find("OpponentStepCounter").GetComponent<TextMeshProUGUI>().text = "Opponents Steps: " + StepCounter;
    }
}

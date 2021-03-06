﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class OpponentController : MonoBehaviour
{
    //The prefab of the modified dijkstra algorithm.
    public GameObject modifiedDijkstraAlgorithmPrefab;
    public GameObject endBattleGameController;
    private float intermediateSteps;
    private float stepDuration;
    private bool moveHorizontal;
    private float fixedPositionValue;
    private float differenceBetweenVariablePositionValues;
    private float variablePositionValueLastNode;
    public GameObject dijkstraPrefab;
    private bool movesToFreezer;
    private float preCalculatedTime;
    private bool resetPosition;

    public float OpponentsTime { get; private set; }
    public int StepCounter { get; set; }
    public int ShortestDistance { get; set; }
    public List<NodeController> ShortestPath { get; set; }

    public NodeController CurrentNodePosition { get; set; }

    public int CurrentPositionInShortestPath { get; set; }

    public void InitializeOpponent()
    {
        movesToFreezer = false;
        CurrentNodePosition = MainScript.AllNodes[700];
        CalculatePath();
        intermediateSteps = 40;
        stepDuration = 0.5f;
        preCalculatedTime = 0f;
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
            if (resetPosition)
            {
                movesToFreezer = false;
                gameObject.transform.position = new Vector3(8.75f, 4.75f, 0f);
                CurrentNodePosition = MainScript.AllNodes[700];
                CalculatePath();
                resetPosition = false;
            }
            else if(CurrentPositionInShortestPath == ShortestPath.Count - 1)
            {
                movesToFreezer = false;
                CalculatePath();
            }
            else if (CurrentPositionInShortestPath%10 == 0)
            {
                CalculatePath();
            }
            CurrentPositionInShortestPath++;
            CurrentNodePosition = ShortestPath[CurrentPositionInShortestPath];
            SetMovingValues();
            for(float i = 1; i <= intermediateSteps; i++)
            {
                if (resetPosition)
                {
                    break;
                }
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
            if (EndBattleGameMenu.OpponentFinished)
            {
                yield break;
            }
        }
        if (!EndBattleGameMenu.OpponentFinished)
        {
            StartCoroutine(MoveToTargetPosition());
        }
    }

    private void SetMovingValues()
    {
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
    }

    IEnumerator MoveToTargetPosition()
    {
        for (float i = 1; i <= intermediateSteps; i++)
        {
            float newValue = -4.75f + (i / intermediateSteps * -0.5f);
            gameObject.transform.position = new Vector3(-8.75f, newValue);
            yield return new WaitForSeconds(1 / intermediateSteps * stepDuration);
        }
        EndBattleGameMenu.OpponentFinished = true;
        if(preCalculatedTime != 0)
        {
            OpponentsTime = preCalculatedTime;
        }
        else
        {
            OpponentsTime = endBattleGameController.GetComponent<EndBattleGameController>().timer;
        }
    }

    public void CalculateOpponentValues()
    {
        GameObject dijkstraGameObject = Instantiate(dijkstraPrefab);
        ModifiedDijkstraAlgorithm dijkstraAlgorithm = dijkstraGameObject.GetComponent<ModifiedDijkstraAlgorithm>();
        dijkstraAlgorithm.Initialize(CurrentNodePosition, MainScript.AllNodes[19], MainScript.CurrentState);
        dijkstraAlgorithm.CalculateModifiedDijkstraAlgorithm();
        preCalculatedTime = endBattleGameController.GetComponent<EndBattleGameController>().timer + (dijkstraAlgorithm.ShortestPath.Count - 1) * stepDuration;
        stepDuration = 0.001f;
    }

    public void UpdateStepCounter()
    {
        GameObject.Find("OpponentStepCounter").GetComponent<TextMeshProUGUI>().text = "Opponents Steps: " + StepCounter;
    }

    public IEnumerator FreezeOpponent(int seconds)
    {
        stepDuration *= 2;
        yield return new WaitForSeconds(seconds);
        stepDuration /= 2;
    }

    private void CalculatePath()
    {
        if (EndBattleGameMenu.PlayerFinished)
        {
            movesToFreezer = false;
        }
        if (movesToFreezer)
        {
            return;
        }
        GameObject dijkstraGameObject;
        ModifiedDijkstraAlgorithm dijkstraAlgorithm;
        List<NodeController> shortestPathToNearestFreezer = new List<NodeController>();
        int distanceToNearestFreezer = Int32.MaxValue;
        int possibilityToChooseFreezer = 0;
        if (!EndBattleGameMenu.PlayerFinished)
        {
            foreach (FreezerController freezer in MainScript.AllFreezer)
            {
                dijkstraGameObject = Instantiate(dijkstraPrefab);
                dijkstraAlgorithm = dijkstraGameObject.GetComponent<ModifiedDijkstraAlgorithm>();
                dijkstraAlgorithm.Initialize(CurrentNodePosition, freezer.CorrespondingNode, MainScript.CurrentState);
                dijkstraAlgorithm.CalculateModifiedDijkstraAlgorithm();
                if (dijkstraAlgorithm.ShortestDistance < distanceToNearestFreezer)
                {
                    distanceToNearestFreezer = dijkstraAlgorithm.ShortestDistance;
                    shortestPathToNearestFreezer = dijkstraAlgorithm.ShortestPath;
                }
                Destroy(dijkstraGameObject);
            }
            if (distanceToNearestFreezer != Int32.MaxValue)
            {
                possibilityToChooseFreezer = Mathf.Max(0, 45 - distanceToNearestFreezer);
            }
        }

        int randomNumber = Random.Range(0, 101);
        if (randomNumber < possibilityToChooseFreezer && shortestPathToNearestFreezer.Count != 0)
        {
            movesToFreezer = true;
            ShortestPath = shortestPathToNearestFreezer;
        }
        else
        {
            movesToFreezer = false;
            dijkstraGameObject = Instantiate(dijkstraPrefab);
            dijkstraAlgorithm = dijkstraGameObject.GetComponent<ModifiedDijkstraAlgorithm>();
            dijkstraAlgorithm.Initialize(CurrentNodePosition, MainScript.AllNodes[19], MainScript.CurrentState);
            dijkstraAlgorithm.CalculateModifiedDijkstraAlgorithm();
            ShortestPath = dijkstraAlgorithm.ShortestPath;
            Destroy(dijkstraGameObject);
        }
        CurrentPositionInShortestPath = 0;
    }

    public void ResetPosition()
    {
        resetPosition = true;
    }
}

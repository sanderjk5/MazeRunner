using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FreezerController : MonoBehaviour
{
    public NodeController CorrespondingNode { get; private set; }
    //The prefab of generating the obstacles
    public GameObject obstacleGenerationPrefab;

    public void Initialize(NodeController correspondingNode)
    {
        CorrespondingNode = correspondingNode;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.Find("Opponent").GetComponent<BoxCollider2D>().Equals(collision) && !EndBattleGameMenu.PlayerFinished)
        {
            ChooseItemProperty(false);
        }
        else if(GameObject.Find("Ruby").GetComponent<EdgeCollider2D>().Equals(collision) && !EndBattleGameMenu.OpponentFinished)
        {
            ChooseItemProperty(true);
        }
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        MainScript.AllFreezer.Remove(this);
    }

    private void ChooseItemProperty(bool activatedByPlayer)
    {
        float possibilityToChooseObstacle = 100f * ((4f - MainScript.BattleGameCurrentButtonCounter)/MainScript.AllFreezer.Count);
        int randomNumber = Random.Range(1, 101);
        if(randomNumber < possibilityToChooseObstacle)
        {
            //Generates an obstacle
            GameObject gameObject = Instantiate(obstacleGenerationPrefab);
            ObstacleGeneration obstacleGeneration = gameObject.GetComponent<ObstacleGeneration>();
            if (activatedByPlayer)
            {
                obstacleGeneration.InsertObstacleOnPath((int)MainScript.BattleGameCurrentButtonCounter, !activatedByPlayer, GameObject.Find("Opponent").GetComponent<OpponentController>().CurrentNodePosition.Id);
            }
            else
            {
                obstacleGeneration.InsertObstacleOnPath((int)MainScript.BattleGameCurrentButtonCounter, !activatedByPlayer, MainScript.PlayerPath[MainScript.PlayerPath.Count -1].Id);
            }
            MainScript.BattleGameCurrentButtonCounter++;
        }
        else
        {
            if (activatedByPlayer)
            {
                StartCoroutine(GameObject.Find("Opponent").GetComponent<OpponentController>().FreezeOpponent(10));
            }
            else
            {
                StartCoroutine(GameObject.Find("Ruby").GetComponent<RubyController>().FreezePlayer(10));
            }
        }
    }
}

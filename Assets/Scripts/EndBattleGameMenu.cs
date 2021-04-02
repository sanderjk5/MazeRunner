using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBattleGameMenu : MonoBehaviour
{
    public static bool PlayerFinished = false;
    public static bool OpponnentFinished = false;
    public GameObject endBattleGameMenuUI;
    public GameObject endBattleGameController;
    //The normal game menu ui.
    public GameObject gameMenuUI;
    public GameObject opponnent;

    // Update is called once per frame
    void Update()
    {
        //Enables the menu if the game is finished.
        if (PlayerFinished && OpponnentFinished)
        {
            EnableEndBattleGameMenu();
        }
    }

    /**
 * <summary>Enables the end menu and prints the corresponding values.</summary>
 */
    public void EnableEndBattleGameMenu()
    {
        PlayerFinished = false;
        OpponnentFinished = false;
        //Deactivates the other menus and the bar at the bottom of the game scene.
        gameMenuUI.SetActive(false);
        //Activates the end game menu.
        endBattleGameMenuUI.SetActive(true);

        float playersScore = MainScript.CurrentStepCount + endBattleGameController.GetComponent<EndBattleGameController>().PlayersTime;
        float opponnentsScore = opponnent.GetComponent<OpponentController>().StepCounter + opponnent.GetComponent<OpponentController>().OpponnentsTime;
        GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = "Your Score:\n" + playersScore;
        GameObject.Find("OpponnentsScoreText").GetComponent<TextMeshProUGUI>().text = "Opponnents Score:\n" + opponnentsScore;
        if(opponnentsScore >= playersScore)
        {
            GameObject.Find("EndGameInfoText").GetComponent<TextMeshProUGUI>().text = "Oh no!\n Your opponnent beats you!";
        }
    }

    /**
     * <summary>Loads the start menu.</summary>
     */
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    /**
     * <summary>Quits the game.</summary>
     */
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

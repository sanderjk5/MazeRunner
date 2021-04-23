using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBattleGameMenu : MonoBehaviour
{
    public static bool PlayerFinished = false;
    public static bool OpponentFinished = false;
    public GameObject endBattleGameMenuUI;
    public GameObject endBattleGameController;
    //The normal game menu ui.
    public GameObject gameMenuUI;
    public GameObject opponent;

    // Update is called once per frame
    void Update()
    {
        //Enables the menu if the game is finished.
        if (PlayerFinished && OpponentFinished)
        {
            MainScript.EnableUserInput = false;
            endBattleGameController.GetComponent<EndBattleGameController>().timerIsRunning = false;
            EnableEndBattleGameMenu();
        }
    }

    /**
 * <summary>Enables the end menu and prints the corresponding values.</summary>
 */
    public void EnableEndBattleGameMenu()
    {
        PlayerFinished = false;
        OpponentFinished = false;
        //Deactivates the other menus and the bar at the bottom of the game scene.
        gameMenuUI.SetActive(false);
        //Activates the end game menu.
        endBattleGameMenuUI.SetActive(true);

        float playersScore = Mathf.Max(1000 - (MainScript.CurrentStepCount + Mathf.FloorToInt(endBattleGameController.GetComponent<EndBattleGameController>().PlayersTime / 2)), 0);
        float opponentsScore = Mathf.Max(1000 - (opponent.GetComponent<OpponentController>().StepCounter + Mathf.FloorToInt(opponent.GetComponent<OpponentController>().OpponentsTime / 2)), 0);
        GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = "Your Score:\n" + playersScore;
        GameObject.Find("OpponentsScoreText").GetComponent<TextMeshProUGUI>().text = "Opponents Score:\n" + opponentsScore;
        if(opponentsScore >= playersScore)
        {
            GameObject.Find("EndGameInfoText").GetComponent<TextMeshProUGUI>().text = "Oh no!\n Your opponent beats you!";
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
        Application.Quit();
    }
}

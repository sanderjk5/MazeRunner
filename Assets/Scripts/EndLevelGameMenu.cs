using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelGameMenu : MonoBehaviour
{
    //Flag if the game is finished.
    public static bool LevelGameIsFinished = false;
    //The ui element.
    public GameObject endLevelGameMenuUI;
    //The controller of the level game end.
    public GameObject endLevelGameController;
    //The normal game menu ui.
    public GameObject gameMenuUI;
    //The game values.
    public GameObject gameValuesUI;

    // Update is called once per frame
    void Update()
    {
        //Enables the menu if the game is finished.
        if (LevelGameIsFinished)
        {
            EnableEndLevelGameMenu();
        }
    }

    /**
     * <summary>Enables the end menu and prints the corresponding values.</summary>
     */
    public void EnableEndLevelGameMenu()
    {
        LevelGameIsFinished = false;
        //Deactivates the other menus and the bar at the bottom of the game scene.
        gameValuesUI.SetActive(false);
        gameMenuUI.SetActive(false);
        //Activates the end game menu.
        endLevelGameMenuUI.SetActive(true);
        
        //The remaining time of the player.
        float remainingTime = endLevelGameController.GetComponent<EndLevelGameController>().timeRemaining;
        remainingTime -= Mathf.Max(MainScript.CurrentStepCount - MainScript.OptimalStepCount, 0);
        if(remainingTime > 0)
        {
            if(remainingTime > PlayerPrefs.GetFloat("LevelModusBestTime", 0))
            {
                PlayerPrefs.SetFloat("LevelModusBestTime", remainingTime);
            }
            //Prints the remaining time.
            remainingTime += 1;
            float minutes = Mathf.FloorToInt(remainingTime / 60);
            float seconds = Mathf.FloorToInt(remainingTime % 60);
            float minutesBestTime = Mathf.FloorToInt(PlayerPrefs.GetFloat("LevelModusBestTime", 0) / 60);
            float secondsBestTime = Mathf.FloorToInt(PlayerPrefs.GetFloat("LevelModusBestTime", 0) % 60);
            GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>().text = "Remaining Time:\n" + string.Format("{0:00}:{1:00}", minutes, seconds);
            GameObject.Find("LevelHighscoreText").GetComponent<TextMeshProUGUI>().text = "Best Time:\n" + string.Format("{0:00}:{1:00}", minutesBestTime, secondsBestTime);
        }
        else
        {
            if (MainScript.CurrentLevelCount + 1 > PlayerPrefs.GetInt("LevelModusHighestLevel", 0))
            {
                PlayerPrefs.SetInt("LevelModusHighestLevel", MainScript.CurrentLevelCount + 1);
            }
            //Prints the texts if the player failed.
            GameObject.Find("EndGameInfoText").GetComponent<TextMeshProUGUI>().text = "Game Over";
            GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>().text = "Remaining Time:\n" + string.Format("{0:00}:{1:00}", 0, 0);
            GameObject.Find("LevelHighscoreText").GetComponent<TextMeshProUGUI>().text = "Highest Level:\n" + PlayerPrefs.GetInt("LevelModusHighestLevel", 0);
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

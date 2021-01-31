using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelGameMenu : MonoBehaviour
{
    public static bool LevelGameIsFinished = false;
    public GameObject endLevelGameMenuUI;
    public GameObject endLevelGameController;
    public GameObject gameMenuUI;
    public GameObject gameValuesUI;

    // Update is called once per frame
    void Update()
    {
        if (LevelGameIsFinished)
        {
            EnableEndLevelGameMenu();
        }
    }

    public void EnableEndLevelGameMenu()
    {
        LevelGameIsFinished = false;
        gameValuesUI.SetActive(false);
        gameMenuUI.SetActive(false);
        endLevelGameMenuUI.SetActive(true);
        
        float remainingTime = endLevelGameController.GetComponent<EndLevelGameController>().timeRemaining;
        if(remainingTime > 0)
        {
            remainingTime += 1;
            float minutes = Mathf.FloorToInt(remainingTime / 60);
            float seconds = Mathf.FloorToInt(remainingTime % 60);
            GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>().text = "Remaining Time:\n" + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            GameObject.Find("EndGameInfoText").GetComponent<TextMeshProUGUI>().text = "Game Over";
            GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>().text = "Remaining Time:\n" + string.Format("{0:00}:{1:00}", 0, 0);
        }
        
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

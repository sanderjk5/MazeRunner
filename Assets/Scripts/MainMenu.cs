using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /**
     * <summary>Loads the level game scene.</summary>
     */
    public void LevelButtonAction()
    {
        SceneManager.LoadScene(2);
    }

    /**
     * Quits the game.
     */
    public void QuitButtonAction()
    {
        Application.Quit();
    }
}

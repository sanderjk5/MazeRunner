using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    //Flag if the game menu is activated.
    public static bool gameMenuIsActivated = false;
    //The game menu.
    public GameObject gameMenuUI;

    // Start is called before the first frame update
    private void Start()
    {
        gameMenuIsActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Enables the game menu if the player presses the escape button and the user input is activated.
        if (Input.GetKeyDown(KeyCode.Escape) && MainScript.EnableUserInput)
        {
            if (gameMenuIsActivated)
            {
                Resume();
            } 
            else
            {
                Pause();
            }
        }
    }

    /**
     * <summary>Resumes the game.</summary>
     */
    public void Resume()
    {
        //Enables player movement.
        Rigidbody2D ruby = GameObject.Find("Ruby").GetComponent<Rigidbody2D>();
        ruby.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Sets the flag and deactivates the menu.
        gameMenuIsActivated = false;
        gameMenuUI.SetActive(false);
    }

    /**
     * <summary>Activates the game menu and freezes the player.</summary>
     */
    private void Pause()
    {
        //Disables the player movement.
        Rigidbody2D ruby = GameObject.Find("Ruby").GetComponent<Rigidbody2D>();
        ruby.constraints = RigidbodyConstraints2D.FreezeAll;

        //Sets the flag and activates the menu.
        gameMenuIsActivated = true;
        gameMenuUI.SetActive(true);
    }

    /**
     * <summary>Loads the main menu.</summary>
     */
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    /**
     * Quits the game.
     */
    public void QuitGame()
    {
        Application.Quit();
    }
}

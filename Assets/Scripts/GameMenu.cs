using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public static bool gameMenuIsActivated = false;
    public GameObject gameMenuUI;

    // Start is called before the first frame update
    private void Start()
    {
        gameMenuIsActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    private void Resume()
    {
        Rigidbody2D ruby = GameObject.Find("Ruby").GetComponent<Rigidbody2D>();
        ruby.constraints = RigidbodyConstraints2D.FreezeRotation;
        gameMenuIsActivated = false;
        gameMenuUI.SetActive(false);
    }

    private void Pause()
    {
        Rigidbody2D ruby = GameObject.Find("Ruby").GetComponent<Rigidbody2D>();
        ruby.constraints = RigidbodyConstraints2D.FreezeAll;
        gameMenuIsActivated = true;
        gameMenuUI.SetActive(true);
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

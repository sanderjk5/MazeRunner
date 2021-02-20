using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    //The remaining time of the timer.
    public float timer;
    //Flag is true when the timer is active.
    public bool timerIsRunning;

    private void Start()
    {
        timer = 0;
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the timer by subtracting the elapsed time.
        if (timerIsRunning && CountdownController.GameStarted)
        {
            timer += Time.deltaTime;
            DisplayTime(timer);
        }
    }

    /**
     * <summary>Displays the timer at the bottom of the game scene.</summary>
     * <param name="timeToDisplay">The current timer.</param>
     */
    private void DisplayTime(float timeToDisplay)
    {
        //Calculates the minutes and seconds.
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        //Prints the timer.
        GameObject remainingTimeText = GameObject.Find("DisplayTime");
        remainingTimeText.GetComponent<TextMeshProUGUI>().text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /**
     * <summary>Triggers the end game menu when the player exits the maze.</summary>
     * <param name="other">The collision of the player with the end game object.</param>
     */
    void OnTriggerEnter2D(Collider2D other){
        MainScript.EnableUserInput = false;
        timerIsRunning = false;
        // Freeze the character
        Rigidbody2D ruby = GameObject.Find("Ruby").GetComponent<Rigidbody2D>();
        ruby.constraints = RigidbodyConstraints2D.FreezeAll;

        // Set the end game variable to true
        EndGameMenu.EndGotReached = true;
    }
}

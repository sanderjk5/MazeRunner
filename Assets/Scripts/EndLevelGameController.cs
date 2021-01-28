using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndLevelGameController : MonoBehaviour
{
    //The remaining time of the timer.
    public float timeRemaining;
    //Flag is true when the timer is active.
    public bool timerIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        //Initializes the timer (900 = 15 min)
        timeRemaining = 900;
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the timer by subtracting the elapsed time.
        if (timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(timeRemaining);
                //The game ends when the timer is expired.
                Application.Quit();
            }
        }
    }

    /**
     * <summary>Loads the next level when the player exists the maze.</summary>
     * <param name="other">The player.</param>
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        //Pauses the timer.
        timerIsRunning = false;
        //Subtracts the difference of the optimal steps and the current steps of the remaining time.
        timeRemaining -= 1.5f * Math.Max(0, MainScript.CurrentStepCount - MainScript.OptimalStepCount);
        if (timeRemaining < 0) timeRemaining = 0;
        DisplayTime(timeRemaining);
        //Exists the game when the timer is expired.
        if(timeRemaining == 0)
        {
            Application.Quit();
        }
        //Checks if there are levels left.
        if(MainScript.CurrentLevelCount < 7)
        {
            //Loads the next level.
            GameObject.Find("MainScript").GetComponent<MainScript>().LoadNextLevel();
            //Enables the user input and starts the timer.
            MainScript.EnableUserInput = true;
            timerIsRunning = true;
        } else
        {
            Application.Quit();
        }
    }

    /**
     * <summary>Displays the timer (minutes and seconds)</summary>
     * <param name="remainingTime">the remaining time of the timer</param>
    */
    private void DisplayTime(float remainingTime)
    {
        remainingTime += 1;

        //Calculates the minutes and seconds.
        float minutes = Mathf.FloorToInt(remainingTime / 60);
        float seconds = Mathf.FloorToInt(remainingTime % 60);

        //Prints the timer.
        GameObject remainingTimeText = GameObject.Find("DisplayTime");
        remainingTimeText.GetComponent<UnityEngine.UI.Text>().text = "Remaining Time : " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

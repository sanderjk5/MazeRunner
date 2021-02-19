using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class EndLevelGameController : MonoBehaviour
{
    //The remaining time of the timer.
    public float timeRemaining;
    //Flag is true when the timer is active.
    public bool timerIsRunning;
    //The optimal bonus times which the could receive.
    public float[] optimalBonusTimes;
    //The canvas object of the scene.
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        //Initializes the timer
        timerIsRunning = true;

        //Initializes the bonus times.
        timeRemaining = 35;
        optimalBonusTimes = new float[7] { 40, 45, 50, 90, 110, 130, 150};
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
                //The game ends when the timer is expired.
                EndGame();
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
        
        //Checks if there are levels left.
        if (MainScript.CurrentLevelCount < 7)
        {
            //Adds the time bonus (depends on the performance of the player during the last level) to the remaining time.
            float timeBonus = CalculateTimeBonus();
            timeRemaining += timeBonus;
            DisplayTime(timeRemaining);
            PrintTimeBonusText(timeBonus);
            //Loads the next level.
            GameObject.Find("MainScript").GetComponent<MainScript>().LoadNextLevel();
            //Enables the user input and starts the timer.
            timerIsRunning = true;
        } else
        {
            EndGame();
        }
    }

    /**
     * <summary>Displays the timer (minutes and seconds)</summary>
     * <param name="timeToDisplay">the remaining time of the timer</param>
    */
    private void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay != 0) timeToDisplay += 1;

        //Calculates the minutes and seconds.
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        //Prints the timer.
        GameObject remainingTimeText = GameObject.Find("DisplayTime");
        remainingTimeText.GetComponent<TextMeshProUGUI>().text = "Remaining Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /**
     * <summary>Freezes the player and initializes the end game menu.</summary>
     */
    private void EndGame()
    {
        MainScript.EnableUserInput = false;
        Rigidbody2D ruby = GameObject.Find("Ruby").GetComponent<Rigidbody2D>();
        ruby.constraints = RigidbodyConstraints2D.FreezeAll;
        EndLevelGameMenu.LevelGameIsFinished = true;
    }

    /**
     * <summary>Calculates the time bonus of the current level.</summary>
     */
    private float CalculateTimeBonus()
    {
        float timeBonus;
        timeBonus = Math.Max(optimalBonusTimes[MainScript.CurrentLevelCount] - Math.Max(0, MainScript.CurrentStepCount - MainScript.OptimalStepCount), 0);
        return timeBonus;
    }

    /**
     * <summary>Prints the time bonus of the current level.</summary>
     */
    private void PrintTimeBonusText(float timeBonus)
    {
        //Calculates the rating of the player.
        float ratingValue = timeBonus / optimalBonusTimes[MainScript.CurrentLevelCount];
        string rating;
        if(ratingValue < 0.2f)
        {
            rating = "Terrible!";
        }
        else if(ratingValue < 0.4f)
        {
            rating = "Bad!";
        }
        else if (ratingValue < 0.6f)
        {
            rating = "Okay!";
        }
        else if (ratingValue < 0.8f)
        {
            rating = "Good!";
        }
        else
        {
            rating = "Awesome!";
        }
        canvas.GetComponent<TimeBonusMenu>().ShowTimeBonus(timeBonus, rating);
    }
}

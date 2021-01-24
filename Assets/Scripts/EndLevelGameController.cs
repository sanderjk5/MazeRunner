using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndLevelGameController : MonoBehaviour
{
    public float timeRemaining;
    public bool timerIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 900;
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
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
                Application.Quit();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        timerIsRunning = false;
        timeRemaining -= 2 * Math.Max(0, MainScript.CurrentStepCount - MainScript.OptimalStepCount);
        DisplayTime(timeRemaining);
        if(timeRemaining <= 0)
        {
            Application.Quit();
        }
        if(MainScript.CurrentLevelCount < 7)
        {
            GameObject.Find("MainScript").GetComponent<MainScript>().LoadNextLevel();
            timerIsRunning = true;
        } else
        {
            Application.Quit();
        }
    }

    private void DisplayTime(float remainingTime)
    {
        remainingTime += 1;

        float minutes = Mathf.FloorToInt(remainingTime / 60);
        float seconds = Mathf.FloorToInt(remainingTime % 60);

        GameObject remainingTimeText = GameObject.Find("DisplayTime");
        remainingTimeText.GetComponent<UnityEngine.UI.Text>().text = "Remaining Time : " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

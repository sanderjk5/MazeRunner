using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeBonusMenu : MonoBehaviour
{
    public GameObject timeBonusMenu;

    public bool timerIsRunning;
    public float timer;

    private void Update()
    {
        if(timerIsRunning)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                timerIsRunning = false;
                timeBonusMenu.SetActive(false);
            }
        }
        
    }
    public void ShowTimeBonus(float bonusTime, string rating)
    {
        float minutes = Mathf.FloorToInt(bonusTime / 60);
        float seconds = Mathf.FloorToInt(bonusTime % 60);
        timeBonusMenu.SetActive(true);
        GameObject.Find("TimeBonusText").GetComponent<TextMeshProUGUI>().text = rating + "\n + " + string.Format("{0:00}:{1:00}", minutes, seconds);
        timer = 3;
        timerIsRunning = true;
    }
}

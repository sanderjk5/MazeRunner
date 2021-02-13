using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeBonusMenu : MonoBehaviour
{
    public GameObject timeBonusMenu;

    public void ShowTimeBonus(float bonusTime, string rating)
    {
        float minutes = Mathf.FloorToInt(bonusTime / 60);
        float seconds = Mathf.FloorToInt(bonusTime % 60);

        timeBonusMenu.SetActive(false);
        timeBonusMenu.SetActive(true);
        GameObject.Find("TimeBonusText").GetComponent<TextMeshProUGUI>().text = rating + "\n + " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

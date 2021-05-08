using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleMenu : MonoBehaviour
{
    private void Start()
    {
        MainScript.UseShooter = false;
    }
    /**
     * <summary>Starts the battle game mode.</summary>
     */
    public void StartGame()
    {
        SceneManager.LoadScene(3);
    }

    public void UseShooter()
    {
        MainScript.UseShooter = !MainScript.UseShooter;
    }
}

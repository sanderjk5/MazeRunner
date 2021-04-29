using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleMenu : MonoBehaviour
{
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

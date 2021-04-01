using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{

    /**
     * <summary>Loads the level game scene.</summary>
     */
    public void LevelButtonAction()
    {
        SceneManager.LoadScene(2);
    }

    public void BattleButtonAction()
    {
        SceneManager.LoadScene(3);
    }
}

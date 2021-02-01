﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    /**
     * <summary>Starts the normal game modus.</summary>
     */
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}

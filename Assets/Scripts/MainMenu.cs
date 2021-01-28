using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void QuitButtonAction()
    {
        Debug.Log("Exited Application by clicking QUIT Button");
        Application.Quit();
    }
}

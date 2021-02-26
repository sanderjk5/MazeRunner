using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    // Start value of the countdown.
    private int countdownTime;
    // The corresponding ui elements.
    public TextMeshProUGUI countdownText;
    public GameObject countdownUI;
    private Rigidbody2D playerRigidbody;

    public static bool GameStarted { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        // Freezes the player.
        playerRigidbody = GameObject.Find("Ruby").GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        MainScript.EnableUserInput = false;
        GameStarted = false;
        // Starts the countdown.
        countdownTime = 3;
        StartCoroutine(CountdownToStart());
    }
    
    // Controls the countdown.
    IEnumerator CountdownToStart()
    {
        // Decrement the countdown in every second.
        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        // Prints go and enables the player movement.
        countdownText.text = "GO!";
        GameStarted = true;
        MainScript.EnableUserInput = true;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(1f);
        
        countdownUI.SetActive(false);
    }


}

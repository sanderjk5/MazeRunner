using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    private int countdownTime;
    public TextMeshProUGUI countdownText;
    public GameObject countdownUI;
    private Rigidbody2D playerRigidbody;

    public static bool GameStarted { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GameObject.Find("Ruby").GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        MainScript.EnableUserInput = false;
        GameStarted = false;
        countdownTime = 3;
        StartCoroutine(CountdownToStart());
    }
    
    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownText.text = "GO!";
        GameStarted = true;
        MainScript.EnableUserInput = true;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(1f);
        
        countdownUI.SetActive(false);
    }


}

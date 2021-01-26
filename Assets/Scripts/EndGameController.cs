using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        // Freeze the character
        Rigidbody2D ruby = GameObject.Find("Ruby").GetComponent<Rigidbody2D>();
        ruby.constraints = RigidbodyConstraints2D.FreezeAll;

        // Compute and show score
        int optimalSteps = MainScript.ShortestDistance;
        int stepsUsed = MainScript.CurrentStepCount;
        int score = 100 - (stepsUsed - optimalSteps);

        if (score < 0) score = 0;

        GameObject scoreObject = FindInActiveObject("Score");
        scoreObject.SetActive(true);
        scoreObject.GetComponent<UnityEngine.UI.Text>().text = "Score : " + score;
    }

    // Find an inactive GameObject by name
    GameObject FindInActiveObject(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    public GameObject optimalPathPrefab;

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

        // Show the optimal path
        ShowOptimalPath(MainScript.ShortestPath);
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

    // Show the optimal path in the game scene
    void ShowOptimalPath(List<NodeController> optimalPath)
    {
        for (int i = 0; i < optimalPath.Count - 1; i++)
        {
            float thisX = optimalPath[i].GetComponent<NodeController>().transform.position.x;
            float thisY = optimalPath[i].GetComponent<NodeController>().transform.position.y;

            float nextX = optimalPath[i + 1].GetComponent<NodeController>().transform.position.x;
            float nextY = optimalPath[i + 1].GetComponent<NodeController>().transform.position.y;

            float pathX = (nextX + thisX) / 2;
            float pathY = (nextY + thisY) / 2;

            if (pathY == nextY)
            {
                Instantiate(optimalPathPrefab, new Vector3(pathX, pathY), Quaternion.identity);
            }
            else if (pathX == nextX)
            {
                Instantiate(optimalPathPrefab, new Vector3(pathX, pathY), Quaternion.Euler(0, 0, 90));
            }
        }
    }
}

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
        // Get all buttons
        ButtonController[] buttons = FindObjectsOfType<ButtonController>();

        // Compute eacch x and y value of the optimal path edges
        for (int i = 0; i < optimalPath.Count - 1; i++)
        {
            float thisX = optimalPath[i].gameObject.transform.position.x;
            float thisY = optimalPath[i].gameObject.transform.position.y;

            float nextX = optimalPath[i + 1].gameObject.transform.position.x;
            float nextY = optimalPath[i + 1].gameObject.transform.position.y;

            float pathX = (nextX + thisX) / 2;
            float pathY = (nextY + thisY) / 2;

            // Create path with correct orientation
            if (pathY == nextY)
            {
                GameObject path = Instantiate(optimalPathPrefab, new Vector3(pathX, pathY), Quaternion.identity);
                path.GetComponent<Transform>().localScale = new Vector3(1 * MainScript.ScaleMazeSize, 0.1f * MainScript.ScaleMazeSize);
            }
            else if (pathX == nextX)
            {
                GameObject path = Instantiate(optimalPathPrefab, new Vector3(pathX, pathY), Quaternion.Euler(0, 0, 90));
                path.GetComponent<Transform>().localScale = new Vector3(1 * MainScript.ScaleMazeSize, 0.1f * MainScript.ScaleMazeSize);
            }

            // If a node is a button, search the correct obstacle and decrease the transparency
            if (optimalPath[i].Button != -1)
            {
                foreach (ButtonController button in buttons)
                {
                    int pos = System.Array.IndexOf(buttons, button);
                    if (button.CorrespondingNode.Id == optimalPath[i].Id)
                    {
                        EdgeController edge = button.CorrespondingEdge;
                        //edge.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 0.03f);
                        edge.gameObject.GetComponent<Transform>().localScale = new Vector3(1 * MainScript.ScaleMazeSize, 0.03f * MainScript.ScaleMazeSize);
                        Color edgeColor = MainScript.Colors[button.CorrespondingNode.Button];
                        edge.gameObject.GetComponent<SpriteRenderer>().color = new Color(edgeColor.r, edgeColor.g, edgeColor.b, 0.3f);
                    }
                }
            }
        }
    }
}

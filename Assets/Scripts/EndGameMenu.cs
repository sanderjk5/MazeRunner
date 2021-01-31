using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    private bool menuIsActive = false;
    private bool firstEndReached = true;
    public static bool EndGotReached = false;
    public GameObject endGameMenuUI;
    public GameObject optimalPathPrefab;

    // Update is called once per frame
    void Update()
    {
        if (EndGotReached && firstEndReached)
        {
            firstEndReached = false;
            EnableEndGameMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EndGotReached && menuIsActive)
            {
                endGameMenuUI.SetActive(false);
                menuIsActive = false;
            }
            else if (EndGotReached && !menuIsActive)
            {
                endGameMenuUI.SetActive(true);
                menuIsActive = true;
            }
        }
    }

    // Enable the end game menu with score of the player
    private void EnableEndGameMenu()
    {
        menuIsActive = true;

        // Activate the end game menu
        endGameMenuUI.SetActive(true);

        // Compute the score of the player
        int score = ComputePlayerScore();

        // Show the score of the player
        GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = "Your Score:\n" + score.ToString();
    }

    // Compute the score of a player
    int ComputePlayerScore()
    {
        int optimalSteps = MainScript.OptimalStepCount;
        int stepsUsed = MainScript.CurrentStepCount;
        int score = 100 - (stepsUsed - optimalSteps);

        if (score < 0) score = 0;

        return score;
    }

    // Show the optimal path in the game scene
    public void ShowOptimalPath()
    {
        // Remove end game menu
        endGameMenuUI.SetActive(false);
        menuIsActive = false;

        // Get the path from the maze
        List<NodeController> optimalPath = MainScript.ShortestPath;

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

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}

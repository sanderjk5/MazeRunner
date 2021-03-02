using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text shown will be formatted using this string.")]
    private string formatText = "";

    private TextMeshProUGUI tmproText;
    private TextMeshProUGUI highscoreText;
    public static float DifficultyValue { get; set; }

    public static string DifficultyText { get; private set; }

    private void Start()
    {
        tmproText = GameObject.Find("DifficultyValueText").GetComponent<TextMeshProUGUI>();
        //Displays the highscore of the player.
        highscoreText = GameObject.Find("HighscoreText").GetComponent<TextMeshProUGUI>();
        DifficultyText = "Beginner";
        tmproText.text = "Beginner";
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt(DifficultyText, 0).ToString();
        DifficultyValue = 1;
        GetComponentInParent<Slider>().onValueChanged.AddListener(HandleValueChanged);
    }

    private void HandleValueChanged(float value)
    {
        tmproText.text = string.Format(formatText, value);
        switch (value)
        {
            case 1:
                tmproText.text = "Beginner";
                break;
            case 2:
                tmproText.text = "Easy";
                break;
            case 3:
                tmproText.text = "Normal";
                break;
            case 4:
                tmproText.text = "Intermediate";
                break;
            case 5:
                tmproText.text = "Hard";
                break;
            case 6:
                tmproText.text = "Very Hard";
                break;
            case 7:
                tmproText.text = "Ultra";
                break;
            case 8:
                tmproText.text = "Nightmare";
                break;
        }

        DifficultyText = tmproText.text;
        //Updates the highscore text.
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt(DifficultyText, 0).ToString();
        DifficultyValue = value;
    }
}

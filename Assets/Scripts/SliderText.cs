using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text shown will be formatted using this string.  {0} is replaced with the actual value")]
    private string formatText = "";

    private TextMeshProUGUI tmproText;
    public static float difficultyValue;

    private void Start()
    {
        tmproText = GetComponent<TextMeshProUGUI>();
        tmproText.text = "Beginner";
        difficultyValue = 1;
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
                tmproText.text = "intermediate";
                break;
            case 5:
                tmproText.text = "hard";
                break;
            case 6:
                tmproText.text = "very hard";
                break;
            case 7:
                tmproText.text = "Ultra";
                break;
            case 8:
                tmproText.text = "Nightmare";
                break;
        }

        difficultyValue = value;
    }
}

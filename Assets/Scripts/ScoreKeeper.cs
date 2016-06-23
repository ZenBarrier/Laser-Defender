using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    private static int score = 0;
    private static Text scoreText;

    void Start()
    {
        score = 0;
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    public static void Score(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

	public static void Reset()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}

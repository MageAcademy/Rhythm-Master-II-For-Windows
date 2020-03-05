using UnityEngine;
using UnityEngine.UI;

public class ReturnSelectMusic : MonoBehaviour
{
    public GameObject maskChangeScene;
    public Text[] texts;

    private void Start()
    {
        texts[0].text = "SCORE: " + GetScore();
        texts[1].text = "BAD-" + Scoreboard.Instance[0].ToString();
        texts[2].text = "GREAT-" + Scoreboard.Instance[1].ToString();
        texts[3].text = "MISS-" + Scoreboard.Instance[2].ToString();
        texts[4].text = "PERFECT-" + Scoreboard.Instance[3].ToString();
    }

    private void OnMouseDown()
    {
        maskChangeScene.SetActive(true);
    }

    private string GetScore()
    {
        float score = 0f;
        for (int a = 0; a < 4; ++a)
        {
            score += Scoreboard.Instance[a];
        }

        score = 1000000f / score;
        score *= Scoreboard.Instance[0] * 0.25f + Scoreboard.Instance[1] * 0.5f + Scoreboard.Instance[3];
        score = score > 1000000f ? 1000000 : Mathf.RoundToInt(score);
        return ((int) score).ToString();
    }
}
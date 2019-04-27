using TMPro;
using UnityEngine;

public class HighScoreElement : MonoBehaviour
{
    public TMP_Text RankField;
    public TMP_Text UsernameField;
    public TMP_Text TimeField;
    public TMP_Text ScoreField;

    public void SetValues (string username, string time, string score, string rank)
    {
        UsernameField.text = username;
        TimeField.text = time;
        ScoreField.text = score;
        RankField.text = rank;
    }
    
    public void SetValues (string username, int time, int score, string rank)
    {
        UsernameField.text = username;
        TimeField.text = (time / 1000f).ToString ("F2");
        ScoreField.text = score.ToString ();
        RankField.text = "#" + rank;

    }
}

using TMPro;
using UnityEngine;

public class HighScoreElement : MonoBehaviour
{
    public TMP_Text UsernameField;
    public TMP_Text TimeField;
    public TMP_Text ScoreField;

    public void SetValues (string username, string time, string score)
    {
        UsernameField.text = username;
        TimeField.text = time;
        ScoreField.text = score;
    }
    
    public void SetValues (string username, int time, int score)
    {
        UsernameField.text = username;
        TimeField.text = (time / 1000f).ToString ("F2");
        ScoreField.text = score.ToString ();
    }
}

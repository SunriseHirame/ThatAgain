using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class DreamloLeaderBoard : MonoBehaviour
{
    public static DreamloLeaderBoard Instance { get; private set; }

    string dreamloWebserviceURL = "http://dreamlo.com/lb/";

    private const string privateCode = "cBM60QmPMUq2FFMTvF-pCwXfRCPGo2E0uM3D1gFPxbLQ";
    private const string publicCode = "5cc460c73eba951290eecc84";

    string highScores = "";

    ////////////////////////////////////////////////////////////////////////////////////////////////

    // A player named Carmine got a score of 100. If the same name is added twice, we use the higher score.
    // http://dreamlo.com/lb/(your super secret very long code)/add/Carmine/100

    // A player named Carmine got a score of 1000 in 90 seconds.
    // http://dreamlo.com/lb/(your super secret very long code)/add/Carmine/1000/90

    // A player named Carmine got a score of 1000 in 90 seconds and is Awesome.
    // http://dreamlo.com/lb/(your super secret very long code)/add/Carmine/1000/90/Awesome

    ////////////////////////////////////////////////////////////////////////////////////////////////


    public struct Score
    {
        public string playerName;
        public int score;
        public int seconds;
        public string shortText;
        public string dateString;
    }

    private void Start ()
    {
        this.highScores = "";
    }

    public static DreamloLeaderBoard GetSceneDreamloLeaderboard ()
    {
        return Instance;
    }


    public static double DateDiffInSeconds (System.DateTime now, System.DateTime olderdate)
    {
        var difference = now.Subtract (olderdate);
        return difference.TotalSeconds;
    }

    System.DateTime _lastRequest = System.DateTime.Now;
    int _requestTotal = 0;


    private bool TooManyRequests ()
    {
        var now = System.DateTime.Now;

        if (DateDiffInSeconds (now, _lastRequest) <= 2)
        {
            _lastRequest = now;
            _requestTotal++;
            if (_requestTotal > 3)
            {
                Debug.LogError ("DREAMLO Too Many Requests. Am I inside an update loop?");
                return true;
            }
        }
        else
        {
            _lastRequest = now;
            _requestTotal = 0;
        }

        return false;
    }

    public void AddScore (string playerName, int totalScore)
    {
        if (TooManyRequests ()) return;

        StartCoroutine (AddScoreWithPipe (playerName, totalScore));
    }

    public void AddScore (string playerName, int totalScore, int totalSeconds)
    {
        if (TooManyRequests ()) return;

        StartCoroutine (AddScoreWithPipe (playerName, totalScore, totalSeconds));
    }

    public void AddScore (string playerName, int totalScore, int totalSeconds, string shortText)
    {
        if (TooManyRequests ()) return;

        StartCoroutine (AddScoreWithPipe (playerName, totalScore, totalSeconds, shortText));
    }

    // This function saves a trip to the server. Adds the score and retrieves results in one trip.
    IEnumerator AddScoreWithPipe (string playerName, int totalScore)
    {
        playerName = Clean (playerName);

        WWW www = new WWW (dreamloWebserviceURL + privateCode + "/add-pipe/" + WWW.EscapeURL (playerName) + "/" +
                           totalScore.ToString ());
        yield return www;
        highScores = www.text;
    }

    IEnumerator AddScoreWithPipe (string playerName, int totalScore, int totalSeconds)
    {
        playerName = Clean (playerName);

        WWW www = new WWW (dreamloWebserviceURL + privateCode + "/add-pipe/" + WWW.EscapeURL (playerName) + "/" +
                           totalScore.ToString () + "/" + totalSeconds.ToString ());
        yield return www;
        highScores = www.text;
    }

    IEnumerator AddScoreWithPipe (string playerName, int totalScore, int totalSeconds, string shortText)
    {
        playerName = Clean (playerName);
        shortText = Clean (shortText);

        WWW www = new WWW (dreamloWebserviceURL + privateCode + "/add-pipe/" + WWW.EscapeURL (playerName) + "/" +
                           totalScore.ToString () + "/" + totalSeconds.ToString () + "/" + shortText);
        yield return www;
        highScores = www.text;
    }

    IEnumerator GetScores (System.Action callback)
    {
        highScores = "";
        WWW www = new WWW (dreamloWebserviceURL + publicCode + "/pipe");
        yield return www;
        highScores = www.text;
        callback?.Invoke ();
    }

    IEnumerator GetSingleScore (string playerName)
    {
        highScores = "";
        WWW www = new WWW (dreamloWebserviceURL + publicCode + "/pipe-get/" + WWW.EscapeURL (playerName));
        yield return www;
        highScores = www.text;
    }

    public void LoadScores (System.Action callback)
    {
        if (TooManyRequests ()) return;
        StartCoroutine (GetScores (callback));
    }


    public string[] ToStringArray ()
    {
        if (this.highScores == null) return null;
        if (this.highScores == "") return null;

        string[] rows = this.highScores.Split (new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        return rows;
    }

    public List<Score> ToListLowToHigh ()
    {
        Score[] scoreList = this.ToScoreArray ();

        if (scoreList == null) return new List<Score> ();

        List<Score> genericList = new List<Score> (scoreList);

        genericList.Sort ((x, y) => x.score.CompareTo (y.score));

        return genericList;
    }

    public List<Score> ToListHighToLow ()
    {
        Score[] scoreList = this.ToScoreArray ();

        if (scoreList == null) return new List<Score> ();

        List<Score> genericList = new List<Score> (scoreList);

        genericList.Sort ((x, y) => y.score.CompareTo (x.score));

        return genericList;
    }

    public Score[] ToScoreArray ()
    {
        string[] rows = ToStringArray ();
        if (rows == null) return null;

        int rowcount = rows.Length;

        if (rowcount <= 0) return null;

        Score[] scoreList = new Score[rowcount];

        for (int i = 0; i < rowcount; i++)
        {
            string[] values = rows[i].Split (new char[] {'|'}, System.StringSplitOptions.None);

            Score current = new Score ();
            current.playerName = values[0];
            current.score = 0;
            current.seconds = 0;
            current.shortText = "";
            current.dateString = "";
            if (values.Length > 1) current.score = CheckInt (values[1]);
            if (values.Length > 2) current.seconds = CheckInt (values[2]);
            if (values.Length > 3) current.shortText = values[3];
            if (values.Length > 4) current.dateString = values[4];
            scoreList[i] = current;
        }

        return scoreList;
    }


    // Keep pipe and slash out of names

    private string Clean (string s)
    {
        s = s.Replace ("/", "");
        s = s.Replace ("|", "");
        return s;
    }

    private int CheckInt (string s)
    {
        int x = 0;

        int.TryParse (s, out x);
        return x;
    }

    #if UNITY_EDITOR
    [InitializeOnLoadMethod]
    #endif
    [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init ()
    {
        if (Instance)
            return;
        Instance = new GameObject ("Leaderboard").AddComponent<DreamloLeaderBoard> ();
        Instance.gameObject.hideFlags = HideFlags.HideAndDontSave;
    }
}
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[ExecuteAlways]
public class DisplayLeaderboard : MonoBehaviour
{
    [SerializeField]
    private Transform container;

    private List<HighScoreElement> displayElements = new List<HighScoreElement> ();
    private void OnEnable ()
    {
        var childCount = container.childCount;
        displayElements.Clear ();
        
        for (var i = 0; i < childCount; i++)
        {
            var element = container.GetChild (i).GetComponent<HighScoreElement> ();
            if (element)
                displayElements.Add (element);
        }
        
        DreamloLeaderBoard.Instance.LoadScores (OnScoresLoaded);
    }

    private void OnScoresLoaded ()
    {
        var sorted = DreamloLeaderBoard.Instance.ToListHighToLow ();
        var shownElements = math.min (displayElements.Count, sorted.Count);
        for (var i = 0; i < displayElements.Count; i++)
        {
            displayElements[i].gameObject.SetActive (i < shownElements);
            
            if (i < shownElements)
                displayElements[i].SetValues (sorted[i].playerName, (sorted[i].seconds), sorted[i].score);
        }
    }
}

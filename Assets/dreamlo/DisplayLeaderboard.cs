using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[ExecuteAlways]
public class DisplayLeaderboard : MonoBehaviour
{
    [SerializeField] private DreamloBoard boardToLoad;   
    [SerializeField]private Transform container;
    [SerializeField] private int StartFrom = 0;
    
    
    private int shownElements;
    private List<DreamloLeaderBoard.Score> sorted = new List<DreamloLeaderBoard.Score>();
    private List<HighScoreElement> displayElements = new List<HighScoreElement> ();
    private void OnEnable ()
    {
       RefreshScores();
    }

    private void OnScoresLoaded ()
    {
        sorted = DreamloLeaderBoard.Instance.ToListHighToLow ();
        shownElements = math.min (displayElements.Count, sorted.Count - StartFrom);
        if (shownElements < 0)
        {
            shownElements = 0;
        }
        print("elements" + shownElements);
        print("start" + StartFrom);
        for (var i = 0; i < displayElements.Count; i++)
        {
            displayElements[i].gameObject.SetActive (i < shownElements);
            
            if (i < shownElements)
                displayElements[i].SetValues (
                    sorted[i + StartFrom].playerName,
                    (sorted[i + StartFrom].seconds), 
                    sorted[i + StartFrom].score,
                    ( StartFrom +i+1).ToString()
                    );
        }
    }

    public void NextPage()
    {
        StartFrom += displayElements.Count;
        print("Total score: " + DreamloLeaderBoard.Instance.ToListHighToLow ().Count);
        OnScoresLoaded();
    }

    public void PreviousPage()
    {
        StartFrom -= displayElements.Count;
        OnScoresLoaded();
    }

    private void RefreshScores()
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
    
    
    
    
}

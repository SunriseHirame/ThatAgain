using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager backgroundManager
    {
        get
        {
            return bgm;
        }
    }

    private static BackgroundManager bgm = null;

    public int completionsPerChange = 3;
    [SerializeField]
    private int currentScoreStep = 0;
    [SerializeField]
    private int changeIndex;

    /// <summary>
    /// Reference to forest element containers. Insert them in the order they should change.
    /// </summary>
    public GameObject[] backgroundForestElements;
    /// <summary>
    /// Reference to city element containers. Insert them in the order they should change.
    /// </summary>
    public GameObject[] backgroundCityElements;

    public bool testToggle = false;

    // Start is called before the first frame update
    void Start()
    {
        if (bgm == null)
        {
            bgm = this;
        }

        ResetBackground();
    }

    // Update is called once per frame
    void Update()
    {
        if(testToggle)
        {
            testToggle = false;
            LevelCompleted();
        }
    }

    /// <summary>
    /// Call this everytime the level is successfully completed.
    /// </summary>
    public void LevelCompleted()
    {
        currentScoreStep++;
        if (currentScoreStep == completionsPerChange)
        {
            currentScoreStep %= completionsPerChange;
            ManageBackground();
            changeIndex++;
        }
    }

    /// <summary>
    /// Call this to reset the background.
    /// </summary>
    public void ResetBackground()
    {
        currentScoreStep = 0;
        changeIndex = 0;

        foreach (GameObject fgo in backgroundForestElements)
        {
            fgo.SetActive(true);
        }

        foreach (GameObject cgo in backgroundCityElements)
        {
            cgo.SetActive(false);
        }
    }

    /// <summary>
    /// Makes the next change in background
    /// </summary>
    private void ManageBackground()
    {
        if (changeIndex < backgroundForestElements.Length)
        {
            backgroundForestElements[changeIndex].SetActive(false);
            backgroundCityElements[changeIndex].SetActive(true);
        }
    }
}

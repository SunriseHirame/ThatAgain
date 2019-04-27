using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRounds : MonoBehaviour
{
    private Text roundText; 
    [SerializeField] private PositionRecorder posRec;


    private void OnEnable()
    {
        PositionRecorder.OnLevelFinished += Refresh;
    }

    private void OnDisable()
    {
        PositionRecorder.OnLevelFinished -= Refresh;
    }

    private void Start()
    {
        roundText = GetComponent<Text>();
        roundText.text = "Round " + (posRec.FinishCount + 1).ToString();
    }

    private void Refresh()
    {
        roundText.text = "Round " + (posRec.FinishCount + 1).ToString();
    }
}

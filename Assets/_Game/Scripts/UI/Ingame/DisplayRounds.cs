using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRounds : MonoBehaviour
{
    [SerializeField] private Text roundText;
    [SerializeField] private Text enemiesText;
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
        Refresh();
    }

    private void Refresh()
    {
        roundText.text = "Round " + (posRec.FinishCount + 1).ToString();
        enemiesText.text = "enemies: " + (posRec.FinishCount).ToString();
    }
}

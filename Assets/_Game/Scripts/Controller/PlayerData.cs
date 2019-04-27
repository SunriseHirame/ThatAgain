using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Game;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private PositionRecorder positionRecorder;
    [SerializeField] private TimeTracker timeTracker;
    [SerializeField] private string name;

    public void PushScore()
    {
        DreamloLeaderBoard.Instance.AddScore(name,positionRecorder.FinishCount,(int)(timeTracker.totalTime*1000));
    }

}

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
        name = PlayerPrefs.GetString("PlayerName", "NO_PLAYER_NAME_ADDED_EVERYTHINGS_BROKEN");
        DreamloLeaderBoard.Instance.AddScore(name,positionRecorder.FinishCount,(int)(timeTracker.totalTime*1000));
        print("Player data saved. Name: " + name + " Finish count: " + positionRecorder.FinishCount);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    private GhostSpawner spawner;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PositionRecorder positionRecorder = other.GetComponent<PositionRecorder>();
        if (positionRecorder != null)
        {
            positionRecorder.ReturnToStartPosition(); //send player to start
            spawner.ResetGhosts();
        }
    }

    public void setSpawner(GhostSpawner s)
    {
        spawner = s;
    }
}

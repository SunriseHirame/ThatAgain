using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPlayback : MonoBehaviour
{
    
    private Vector3[] positionHistory;
    private int positionIter;

    public void SetPositionHistory(Vector3[] history)
    {
        positionHistory = history;
        positionIter = positionHistory.Length;
    }

    private void FixedUpdate()
    {
        if (positionIter > 0)
        {
            positionIter--;
            transform.position = positionHistory[positionIter];
        }
    }

    public void ResetGhost()
    {
        positionIter = positionHistory.Length;
    }
}

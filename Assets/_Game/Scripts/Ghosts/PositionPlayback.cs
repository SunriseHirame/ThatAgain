using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPlayback : MonoBehaviour
{
    
    private Vector3[] positionHistory;
    private int positionIter;
    private bool pong;

    private void Start()
    {
        gameObject.SetActive(false); //keep new ghost disabled until reset
    }

    public void SetPositionHistory(Vector3[] history)
    {
        positionHistory = history;
        positionIter = positionHistory.Length;
    }

    private void FixedUpdate()
    {
        if (pong)
        {
            if (positionIter < positionHistory.Length-1)
            {
                positionIter++;
                transform.position = positionHistory[positionIter];
            }
            else
            {
                pong = false;
                //gameObject.SetActive(false);
            }
        }
        else
        {
            if (positionIter > 0)
            {
                positionIter--;
                transform.position = positionHistory[positionIter];
            }
            else
            {
                pong = true;
                //gameObject.SetActive(false);
            }
        }
    }

    public void ResetGhost()
    {
        positionIter = positionHistory.Length;
    }
}

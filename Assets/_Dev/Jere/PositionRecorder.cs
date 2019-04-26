using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRecorder : MonoBehaviour
{
    private List<Vector3> recordedPos;
    private void FixedUpdate()
    {
        recordedPos.Add(transform.position);
    }
    
    public List<Vector3> GetPositionHistory()
    {
        return recordedPos;
    }

    public void ClearRecordedPosition()
    {
        recordedPos.Clear();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionRecorder : MonoBehaviour
{
    private List<Vector3> recordedPos = new List<Vector3>();
    private Vector3 startPosition;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        GameRoundController.Instance.AddPlayer(this);
    }

    private void FixedUpdate()
    {
        recordedPos.Add(transform.position);
    }

    public void ReturnToStartPosition()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        ClearRecordedPosition();
    }
    
    public Vector3[] GetPositionHistory()
    {
        return recordedPos.ToArray();
    }
    
    public void ClearRecordedPosition()
    {
        recordedPos.Clear();
    }

    
}

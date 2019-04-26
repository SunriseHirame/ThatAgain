using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ghost;
    private List<PositionPlayback> ghosts;


    private void Awake()
    {
        ghosts = new List<PositionPlayback>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PositionRecorder posRec = other.GetComponent<PositionRecorder>();
        
        if (posRec != null)
        {
            posRec.ReturnToStartPosition(); //send player back to start
            SpawnGhost(posRec.GetPositionHistory()); //spawn new ghost
            ResetGhosts(); //send all ghosts to their start position
            posRec.ClearRecordedPosition();
        }
    }

    private void SpawnGhost(Vector3[] positionHistory)
    {
        //spawn ghost at last point in history
        GameObject spawnedGhost = Instantiate(ghost, positionHistory[positionHistory.Length - 1], quaternion.identity);
        PositionPlayback spawnedPlayback = spawnedGhost.GetComponent<PositionPlayback>();
        spawnedPlayback.SetPositionHistory(positionHistory);
        spawnedGhost.GetComponent<PlayerKiller>().setSpawner(this);
        ghosts.Add(spawnedPlayback); //add playback to list of all playbacks for reseting.
    }

    public void ResetGhosts()
    {
        foreach (PositionPlayback g in ghosts)
        {
            g.ResetGhost();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ghost;
    private List<PositionPlayback> ghosts = new List<PositionPlayback>();


    private void Start()
    {
        GameRoundController.Instance.SetGoalObject(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PositionRecorder player = other.GetComponent<PositionRecorder>();
        
        if (player != null)
        {
            player.gameObject.SetActive(false); //disable player
            GameRoundController.Instance.PlayerFinished(); //count finished players
            SpawnGhost(player.GetPositionHistory()); //spawn new ghost
            ResetGhosts(); //send all ghosts to their start position
            player.ClearRecordedPosition();
        }
    }

    private void SpawnGhost(Vector3[] positionHistory)
    {
        //spawn ghost at last point in history
        GameObject spawnedGhost = Instantiate(ghost, positionHistory[positionHistory.Length - 1], quaternion.identity);
        PositionPlayback spawnedPlayback = spawnedGhost.GetComponent<PositionPlayback>();
        spawnedPlayback.SetPositionHistory(positionHistory);
        ghosts.Add(spawnedPlayback); //add playback to list of all playbacks for resetting.
        spawnedGhost.SetActive(false); //keep new ghost disabled until reset
    }

    public void ResetGhosts()
    {
        foreach (PositionPlayback g in ghosts)
        {
            g.gameObject.SetActive(true);
            g.ResetGhost();
        }
    }

    public void ClearGhosts()
    {
        ghosts.Clear();
    }
}

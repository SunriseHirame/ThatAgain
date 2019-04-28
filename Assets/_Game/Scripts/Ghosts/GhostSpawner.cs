using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Game
{
    public class GhostSpawner : MonoBehaviour
    {
        public static GhostSpawner Instance { get; private set; }

        [SerializeField] private GameObject ghost;
        private List<PositionPlayback> ghosts = new List<PositionPlayback>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GameRoundController.Instance.SetGoalObject(this);
        }

        private void OnTriggerEnter2D(Collider2D other) //player enters finish line
        {
            PositionRecorder player = other.attachedRigidbody.GetComponent<PositionRecorder>();

            if (player != null && !player.IsFinished() && !player.IsDead())
            {
                SpawnGhost(player.GetPositionHistory()); //spawn new ghost
                player.FinishLevel();
            }
        }


        private void SpawnGhost(Vector3[] positionHistory)
        {
            //spawn ghost at last point in history
            GameObject spawnedGhost =
                Instantiate(ghost, positionHistory[positionHistory.Length - 1], quaternion.identity);
            PositionPlayback spawnedPlayback = spawnedGhost.GetComponent<PositionPlayback>();
            spawnedPlayback.SetPositionHistory(positionHistory);
            ghosts.Add(spawnedPlayback); //add playback to list of all playbacks for resetting.
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
}
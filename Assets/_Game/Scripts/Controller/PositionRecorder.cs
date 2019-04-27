using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PositionRecorder : MonoBehaviour
    {
        [SerializeField] private TimeTracker timeTracker;
        private List<Vector3> recordedPos = new List<Vector3> ();
        private Vector3 startPosition;
        private Rigidbody2D rb;
        private bool dead;
        private bool finished;

        public int DeathCount { get; private set; }
        public int FinishCount { get; private set; }

        private void Start ()
        {
            rb = GetComponent<Rigidbody2D> ();
            startPosition = transform.position;
            GameRoundController.Instance.AddPlayer (this);
            timeTracker.StartCounting();
        }

        private void FixedUpdate ()
        {
            recordedPos.Add (transform.position);
        }

        public void ReturnToStartPosition ()
        {
            dead = false;
            finished = false;
            rb.velocity = Vector2.zero;
            transform.position = startPosition;
            ClearRecordedPosition ();
        }

        public void Die ()
        {
            GameRoundController.Instance.PlayerDied ();
            ClearRecordedPosition ();
            gameObject.SetActive (false);
            dead = true;
            DeathCount++;
        }

        public bool IsDead ()
        {
            return dead;
        }

        public void FinishLevel ()
        {
            timeTracker.SetTime();
            finished = true;
            GameRoundController.Instance.PlayerFinished (); //count finished players
            ClearRecordedPosition ();
            gameObject.SetActive (false);
            FinishCount++;
        }

        public bool IsFinished ()
        {
            return finished;
        }

        public Vector3[] GetPositionHistory ()
        {
            return recordedPos.ToArray ();
        }

        private void ClearRecordedPosition ()
        {
            recordedPos.Clear ();
        }
    }
}
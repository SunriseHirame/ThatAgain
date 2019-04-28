using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Game
{
    public class PositionRecorder : MonoBehaviour
    {
        [SerializeField] private TimeTracker timeTracker;
        [SerializeField] private GameObject deathParticleSystem;
        [SerializeField] private GameObject ragdoll;
        [SerializeField] private GameObject graphics;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private DeathZoom zoom;
        [SerializeField] private LayeredAudioPlayer layeredAudioPlayer;

        public UnityEvent Death;
        
        private List<Vector3> recordedPos = new List<Vector3> ();
        private Vector3 startPosition;
        private Rigidbody2D rb;
        private bool dead;
        private bool finished;
        public static System.Action OnLevelFinished;
        public static System.Action OnPlayerLoseHealth;

        public int DeathCount { get; private set; }
        public int FinishCount { get; private set; }

        private void Start ()
        {
            GameRoundController.Instance.StartGame();
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
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<Animator>().enabled = true;
            GetComponent<RagdollController>().SetRagdolling(false);
            dead = false;
            finished = false;
            rb.velocity = Vector2.zero;
            transform.position = startPosition;
            ClearRecordedPosition ();
            zoom.ResetZoom();
        }

        public void Die ()
        {
            dead = true;
            DeathCount++;
            DeathEffects();
            Invoke(nameof(Disable),3);
            GetComponent<Animator>().enabled = false;
            GetComponent<RagdollController>().SetRagdolling(true);
            GetComponent<BoxCollider2D>().enabled = false;
            
            OnPlayerLoseHealth?.Invoke();
            if (DeathCount >= 5)
            {
                GameRoundController.Instance.EndGame();
                
            }
        }

        private void DeathEffects()
        {
            //graphics.SetActive(false);
            deathParticleSystem.GetComponent<ParticleSystem>().Play();
            //GameObject rag = Instantiate(ragdoll, transform.position+Vector3.up, quaternion.identity);
            zoom.target = this.transform;
            zoom.Zoom();
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1.0f,1.0f)*100f,Random.Range(-1.0f,1.0f)*100f);
        }
        
        private void Disable()
        {
            //gameObject.SetActive (false);
            GameRoundController.Instance.PlayerDied ();
        }

        public bool IsDead ()
        {
            return dead;
        }

        public void FinishLevel ()
        {
            layeredAudioPlayer.ActivateNext();
            timeTracker.SetTime();
            finished = true;
            GameRoundController.Instance.PlayerFinished (); //count finished players
            //gameObject.SetActive (false);
            FinishCount++;
            OnLevelFinished?.Invoke();
            playerData.PushScore();
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

        private void OnDestroy()
        {
            GameRoundController.Instance.RemovePlayer(this);
        }
    }
}
﻿using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
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
            graphics.SetActive(true);
            dead = false;
            finished = false;
            rb.velocity = Vector2.zero;
            transform.position = startPosition;
            ClearRecordedPosition ();
        }

        public void Die ()
        {
            ClearRecordedPosition ();
            dead = true;
            DeathCount++;
            DeathEffects();
            Invoke(nameof(Disable),3);
        }

        private void DeathEffects()
        {
            graphics.SetActive(false);
            deathParticleSystem.GetComponent<ParticleSystem>().Play();
            GameObject rag = Instantiate(ragdoll, transform.position+Vector3.up, quaternion.identity);
            rag.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1.0f,1.0f)*500f,Random.Range(-1.0f,1.0f)*500f);
        }
        
        private void Disable()
        {
            gameObject.SetActive (false);
            GameRoundController.Instance.PlayerDied ();
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
    }
}
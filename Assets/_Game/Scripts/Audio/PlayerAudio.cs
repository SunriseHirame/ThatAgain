using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    
    public class PlayerAudio : MonoBehaviour
    {
        public AudioClip[] FootSteps;
        public AudioClip Jump;
        public AudioClip Death;

        public AudioSource Source;
        public Rigidbody2D AttachedRigidbody;

        public KinematicController AttachedController;
        
        public float TimeBetweenSteps;
        private float lastStep;
        
        private void Update ()
        {
            if (AttachedController.OnGround && AttachedRigidbody.velocity.sqrMagnitude > 1f)
            {
                if (Time.time > lastStep + TimeBetweenSteps)
                {
                    Source.PlayOneShot (FootSteps[Random.Range (0, FootSteps.Length)]);
                    lastStep = Time.time;
                }
            }
        }

        public void PlayJump ()
        {
            Source.PlayOneShot (Jump);
        }
        
        public void PlayDeath ()
        {
            Source.PlayOneShot (Death);
        }
    }
    
}

using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MissileLauncher : MonoBehaviour
    {
        private readonly List<Transform> trackedTransforms = new List<Transform> ();

        
        [SerializeField] private HomingMissile missileProto;
        [SerializeField] private float missileDelay;
        [SerializeField] private float missileCooldown;

        [SerializeField] private LayerMask targetingLayer;

        private float lastPrimedTime;
        
        private void Update ()
        {
            var time = Time.time;
            if (trackedTransforms.Count == 0)
            {
                lastPrimedTime = time;
                return;
            }

            if (time > lastPrimedTime + missileDelay)
            {
                lastPrimedTime = time + missileCooldown;
                var missile = Instantiate (missileProto, transform.position, Quaternion.identity);
                missile.SetTarget (trackedTransforms[0]);
            }
        }

        private void OnTriggerEnter2D (Collider2D other)
        {
            var rootTransform = other.attachedRigidbody.transform;
            if ((1 << rootTransform.gameObject.layer) != targetingLayer || trackedTransforms.Contains (rootTransform))
                return;
            
            print ("Entered");
            trackedTransforms.Add (rootTransform);
        }

        private void OnTriggerExit2D (Collider2D other)
        {
            var rootTransform = other.attachedRigidbody.transform;
            if ((1 << other.gameObject.layer) != targetingLayer|| trackedTransforms.Contains (rootTransform))
                return;
            trackedTransforms.Remove (rootTransform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Game
{
    public class DeathLazer : MonoBehaviour
    {
        private readonly List<Transform> targets = new List<Transform> ();
    
        [SerializeField] private LayerMask hitMask;    
        [SerializeField] private LineRenderer lineRenderer;
    
        private void Update()
        {
            lineRenderer.enabled = targets.Count > 0;
            if (!lineRenderer.enabled)
                return;
            
            lineRenderer.SetPosition (0, transform.position);
            lineRenderer.SetPosition (1, targets[0].position);
        }

        private void OnTriggerEnter2D (Collider2D other)
        {
            if ((1 << other.gameObject.layer) != hitMask) 
                return;
            
            var otherTransform = other.attachedRigidbody.transform;

            if (targets.Contains (otherTransform))
                return;
                
            var player = otherTransform.GetComponent<Player> ();
            targets.Add (player ? player.Body : otherTransform);
        }

        private void OnTriggerExit2D (Collider2D other)
        {
            if ((1 << other.gameObject.layer) != hitMask) 
                return;
            
            var otherTransform = other.attachedRigidbody.transform;

            if (targets.Contains (otherTransform))
                return;
                
            var player = otherTransform.GetComponent<Player> ();
            targets.Remove (player ? player.Body : otherTransform);
        }
    }

}
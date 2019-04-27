using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class HomingMissile : MonoBehaviour
    {
        [SerializeField] private float speed = 4;
        [SerializeField] private float acceleration = 1;
        [SerializeField] private float turningSpeed = 90;
        [SerializeField] private Transform graphicsRoot;
        
        private Transform target;

        private Rigidbody2D attachedRigidbody;
        private float speedVelocity;
        
        public void SetTarget (Transform target)
        {
            if (this.target == false)
            {
                var direction = target.position - transform.position;
                var angle = Vector3.SignedAngle (Vector3.right, direction, Vector3.forward);
                graphicsRoot.rotation = Quaternion.Euler (0, 0, angle);
            }
            this.target = target;
        }
        
        private void Awake ()
        {
            attachedRigidbody = GetComponent<Rigidbody2D> ();
        }

        private void FixedUpdate ()
        {
            if (target == false)
                return;
            
            var dt = Time.fixedDeltaTime;
            var frameSpeed = Mathf.SmoothDamp (
                attachedRigidbody.velocity.magnitude, speed, 
                ref speedVelocity, acceleration, speed / dt);
            
            var direction = target.position - transform.position;
          
            var angle = Vector3.SignedAngle (Vector3.right, direction, Vector3.forward);
            graphicsRoot.rotation = Quaternion.RotateTowards (
                graphicsRoot.rotation, Quaternion.Euler (0, 0, angle), 
                turningSpeed * dt);
            
            attachedRigidbody.MovePosition (
                attachedRigidbody.position + 
                graphicsRoot.forward * new Vector2(direction.x, direction.y) * frameSpeed * dt);

        }

        private void OnTriggerEnter2D (Collider2D other)
        {
            Destroy (gameObject);
        }
    }

}
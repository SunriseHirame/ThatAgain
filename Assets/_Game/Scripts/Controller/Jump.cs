﻿using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private float minJumpHeight = 1;
        [SerializeField] private float maxJumpHeight = 5;
        [SerializeField] private float jumpCooldown = 0.1f;
        
        [SerializeField]
        private Rigidbody2D attachedRigidbody;

        private float lastJumpTime;
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool GetJumpVelocity (bool onGround, ref Vector2 velocity)
        {
            var gravity = Physics2D.gravity.y * attachedRigidbody.gravityScale;
            var currentTime = Time.time;
            if(!onGround || lastJumpTime + jumpCooldown > currentTime)
            {             
                return false;
            }
            
            //gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
            //maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            //minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);         
            // Mathf.Pow (timeToJumpApex, 2) * gravity = -2 * maxJumpHeight 
            
            lastJumpTime = currentTime;
        
            var timeToJumpApex = Mathf.Sqrt (-2 * maxJumpHeight / gravity);
            var maxJumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
            velocity.y = maxJumpVelocity; 
            return true;
        }


    }

}

using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private float minJumpHeight = 3.5f;
        [SerializeField] private float maxJumpHeight = 5;
        [SerializeField] private float jumpCooldown = 0.05f;
        
        [SerializeField]
        private Rigidbody2D attachedRigidbody;

        private float lastJumpTime;
        private int jumpCount;

        public void ResetJumpCounter ()
        {
            jumpCount = 0;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool GetJumpVelocity (bool onGround, ref Vector2 velocity)
        {
            var gravity = Physics2D.gravity.y * attachedRigidbody.gravityScale;
            var currentTime = Time.time;
                        
            if (lastJumpTime + jumpCooldown > currentTime)
            {
                return false;
            }

            if (onGround)
            {
                jumpCount = 0;
            }
            
            if (++jumpCount > 2)
                return false;

            
            //gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
            //maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            //minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);         
            // Mathf.Pow (timeToJumpApex, 2) * gravity = -2 * maxJumpHeight 
            
            lastJumpTime = currentTime;
            
            var timeToJumpApex = Mathf.Sqrt (-2 * maxJumpHeight / gravity);
            var maxJumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
            velocity.y = Mathf.Clamp (velocity.y + maxJumpVelocity, minJumpHeight, maxJumpVelocity);
            return true;
        }


    }

}

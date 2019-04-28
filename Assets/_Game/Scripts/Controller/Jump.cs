using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Game
{
    public class Jump : MonoBehaviour
    {
        [Header ("Base Settings")]
        [SerializeField] private float minJumpHeight = 3.5f;
        [SerializeField] private float maxJumpHeight = 5;
        [SerializeField] private float jumpCooldown = 0.05f;

        [Header ("Air Jumping")]
        [SerializeField] private bool allowAirJump;

        [Header ("Wall Jumping")]
        [SerializeField] private bool allowWallJump;
        [SerializeField] private float wallJumpAngle = 30f;
        
        [SerializeField]
        private Rigidbody2D attachedRigidbody;

        private float lastJumpTime;
        private int jumpCount;

        public void ResetJumpCounter ()
        {
            jumpCount = 0;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool GetJumpVelocity (in SurfaceInfo surfaceInfo, ref Vector2 velocity)
        {
            var currentTime = Time.time;
                        
            if (lastJumpTime + jumpCooldown > currentTime)
            {
                return false;
            }

            if (surfaceInfo.OnGround)
            {
                jumpCount = 0;
            }
            
            if (++jumpCount > (allowAirJump ? 2 : 1))
                return false;

            if (surfaceInfo.OnGround == false && surfaceInfo.OnWall)
            {
                print ("Wall Jump!");
                var v = GetJumpVelocity (in velocity);
                velocity.y = math.cos (wallJumpAngle * Mathf.Deg2Rad) * v;

                var xAdditionalVelocity = math.sin (wallJumpAngle * Mathf.Deg2Rad) * v;
                velocity.x += (surfaceInfo.Collisions & CollisionFlags.Left) != 0
                    ? xAdditionalVelocity
                    : -xAdditionalVelocity;
                return true;
            }
            
            //gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
            //maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            //minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);         
            // Mathf.Pow (timeToJumpApex, 2) * gravity = -2 * maxJumpHeight 
            
            lastJumpTime = currentTime;

            velocity.y = GetJumpVelocity (in velocity);
            return true;
        }

        private float GetJumpVelocity (in Vector2 velocity)
        {            
            var gravity = Physics2D.gravity.y * attachedRigidbody.gravityScale;
            var timeToJumpApex = Mathf.Sqrt (-2 * maxJumpHeight / gravity);
            var maxJumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
            return Mathf.Clamp (velocity.y + maxJumpVelocity, minJumpHeight, maxJumpVelocity);
        }
    }

}

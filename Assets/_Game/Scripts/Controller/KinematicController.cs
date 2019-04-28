using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
 
namespace Game
{
    public class KinematicController : MonoBehaviour
    {
        private enum WallCling { Left, Right }
        
        [SerializeField]
        private float speed = 3;

        [SerializeField] private float moveDrag = 0;
        [SerializeField] private float idleDrag = 1;
        
        [SerializeField, Range(0, 1)]
        private float airControl = 1f;

        [SerializeField, Min (0)] private float smoothing = 1f;
        [SerializeField, Min(0)] private float wallClingTime = 0.25f;
        
        private float inputX;
        private bool jumpInput;        

        [SerializeField] private Rigidbody2D attachedRigidbody;
        [SerializeField] private SurfaceChecker surfaceCheck;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Jump jump;

        
        private float fallMultiplier = 2.5f;
        private float lowJumpMultiplier = 1.5f;
        private WallCling lastWallCling;
        private bool isWallClinging;
    
        // This is just to show this in inspector
        [SerializeField]
        private SurfaceInfo surfaceInfo;

        public bool OnGround => surfaceInfo.OnGround;
        
        private float damperValue;
        private float wallClingStart;
        
        private void Update ()
        {
            inputX = playerInput.GetMovement ().x;
            if (jumpInput == false)
                jumpInput = playerInput.GetJump ();    
        }

        private void FixedUpdate ()
        {   
            surfaceInfo = surfaceCheck.CheckCollisions ();

            attachedRigidbody.drag = Math.Abs (inputX) > 0.1f ? moveDrag : GetIdleDrag ();
            
            var dt = Time.fixedDeltaTime;
            var currentVelocity = attachedRigidbody.velocity;
            var airMult = surfaceInfo.OnGround ? 1f : airControl;
            currentVelocity.x = Mathf.SmoothDamp (
                currentVelocity.x, inputX * speed, ref damperValue, 
                smoothing / airMult, speed / dt, dt);

            
            
            //HandleWallCling (ref currentVelocity);
            HandleJump (ref currentVelocity);
            
            attachedRigidbody.velocity = currentVelocity;
        }

        private float GetIdleDrag ()
        {
            return attachedRigidbody.velocity.sqrMagnitude > 0.2f ? attachedRigidbody.drag : idleDrag;
        }
        
        private void HandleWallCling (ref Vector2 currentVelocity)
        {
            if (surfaceInfo.OnGround)
            {
                isWallClinging = false;
                return;
            }
            
            if (isWallClinging && wallClingStart + wallClingTime <= Time.time)
            {
                isWallClinging = false;
                return;
            }         

            if (inputX > 0.1f && surfaceInfo.SurfaceAngles.RightWallAngle > 70)
            {
                currentVelocity.y = 0f;
                jump.ResetJumpCounter ();
                return;
            }
            
            if (inputX < -0.1f && surfaceInfo.SurfaceAngles.LeftWallAngle > 70)
            {
                currentVelocity.y = 0f;
                jump.ResetJumpCounter ();
                return;
            }
        }

        private void HandleJump (ref Vector2 currentVelocity)
        {
            if (!jumpInput) 
                return;
            
            jumpInput = false;
            jump.GetJumpVelocity (surfaceInfo.OnGround, ref currentVelocity);
        }

    }

}

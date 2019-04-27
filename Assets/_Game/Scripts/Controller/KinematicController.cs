using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
 
namespace Game
{
    public class KinematicController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3;

        [SerializeField]
        internal float jumpHeight = 5;

        [SerializeField, Range(0, 1)]
        private float airControl = 1f;

        [SerializeField, Min (0)] private float smoothing = 1f;
        
        private float inputX;
        private bool jumpInput;
        private bool inputConsumed;
        

        [SerializeField] private Rigidbody2D attachedRigidbody;
        [SerializeField] private SurfaceChecker surfaceCheck;
        [SerializeField] private PlayerInput playerInput;
        

        private float fallMultiplier = 2.5f;
        private float lowJumpMultiplier = 1.5f;

        // This is just to show this in inspector
        [SerializeField]
        private SurfaceInfo surfaceInfo;

        private float damperValue;
        private void Update ()
        {
            if (!inputConsumed)
                return;
            inputConsumed = false;
            
            inputX = playerInput.GetMovement ().x;
            jumpInput = playerInput.GetJump ();
        }

        private void FixedUpdate ()
        {   
            inputConsumed = true;
            var dt = Time.fixedDeltaTime;
            var currentVelocity = attachedRigidbody.velocity;
            var airMult = surfaceInfo.OnGround ? 1f : airControl;
            var xVelocity = Mathf.SmoothDamp (
                currentVelocity.x, inputX * speed, ref damperValue, 
                smoothing / airMult, speed / dt, dt);      
                        
            if (currentVelocity.y < 0)
            {
                currentVelocity.y += Vector2.up.y * Physics2D.gravity.y * (fallMultiplier -1) * Time.deltaTime;
            } 
            else if(!Input.GetButtonDown("Jump"))
            {
                currentVelocity.y += Vector2.up.y * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            
            var frameVelocity = new Vector2 (
                xVelocity, 
                jumpInput ?  GetJumpHeight () : 0
                );

            frameVelocity.y += currentVelocity.y;
            
            surfaceInfo = surfaceCheck.CheckCollisions ();   
            attachedRigidbody.velocity = frameVelocity;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        private float GetJumpHeight ()
        {
            if(surfaceInfo.OnGround)
                return Mathf.Sqrt (jumpHeight * -10 * Physics2D.gravity.y * Time.fixedDeltaTime);
            return 0;
        }

    }

}

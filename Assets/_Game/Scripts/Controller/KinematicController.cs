using System.Runtime.CompilerServices;
using UnityEngine;
 
namespace Game
{
    public class KinematicController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3;

        [SerializeField]
        internal float jumpHeight = 5;

        [SerializeField]
        private float airControl = 1f;
        
        private float inputX;
        private bool jumpInput;
        private bool inputConsumed;
        

        [SerializeField] private Rigidbody2D attachedRigidbody;
        [SerializeField] private SurfaceChecker surfaceCheck;

        private float fallMultiplier = 2.5f;
        private float lowJumpMultiplier = 1.5f;

        // This is just to show this in inspector
        [SerializeField]
        private SurfaceInfo surfaceInfo;
        
        private void Update ()
        {
            if (!inputConsumed)
                return;
            inputConsumed = false;
            
            inputX = Input.GetAxisRaw ("Horizontal");
            jumpInput = Input.GetAxisRaw("Jump") > 0.1f;
        }

        private void FixedUpdate ()
        {   
            inputConsumed = true;
            var dt = Time.fixedDeltaTime;
            var yVelocity = attachedRigidbody.velocity.y;

            if (yVelocity < 0)
            {
                yVelocity += Vector2.up.y * Physics2D.gravity.y * (fallMultiplier -1) * Time.deltaTime;
            } 
            else if(yVelocity > 0 && !Input.GetButtonDown("Jump"))
            {
                yVelocity += Vector2.up.y * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            
            var frameVelocity = new Vector2 (
                inputX * speed, 
                jumpInput ?  GetJumpHeight () : 0
                );

            frameVelocity.y += yVelocity;
            
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

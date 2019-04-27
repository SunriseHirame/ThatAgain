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
            return Mathf.Sqrt (jumpHeight * -2 * Physics2D.gravity.y * Time.fixedDeltaTime);
        }

    }

}

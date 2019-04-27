using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
 
namespace Game
{
    public class KinematicController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3;

        [SerializeField, Range(0, 1)]
        private float airControl = 1f;

        [SerializeField, Min (0)] private float smoothing = 1f;
        
        private float inputX;
        private bool jumpInput;        

        [SerializeField] private Rigidbody2D attachedRigidbody;
        [SerializeField] private SurfaceChecker surfaceCheck;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Jump jump;

        private float fallMultiplier = 2.5f;
        private float lowJumpMultiplier = 1.5f;

        // This is just to show this in inspector
        [SerializeField]
        private SurfaceInfo surfaceInfo;

        private float damperValue;
        private void Update ()
        {
            inputX = playerInput.GetMovement ().x;
            if (jumpInput == false)
                jumpInput = playerInput.GetJump ();    
        }

        private void FixedUpdate ()
        {   
            surfaceInfo = surfaceCheck.CheckCollisions ();   

            var dt = Time.fixedDeltaTime;
            var currentVelocity = attachedRigidbody.velocity;
            var airMult = surfaceInfo.OnGround ? 1f : airControl;
            currentVelocity.x = Mathf.SmoothDamp (
                currentVelocity.x, inputX * speed, ref damperValue, 
                smoothing / airMult, speed / dt, dt);

            if (jumpInput)
            {
                jumpInput = false;
                jump.GetJumpVelocity (surfaceInfo.OnGround, ref currentVelocity);
            }
            attachedRigidbody.velocity = currentVelocity;
        }


    }

}

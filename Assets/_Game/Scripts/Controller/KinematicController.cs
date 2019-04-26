using UnityEngine;
 
namespace Game
{
    public class KinematicController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3;

        [SerializeField]
        private float jumpHeight = 5;

        [SerializeField]
        private float gravity = 20;
        
        [SerializeField]
        private float airControl = 1f;
        
        private float inputX;
        private bool jumpInput;
        
        private bool inputConsumed;

        [SerializeField] private Rigidbody2D attachedRigidbody;
        [SerializeField] private SurfaceChecker surfaceCheck;
        
        private void Update ()
        {
            if (!inputConsumed)
                return;
            inputConsumed = false;
            
            inputX = Input.GetAxisRaw ("Horizontal");
            jumpInput = Input.GetAxisRaw ("Jump") > 0.1f;
            
            
        }

        private void FixedUpdate ()
        {   
            inputConsumed = true;
            
            var dt = Time.fixedDeltaTime;
            var frameVelocity = new Vector2 (
                inputX * speed * dt, 
                jumpInput ? jumpHeight * dt : -0.02f
                ); 
            
            var surfaceInfo = surfaceCheck.CheckCollisions (ref frameVelocity);            
            attachedRigidbody.MovePosition (attachedRigidbody.position + frameVelocity);
        }

    }

}

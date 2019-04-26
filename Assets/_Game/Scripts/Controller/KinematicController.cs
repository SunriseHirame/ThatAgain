using UnityEngine;

namespace Game
{
    public class KinematicController : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float jumpHeight;
        
        private float inputX;
        private bool jumpInput;
        
        private bool inputConsumed;

        [SerializeField]
        private Rigidbody2D attachedRigidbody;
        
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
            var xMove = inputX * speed;
            var yMove = jumpInput ? jumpHeight : 0;
            
            var movement = new Vector2 (
                xMove * dt, 
                yMove * dt);
            
            attachedRigidbody.MovePosition (attachedRigidbody.position + movement);
        }
    }

}


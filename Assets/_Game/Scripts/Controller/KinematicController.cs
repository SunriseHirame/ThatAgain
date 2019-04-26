using System.Collections;
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

        private bool onGround;
        [SerializeField] private Transform groundChecker;

        //used for debugging jumping
        [SerializeField]  private float groundDistance;

        [SerializeField] private Rigidbody2D attachedRigidbody;

        [SerializeField] private LayerMask groundLayerMask;

        private void Start()
        {
            groundChecker = transform.GetChild(0);
        }
        
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
            float yMove;
            
            onGround = Physics2D.OverlapCircle(
                new Vector2(groundChecker.transform.position.x,  groundChecker.transform.position.y),
                groundDistance,groundLayerMask 
                );


            if (!onGround)
            {
                yMove = Physics2D.gravity.y * Time.deltaTime * 5;
            }
            else
            {
                yMove = 0;
            }

            if (Input.GetButtonDown("Jump") && onGround)
            {
                yMove += Mathf.Sqrt(jumpHeight * -2f * Physics2D.gravity.y);
            }
            
            var movement = new Vector2 (
                xMove * dt, 
                yMove * dt);
            
            attachedRigidbody.MovePosition (attachedRigidbody.position + movement);
        }

    }

}

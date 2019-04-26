using System.Collections;
using UnityEngine;

namespace Game
{
    public class Jump : MonoBehaviour
    {
        
        [SerializeField] private float jumpSpeed;
        
        [SerializeField] private Transform groundChecker;
        
        private float inputY;
        private bool onGround;

        private Vector3 groundPos;
        private float maxJumpHeight;
        private bool doJump;
        private KinematicController kinematicController;

        private void Start()
        {
            kinematicController = GetComponent<KinematicController>();
            groundPos = groundChecker.transform.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Jumping());
                enabled = false;
            }
        }

        private IEnumerator Jumping()
        {
            kinematicController.jumpHeight = Mathf.Sqrt(kinematicController.jumpHeight * -2f * Physics2D.gravity.y * Time.fixedDeltaTime);
            while (true)
            {
                kinematicController.jumpHeight += Physics2D.gravity.y * 2 * Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            doJump = false;
        }



    }

}

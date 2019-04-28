using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimationController : MonoBehaviour
{
    private static int Anim_MoveSpeed = Animator.StringToHash ("MoveSpeed");
    private static int Anim_InAir = Animator.StringToHash ("InTheAir");
    private static int Anim_Death = Animator.StringToHash ("Death");
    private static int Anim_Respawn = Animator.StringToHash ("Respawn");
    private static int Anim_Jump = Animator.StringToHash ("Jump");

    [SerializeField] private Animator attachedAnimator;
    [SerializeField] private Rigidbody2D attachedRigidody;

    [SerializeField] private Transform graphicsRoot;

    [SerializeField] private float speedMult = 1f;

    private bool inAir;

    private void OnEnable ()
    {
        attachedAnimator.SetTrigger (Anim_Respawn);
    }

    private void Update ()
    {
        var xVelocity = attachedRigidody.velocity.x;
        /*graphicsRoot.localRotation = quaternion.Euler (
            0f,
            xVelocity > 0.2f ? 0 : xVelocity < -0.2f ? 180 : graphicsRoot.localScale.x,
            0f
        );
*/
        graphicsRoot.localScale = new Vector3(
            xVelocity > 0.2f ? 1 : xVelocity < -0.2f ? -1 : graphicsRoot.localScale.x,
            1f,
            1f
        );

        attachedAnimator.SetFloat (Anim_MoveSpeed, Mathf.Abs(xVelocity) * speedMult);
    }
}

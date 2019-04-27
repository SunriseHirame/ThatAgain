using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private bool initialState;
    
    //private HingeJoint2D[] joints;
    private Rigidbody2D[] bodies;

    private void Awake ()
    {
        //joints = GetComponentsInChildren<HingeJoint> ();
        bodies = GetComponentsInChildren<Rigidbody2D> ();
        SetRagdolling (initialState);
    }

    public void SetRagdolling (bool state)
    {
//        foreach (var joint in joints)
//        {
//            joint.enabled = state;
//        }

        foreach (var body in bodies)
        {
            body.simulated = state;
        }
    }
}

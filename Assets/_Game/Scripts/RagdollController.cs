using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private bool initialState;

    private bool ignoreObjectsOnRoot;
    private Collider2D[] colliders;
    private Rigidbody2D[] bodies;

    private void Awake ()
    {
        //joints = GetComponentsInChildren<HingeJoint> ();
        bodies = GetComponentsInChildren<Rigidbody2D> ();
        colliders = GetComponentsInChildren<Collider2D> ();
        SetRagdolling (initialState);
    }

    public void SetRagdolling (bool state)
    {
//        foreach (var joint in joints)
//        {
//            joint.enabled = state;
//        }

        var ownTransform = transform;
        
        foreach (var body in bodies)
        {
            if (body.transform.Equals (ownTransform))
                continue;
            body.simulated = state;
        }

        foreach (var col in colliders)
        {
            if (col.transform.Equals (ownTransform))
                continue;
            col.enabled = state;
        }
    }
}

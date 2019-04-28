using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private bool initialState;

    private bool ignoreObjectsOnRoot;
    private Collider2D[] colliders;
    private Rigidbody2D[] bodies;
    private Vector3[] originalPosition;
    private float[] originalRotations;

    private void Awake ()
    {
        //joints = GetComponentsInChildren<HingeJoint> ();
        bodies = GetComponentsInChildren<Rigidbody2D> ();
        colliders = GetComponentsInChildren<Collider2D> ();

        originalPosition = new Vector3[bodies.Length];
        originalRotations = new float[bodies.Length];

        for (var i = 0; i < bodies.Length; i++)
        {
            originalPosition[i] = bodies[i].position;
            originalRotations[i] = bodies[i].rotation;
        }
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
            {
                //body.freezeRotation = !state;
                continue;
            }
            body.simulated = state;

            if (state == false)
            {
                body.velocity = Vector2.zero;
                body.angularVelocity = 0f;
            }
        }

        foreach (var col in colliders)
        {
            if (col.transform.Equals (ownTransform))
                continue;
            col.enabled = state;
        }

        if (state == false)
        {
            for (var i = 0; i < bodies.Length; i++)
            {
                bodies[i].position = originalPosition[i];
                bodies[i].rotation = originalRotations[i];
            }
        }
    }
}

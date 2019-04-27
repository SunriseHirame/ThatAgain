using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private float Force;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((1 << other.gameObject.layer) == PlayerLayer)
        {
            other.attachedRigidbody.AddForce(transform.up * Force,ForceMode2D.Impulse);
        }
    }
}

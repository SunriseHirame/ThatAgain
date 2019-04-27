using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class DeathLazer : MonoBehaviour
{
    [SerializeField] private LayerMask hitMask;
    
    [SerializeField] private LineRenderer lineRenderer;
    
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if ((1 << other.gameObject.layer) == hitMask)
        {
            print (other.gameObject);
        }
    }
}

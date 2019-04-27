using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float delay;
    void Start()
    {
        Invoke(nameof(DestroyThis), delay);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}

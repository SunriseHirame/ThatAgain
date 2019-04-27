using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TrashThrowing : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField]private GameObject[] trash;
    [SerializeField] private float force;
    [SerializeField] private Vector3 delta;
    void Update()
    {
        if (Random.value < 0.2f * Time.deltaTime)
        {
            ThrowTrash();
        }
    }

    void ThrowTrash()
    {
        GameObject t = Instantiate(trash[Random.Range(0, trash.Length - 1)], transform.position+delta, quaternion.identity);
        t.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1.0f,1.0f)*force,Random.Range(0.5f,1.0f)*force),ForceMode2D.Impulse);
    }
}

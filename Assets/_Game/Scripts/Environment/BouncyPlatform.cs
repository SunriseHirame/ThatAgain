using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private float Force;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.transform.GetComponentInParent<Game.Player> ();
        if (player)
        {
            print("Bounce");
            var rb = player.GetComponent<Rigidbody2D> ();
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * Force,ForceMode2D.Impulse);
        }
    }
}

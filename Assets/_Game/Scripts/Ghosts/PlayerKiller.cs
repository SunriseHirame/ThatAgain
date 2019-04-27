using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerKiller : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.attachedRigidbody)
            {
                PositionRecorder player = other.collider.attachedRigidbody.GetComponent<PositionRecorder>();
                if (player != null)
                {
                    if (!player.IsDead())
                    {
                        player.Die();
                    }
                }
            }
        }
    }

}
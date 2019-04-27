﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PositionRecorder player = other.GetComponent<PositionRecorder>();
        if (player != null)
        {
            if (!player.IsDead())
            {
                player.Die();
            }
        }
    }
}
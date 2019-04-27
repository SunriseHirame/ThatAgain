﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoundController : MonoBehaviour
{
    public static GameRoundController Instance { get; private set; }
    
    private GhostSpawner goal;

    private List<PositionRecorder> players = new List<PositionRecorder>();
    
    public int playerCount;
    public int finishedPlayers;
    public int deadPlayers;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (EveryoneFinished())
        {
            StartRound();
        }
    }

    public void StartRound()
    {
        //resets ghosts and players
        GhostSpawner.Instance.ResetGhosts();
        ResetPlayers();
    }

    private void ResetPlayers()
    {
        finishedPlayers = 0;
        deadPlayers = 0;
        foreach (PositionRecorder player in players) //reset all players
        {
            player.ReturnToStartPosition();
            player.gameObject.SetActive(true);
        }
    }

    public void SetGoalObject(GhostSpawner spawner)
    {
        goal = spawner;
    }
    
    private bool EveryoneFinished()
    {
        return (playerCount <= finishedPlayers + deadPlayers);
    }

    public void AddPlayer(PositionRecorder recorder)
    {
        players.Add(recorder);
        playerCount++;
    }

    public void RemovePlayer(PositionRecorder recorder)
    {
        players.Remove(recorder);
        playerCount--;
    }

    public void PlayerDied()
    {
        deadPlayers++;
    }

    public void PlayerFinished()
    {
        finishedPlayers++;
    }
}

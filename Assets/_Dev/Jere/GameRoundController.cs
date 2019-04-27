using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoundController : MonoBehaviour
{
    public static GameRoundController Instance { get; private set; }
    
    private GhostSpawner goal;

    private List<PositionRecorder> players = new List<PositionRecorder>();
    
    private int playerCount;
    private int finishedPlayers;
    private int deadPlayers;

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
        goal.ResetGhosts();
        finishedPlayers = 0;
        deadPlayers = 0;

        foreach (PositionRecorder player in players)
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
        return (playerCount == finishedPlayers + deadPlayers);
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

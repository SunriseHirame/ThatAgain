using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameRoundController : MonoBehaviour
    {
        public static GameRoundController Instance { get; private set; }

        [SerializeField] private bool betterMode;

        //private GhostSpawner goal;

        private List<PositionRecorder> players = new List<PositionRecorder> ();
        private bool gameEnded;

        public int playerCount;
        public int finishedPlayers;
        public int deadPlayers;

        void Update ()
        {
            if (playerCount > 0 && EveryoneFinished () && (!gameEnded || betterMode))
            {
                StartRound ();
            }
        }

        private void Start()
        {
            gameEnded = false;
        }

        public void EndGame()
        {
            print("YOU DIED");
            gameEnded = true;
        }

        public void StartRound ()
        {
            //resets ghosts and players
            GhostSpawner.Instance.ResetGhosts ();
            if (BackgroundManager.backgroundManager && finishedPlayers > 0)
                BackgroundManager.backgroundManager.LevelCompleted ();
            ResetPlayers ();
        }

        private void ResetPlayers ()
        {
            finishedPlayers = 0;
            deadPlayers = 0;
            foreach (PositionRecorder player in players) //reset all players
            {
                //player.gameObject.SetActive (true);
                player.ReturnToStartPosition ();
            }
        }

        public void SetGoalObject (GhostSpawner spawner)
        {
            //goal = spawner;
        }

        private bool EveryoneFinished ()
        {
            return (playerCount <= finishedPlayers + deadPlayers);
        }

        public void AddPlayer (PositionRecorder recorder)
        {
            players.Add (recorder);
            playerCount++;
        }

        public void RemovePlayer (PositionRecorder recorder)
        {
            players.Remove (recorder);
            playerCount--;
        }

        public void PlayerDied ()
        {
            deadPlayers++;
        }

        public void PlayerFinished ()
        {
            finishedPlayers++;
        }

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init ()
        {
           DontDestroyOnLoad(Instance = new GameObject ("GameController").AddComponent<GameRoundController> ());
           
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TimeTracker : MonoBehaviour
    {
        private float startTime;
        public float totalTime { get; private set; }

        public void StartCounting()
        {
            startTime = Time.realtimeSinceStartup;
        }

        public void SetTime()
        {
            totalTime = Time.realtimeSinceStartup - startTime;
            print(totalTime);
        }
    }
}
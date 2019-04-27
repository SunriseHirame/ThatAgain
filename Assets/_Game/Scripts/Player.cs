using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [Header ("Torso")]
        public Transform Head;
        public Transform Body;

        [Header ("Arms")] 
        public Transform ArmUpperLeft;
        public Transform ArmLowerLeft;

        public Transform ArmUpperRight;
        public Transform ArmLowerRight;

        [Header ("Legs")] 
        public Transform LegUpperLeft;
        public Transform LegLowerLeft;
        public Transform FootLeft;

        public Transform LegUpperRight;
        public Transform LegLowerRight;
        public Transform FootRight;
    }
}
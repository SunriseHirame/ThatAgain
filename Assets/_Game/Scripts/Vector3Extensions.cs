using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    
    public static class Vector3Extensions
    {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vector3 Add (this Vector2 self, Vector3 other)
        {
            return new Vector3(
                other.x + self.x,
                other.y + self.y,
                other.z);
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vector3 Add (this Vector3 self, Vector2 other)
        {
            return new Vector3(
                self.x + other.x,
                self.y + other.y,
                self.z);
        }
    }
    
}

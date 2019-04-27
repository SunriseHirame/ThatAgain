using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Game
{
    public class SurfaceChecker : MonoBehaviour
    {
        private const float SkinWidth = 0.01f;
        [SerializeField] private LayerMask surfaceMask;
        [SerializeField, Min (0)] private Vector2 scanDistance = new Vector2 (0.08f, 0.08f);
        [SerializeField] private Rect CheckBounds;
        [SerializeField, Min (1)] private int Resolution = 10;

        [SerializeField] private Rigidbody2D attachedRigidbody;

        public SurfaceInfo CheckCollisions ()
        {
            var collisionFlags = ScanSurfaces (scanDistance);
            return new SurfaceInfo (collisionFlags, false);
        }

        private CollisionFlags ScanSurfaces (Vector2 velocity)
        {
            var position = attachedRigidbody.position;
            var corners = new RectCorners (CheckBounds, position, SkinWidth);
            var collisionFlags = (CollisionFlags) 0;
            
            var checkVertical = new Vector2(
                0,
                velocity.y + SkinWidth
            );
            var steps = (int) CheckBounds.width * Resolution;
            var stepSize = new Vector2(CheckBounds.width / steps, 0);
            collisionFlags |= Scan (
                corners.TopLeft, corners.BottomLeft, checkVertical, 
                steps, stepSize, CollisionFlags.Above, CollisionFlags.Below);
            
            var checkHorizontal = new Vector2(
                velocity.x + SkinWidth,
                0
            );
            steps = (int) CheckBounds.height * Resolution;
            stepSize = new Vector2(0, CheckBounds.height / steps);
            collisionFlags |= Scan (
                corners.BottomRight, corners.BottomLeft, checkHorizontal,
                steps, stepSize, CollisionFlags.Right, CollisionFlags.Left);
            return collisionFlags;
        }

        private CollisionFlags Scan (Vector2 startPositive, Vector2 startNegative, Vector2 checkDistance,
            int steps, Vector2 stepSize, CollisionFlags positive, CollisionFlags negative)
        {
            var positiveOrigin = startPositive;
            var negativeOrigin = startNegative;
            
            var direction = checkDistance.normalized;
            var maxDistance = checkDistance.magnitude;

            var flags = default (CollisionFlags);
            
            for (var i = 0; i < steps; i++)
            {
                Debug.DrawRay (negativeOrigin, -checkDistance);
                Debug.DrawRay (positiveOrigin, checkDistance);

                var rayHitUp = Physics2D.Raycast (positiveOrigin, direction, maxDistance, surfaceMask);
                if (rayHitUp != default (RaycastHit2D))
                {
                    flags |= positive;
                }

                var rayHitDown = Physics2D.Raycast (negativeOrigin, -direction, maxDistance, surfaceMask);
                if (rayHitDown != default (RaycastHit2D))
                {
                    flags |= negative;
                }
                
                negativeOrigin += stepSize;
                positiveOrigin += stepSize;
            }
            return flags;
        }
        
        private void OnDrawGizmosSelected ()
        {
            var rectCorners = new RectCorners (CheckBounds, transform.position);

            Gizmos.DrawLine (rectCorners.TopLeft, rectCorners.TopRight);
            Gizmos.DrawLine (rectCorners.TopRight, rectCorners.BottomRight);
            Gizmos.DrawLine (rectCorners.BottomRight, rectCorners.BottomLeft);
            Gizmos.DrawLine (rectCorners.BottomLeft, rectCorners.TopLeft);
        }
    }

    public struct RectCorners
    {
        public readonly Vector2 BottomLeft;
        public readonly Vector2 BottomRight;
        public readonly Vector2 TopLeft;
        public readonly Vector2 TopRight;

        public RectCorners (Rect rect, Vector2 origin)
        {
            var hw = rect.width / 2f;
            var hh = rect.height / 2f;

            BottomLeft = origin + new Vector2 (-hw + rect.x, -hh + rect.y);
            BottomRight = origin + new Vector2 (hw + rect.x, -hh + rect.y);
            TopLeft = origin + new Vector2 (-hw + rect.x, hh + rect.y);
            TopRight = origin + new Vector2 (hw + rect.x, hh + rect.y);
        }

        public RectCorners (Rect rect, Vector2 origin, float skinWidth)
        {
            var hw = rect.width / 2f - skinWidth;
            var hh = rect.height / 2f - skinWidth;

            BottomLeft = origin + new Vector2 (-hw + rect.x, -hh + rect.y);
            BottomRight = origin + new Vector2 (hw + rect.x, -hh + rect.y);
            TopLeft = origin + new Vector2 (-hw + rect.x, hh + rect.y);
            TopRight = origin + new Vector2 (hw + rect.x, hh + rect.y);
        }
    }

    public struct SurfaceInfo
    {
        public readonly CollisionFlags Collisions;
        public readonly bool OnSlope;

        public bool OnGround => Collisions.HasFlag (CollisionFlags.Below);
        public bool OnWall => Collisions.HasFlag (CollisionFlags.Left) || Collisions.HasFlag (CollisionFlags.Right);

        public SurfaceInfo (CollisionFlags collisions, bool onSlope)
        {
            Collisions = collisions;
            OnSlope = onSlope;
        }
    }

    [System.Flags]
    public enum CollisionFlags
    {
        Above = 1,
        Below = 2,
        Left = 4,
        Right = 8
    }

    public static class CollisionFlagsExtensions
    {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool HasFlag (this CollisionFlags self, CollisionFlags other)
        {
            return (self & other) == other;
        }
    }
}
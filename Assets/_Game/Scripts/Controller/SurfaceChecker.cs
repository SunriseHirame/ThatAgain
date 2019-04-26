using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    public class SurfaceChecker : MonoBehaviour
    {
        private const float SkinWidth = 0.1f;
        [SerializeField] private LayerMask surfaceMask;

        [SerializeField] private Rect CheckBounds;
        [SerializeField, Min (1)] private int Resolution = 10;

        [SerializeField] private Rigidbody2D attachedRigidbody;

        public SurfaceInfo CheckCollisions (ref Vector2 velocity)
        {
            var collisionFlags = ScanSurfaces (ref velocity);
            return new SurfaceInfo ();
        }

        private CollisionFlags ScanSurfaces (ref Vector2 velocity)
        {
            var position = attachedRigidbody.position;
            var corners = new RectCorners (CheckBounds, position);
            var collisionFlags = (CollisionFlags) 0;
            
            collisionFlags |= ScanHorizontal (in corners, ref velocity);
            collisionFlags |= ScanVertical (in corners, ref velocity);
            return collisionFlags;
        }

        private CollisionFlags ScanVertical (in RectCorners corners, ref Vector2 velocity)
        {
            var isUp = velocity.y <= 0;
            var sign = Mathf.Sign (velocity.y);
            
            var rayOrigin = isUp ? corners.BottomLeft : corners.TopLeft;
            rayOrigin.y += SkinWidth * -sign;
            rayOrigin.x += SkinWidth;

            var scale = CheckBounds.width * Resolution;
            
            var resolution = 1 + scale;
            var spacing = (CheckBounds.width - 2 * SkinWidth) / scale;
            
            var maxDistance = velocity.magnitude + SkinWidth * -sign;
            var direction = velocity / maxDistance;
            
            var move = velocity.y;
            var hasCollided = false;

            for (var i = 0; i < resolution; i++)
            {
                Debug.DrawRay(rayOrigin, velocity * 5);
                
                var hit = Physics2D.Raycast (rayOrigin, direction, maxDistance, surfaceMask);
                if (hit != default (RaycastHit2D))
                {
                    maxDistance = Mathf.Max (hit.distance, SkinWidth * sign);
                    hasCollided = true;
                    move = hit.point.y - rayOrigin.y;
                }

                rayOrigin.x += spacing;
            }

            if (!hasCollided)
                return 0;

            velocity.y = isUp ? Mathf.Max (move - SkinWidth, 0) : Mathf.Min (move + SkinWidth, 0);
            return isUp ? CollisionFlags.Below : CollisionFlags.Above;
        }

        private CollisionFlags ScanHorizontal (in RectCorners corners, ref Vector2 velocity)
        {
            var isRight = velocity.x >= 0;
            var sign = Mathf.Sign (velocity.x);
            
            var rayOrigin = isRight ? corners.BottomRight : corners.BottomLeft;
            rayOrigin.x += SkinWidth * -sign;
            rayOrigin.y += SkinWidth;
            
            var yScale = Resolution * CheckBounds.height;
            
            var resolution = 1 + yScale;
            var spacing = (CheckBounds.height - 2 * SkinWidth) / yScale;
            
            var maxDistance = velocity.magnitude + SkinWidth * sign;
            var direction = velocity / maxDistance;

            var move = velocity.y;
            var hasCollided = false;

            for (var i =  0; i < resolution; i++)
            {
                Debug.DrawRay(rayOrigin, velocity * 5);
                
                var hit = Physics2D.Raycast (rayOrigin, direction, maxDistance, surfaceMask);
                if (hit != default (RaycastHit2D))
                {
                    maxDistance = Mathf.Max (hit.distance, SkinWidth * sign);
                    hasCollided = true;
                    move = hit.point.x - rayOrigin.x;
                }

                rayOrigin.y += spacing;
            }

            if (!hasCollided)
                return 0;

            velocity.x = isRight ? Mathf.Max (move - SkinWidth, 0) : Mathf.Min (move + SkinWidth, 0);
            return isRight ? CollisionFlags.Right : CollisionFlags.Left;
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
    }

    public struct SurfaceInfo
    {
        public readonly CollisionFlags Collisions;
        public readonly bool IsSlope;

        public SurfaceInfo (CollisionFlags collisions, bool isSlope)
        {
            Collisions = collisions;
            IsSlope = isSlope;
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
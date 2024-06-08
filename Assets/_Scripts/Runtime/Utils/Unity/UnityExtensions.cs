using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public static class UnityExtensions
    {
        #region Vec to VecInt
        public static Vector2Int ToVec2Int(this Vector2 v2, bool round = false) => round
                ? new(Mathf.RoundToInt(v2.x), Mathf.RoundToInt(v2.y))
                : new((int)v2.x, (int)v2.y);

        public static Vector2Int ToVec2Int(this Vector3 v3, bool round = false) => round
                ? new(Mathf.RoundToInt(v3.x), Mathf.RoundToInt(v3.y))
                : new((int)v3.x, (int)v3.y);

        public static Vector3Int ToVec3Int(this Vector2 v2, bool round = false) => round
                ? new(Mathf.RoundToInt(v2.x), Mathf.RoundToInt(v2.y), 0)
                : new((int)v2.x, (int)v2.y, 0);

        public static Vector3Int ToVec3Int(this Vector3 v3, bool round = false) => round
                ? new(Mathf.RoundToInt(v3.x), Mathf.RoundToInt(v3.y), Mathf.RoundToInt(v3.z))
                : new((int)v3.x, (int)v3.y, (int)v3.z);
        #endregion

        #region Vec Abs
        public static Vector2 Abs(this Vector2 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y));
        public static Vector2Int Abs(this Vector2Int v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y));

        public static Vector3 Abs(this Vector3 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        public static Vector3Int Abs(this Vector3Int v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        #endregion

        #region Camera
        /// <summary>
        /// Returns a <see cref="Rect"/> with the camera size and position information.
        /// </summary>
        public static Rect UnitRect2D(this Camera c)
        {
            Vector2 cameraPos = c.transform.position;

            Vector2 halfSize = new()
            {
                x = c.orthographicSize * c.aspect,
                y = c.orthographicSize,
            };

            return new()
            {
                x = cameraPos.x - halfSize.x,
                y = cameraPos.y - halfSize.y,
                width = halfSize.x * 2,
                height = halfSize.y * 2,
            };
        }

        #region Is Point on Screen 2D
        /// <summary>
        /// Is <paramref name="point"/> being rendered by this <see cref="Camera"/>?
        /// </summary>
        public static bool IsPointOnScreen2D(this Camera c, Vector2 point, Vector2 padding = default) => IsPointOnScreen2D(c, (Vector3)point, padding);

        /// <summary>
        /// Is <paramref name="point"/> being rendered by this <see cref="Camera"/>?
        /// </summary>
        public static bool IsPointOnScreen2D(this Camera c, Vector2Int point, Vector2 padding = default) => IsPointOnScreen2D(c, (Vector2)point, padding);

        /// <summary>
        /// Is <paramref name="point"/> being rendered by this <see cref="Camera"/>?
        /// </summary>
        /// 
        public static bool IsPointOnScreen2D(this Camera c, Vector3Int point, Vector2 padding = default) => IsPointOnScreen2D(c, (Vector3)point, padding);
        /// <summary>
        /// Is <paramref name="point"/> being rendered by this <see cref="Camera"/>?
        /// </summary>
        public static bool IsPointOnScreen2D(this Camera c, Vector3 point, Vector2 padding = default)
        {
            Rect camRect = UnitRect2D(c);
            
            if (padding != Vector2.zero)
            {
                camRect.min -= padding;
                camRect.max += padding;
            }

            return camRect.Contains(point);
        }
        #endregion

        #region On Screen 2D
        /// <summary>
        /// Is this object being rendered by given <see cref="Camera"/> <paramref name="cam"/>?
        /// </summary>
        /// <param name="cam">Specific <see cref="Camera"/></param>
        public static bool OnScreen2D(this Transform t, Camera cam, Vector2 padding = default) => IsPointOnScreen2D(cam, t.position, padding);

        /// <summary>
        /// Is this object being rendered by <see cref="Camera.main"/>?
        /// </summary>
        public static bool OnScreen2D(this Transform t, Vector2 padding = default) => OnScreen2D(t, Camera.main, padding); 
        #endregion
        #endregion
    }
}

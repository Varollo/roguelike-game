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
    }
}

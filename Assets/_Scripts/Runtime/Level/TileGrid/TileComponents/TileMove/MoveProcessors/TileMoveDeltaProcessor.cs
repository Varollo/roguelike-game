using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileMoveDeltaProcessor : ITileMoveProcessor
    {
        public Vector2Int ProcessMove(Vector2Int currentPos, Vector2Int targetPos)
        {
            return targetPos - currentPos;
        }
    }
}

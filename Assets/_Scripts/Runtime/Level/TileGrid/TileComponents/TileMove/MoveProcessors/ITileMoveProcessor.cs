using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public interface ITileMoveProcessor
    {
        Vector2Int ProcessMove(Vector2Int currentPos, Vector2Int targetPos);
    }
}

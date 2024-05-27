using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public interface ITileMoveRule
    {
        bool CanMove(Vector2Int from, Vector2Int to);
    }
}

using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public interface IMoveValidator
    {
        bool CanMove(Vector2Int from, Vector2Int to);
    }
}

using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public interface IMoveProvider
    {
        Vector2Int GetMovePosition(Vector2Int origin, IMoveValidator moveValidator);
    }
}

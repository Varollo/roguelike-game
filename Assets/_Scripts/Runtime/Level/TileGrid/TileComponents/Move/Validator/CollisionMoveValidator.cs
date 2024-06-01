using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class CollisionMoveValidator : IMoveValidator
    {
        public bool Validate(BaseTile tile, Vector2Int toPos)
        {
            return !TileGridManager.TryGetAllTiles(toPos.x, toPos.y, out var tiles)
                || tiles.Count == 0;
        }
    }
}

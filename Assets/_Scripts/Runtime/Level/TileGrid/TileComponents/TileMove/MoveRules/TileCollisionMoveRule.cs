using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileCollisionMoveRule : ITileMoveRule
    {
        public bool CanMove(Vector2Int from, Vector2Int to) => !TileGridManager.HasTile(to.x, to.y);
        //public bool CanMove(Vector2Int from, Vector2Int to) => !TileGridManager.TryGetAllTiles(to.x, to.y, out var tiles)
        //    || tiles.All(tile =>
        //    {
        //        TileColliderComponent collider = tile.GetComponent<TileColliderComponent>();
        //        return collider == null || !collider.IsSolid;
        //    });
    }
}

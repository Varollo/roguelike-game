using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class ManagedMoveValidator : IMoveValidator
    {
        public virtual bool CanMove(Vector2Int from, Vector2Int to)
        {
            return TileManager.TryMove(from, to);
        }
    }
}

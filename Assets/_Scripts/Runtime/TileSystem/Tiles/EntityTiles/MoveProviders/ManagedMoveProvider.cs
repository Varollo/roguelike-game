using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public abstract class ManagedMoveProvider : IMoveProvider
    {
        public Vector2Int GetMovePosition(Vector2Int origin, IMoveValidator moveValidator)
        {
            if (TryGetMovePosition(origin, moveValidator, out var resultPos))
                return resultPos;
            
            else
                return GetFallbackPosition(origin, resultPos, moveValidator);
        }

        protected virtual Vector2Int GetFallbackPosition(Vector2Int origin, Vector2Int failedMove, IMoveValidator moveValidator)
        {
            return origin;
        }

        protected abstract bool TryGetMovePosition(Vector2Int origin, IMoveValidator moveValidator, out Vector2Int resultPos);
    }
}

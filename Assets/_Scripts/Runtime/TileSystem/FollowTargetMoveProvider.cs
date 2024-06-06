using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class FollowTargetMoveProvider : IMoveProvider
    {
        private const int MOVE_LENGTH = 1;

        private readonly Transform _ownerTransform;

        private Tile _target;
        private bool _allowDiagonals;

        public FollowTargetMoveProvider(Transform ownerTransform, bool allowDiagonals = true) : this(ownerTransform, null, allowDiagonals) { }
        public FollowTargetMoveProvider(Transform ownerTransform, Tile target, bool allowDiagonals = true)
        {
            _allowDiagonals = allowDiagonals;
            _ownerTransform = ownerTransform;
            _target = target;
        }

        public void SetTarget(Tile target) => _target = target;

        public Vector2Int GetMovePosition(Vector2Int origin, IMoveValidator moveValidator)
        {
            if (_target == null)
                return origin;

            Vector2Int targetPos = _target.Position;

            float bestDist = float.PositiveInfinity;
            Vector2Int bestMove = origin;

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if ((x == 0 && y == 0) 
                    || (!_allowDiagonals && Mathf.Abs(x) == Mathf.Abs(y)))
                        continue;

                    Vector2Int movePos = RaycastTiles(origin, new(x, y), MOVE_LENGTH);
                    float moveDist = (targetPos - movePos).sqrMagnitude;

                    if (moveDist < bestDist && !TileManager.CanMove(movePos)/*&& !Physics2D.OverlapPoint(movePos)*/)
                    {
                        bestDist = moveDist;
                        bestMove = movePos;
                    }
                }
            }
                        
            return moveValidator.CanMove(origin, bestMove)
                ? bestMove
                : origin;
        }

        private Vector2Int RaycastTiles(Vector2Int origin, Vector2Int direction, int length)
        {
            Vector2Int hitPoint = origin;

            for (int i = 1; i <= length; i++)
            {
                Vector2Int point = origin + direction * i;
                Collider2D[] hit = Physics2D.OverlapCircleAll(point, .4f);

                if (hit != null && hit.Length > 0)
                    break;

                hitPoint = point;
            }

            return hitPoint;
        }
    }
}

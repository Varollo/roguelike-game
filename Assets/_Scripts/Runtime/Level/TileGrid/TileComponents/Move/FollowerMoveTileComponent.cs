using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class FollowerMoveTileComponent : MoveTileComponent
    {
        private readonly bool _allowDiagonals;
        private BaseTile _target;

        public FollowerMoveTileComponent(BaseTile target = null, bool allowDiagonals = false) : base() 
        {
            _allowDiagonals = allowDiagonals;
            SetTarget(target);
        }

        public FollowerMoveTileComponent(BaseTile target, IMoveValidator validator, bool allowDiagonals = false) : base(validator)
        {
            _allowDiagonals = allowDiagonals;
            SetTarget(target);
        }

        public void SetTarget(BaseTile target)
        {
            _target = target;
        }

        protected override Vector2Int ComputeMove()
        {
            const int CHECK_WIDTH = 3;
            const int CHECK_HEIGHT = 3;

            Vector2Int bestMove = TilePosition;
            float bestWeight = float.PositiveInfinity;

            GetBounds(CHECK_WIDTH, CHECK_HEIGHT, out Vector2Int minBounds, out Vector2Int maxBounds);

            for (int y = minBounds.y; y <= maxBounds.y; y++)
            {
                for (int x = minBounds.x; x <= maxBounds.x; x++)
                {
                    if (CanSkip(x, y, minBounds, maxBounds))
                        continue;

                    Vector2Int move = new Vector2Int(x, y);
                    Vector2Int movePos = TilePosition + move;

                    if (!Validator.Validate(TilePosition, movePos))
                        continue;

                    float moveWeight = GetMoveWeight(movePos);
                    if (moveWeight < bestWeight)
                    {
                        bestWeight = moveWeight;
                        bestMove = move;
                    }
                }
            }

            return bestMove;
        }

        private float GetMoveWeight(Vector2Int movePos)
        {
            return (_target.Position - movePos).sqrMagnitude;            
        }

        private bool CanSkip(int x, int y, Vector2Int minBounds, Vector2Int maxBounds)
        {
            return (x == 0 && y == 0) // always skip center tile!
                || (!_allowDiagonals && IsDiagnoal(x, y, minBounds, maxBounds)); // if don't allow diag, skip diag tiles!
        }

        private static bool IsDiagnoal(int x, int y, Vector2Int minBounds, Vector2Int maxBounds)
        {
            return (x == minBounds.x || x == maxBounds.x) && (y == minBounds.y || y == maxBounds.y);
        }

        private static void GetBounds(int width, int height, out Vector2Int minBounds, out Vector2Int maxBounds)
        {
            maxBounds = new()
            {
                x = Mathf.FloorToInt(width * .5f),
                y = Mathf.FloorToInt(height * .5f)
            };
            minBounds = -maxBounds;
        }
    }
}

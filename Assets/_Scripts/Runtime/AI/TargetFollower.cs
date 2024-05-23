using DG.Tweening;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TargetFollower : MonoBehaviour//, ITurnListener
    {
        /*
        private readonly static List<Vector2> _pendingMoves = new();

        [SerializeField] private Transform target;
        [SerializeField][Min(0.01f)] private float moveDistance = 1f;

        private void OnEnable() => TurnManager.AddTurnListener(this);
        private void OnDisable() => TurnManager.RemoveTurnListener(this);

        public void OnTurnAction(ulong turnCount)
        {
            if (turnCount % 2 == 0)
                return;

            Vector2 currentPos = transform.position;
            Vector2 targetPos = target.position;

            float bestDist = float.PositiveInfinity;
            Vector2 bestMove = Vector2.zero;

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    Vector2 moveDir = new(x, y);
                    Vector2 movePos;

                    RaycastHit2D hit = Physics2D.RaycastAll(transform.position, moveDir, moveDistance)
                        .Where(h => h.transform != transform)
                        .FirstOrDefault();

                    if (hit)
                    {
                        Vector2 point = new()
                        {
                            x = Mathf.Floor(hit.point.x),
                            y = Mathf.Floor(hit.point.y),
                        };
                        movePos = point - moveDir;
                    }
                    else
                    {
                        movePos = currentPos + moveDir * moveDistance;
                    }

                    float moveDist = (targetPos - movePos).sqrMagnitude;

                    if (moveDist < bestDist
                    && !_pendingMoves.Contains(movePos)
                    && !Physics2D.OverlapPoint(movePos))
                    {
                        bestDist = moveDist;
                        bestMove = movePos;
                    }
                }
            }

            if (bestDist != float.PositiveInfinity)
            {
                transform.DOMove(bestMove, 0.25f);

                _pendingMoves.Remove(currentPos);
                _pendingMoves.Add(bestMove);
            }
        }

        public void OnTurnEnd(ulong turnCount)
        {
            if (_pendingMoves.Count > 0)
                _pendingMoves.Clear();
        }

        public void OnTurnStart(ulong turnCount) { }
        */

        private WorldObject _wo;

        private void Awake() => _wo ??= new(new((int)transform.position.x, (int)transform.position.y));
        private void OnDestroy() => WorldManager.Kill(_wo);

        private void OnEnable()
        {
            InputManager.GetController<SwipeInputController>().OnSwipe += OnSwipe;
            _wo.OnMove += OnMove;
        }

        private void OnDisable()
        {
            InputManager.GetController<SwipeInputController>().OnSwipe -= OnSwipe;
            _wo.OnMove -= OnMove;
        }

        private void OnMove(Vector2Int endPos, Vector2Int moveDelta)
        {
            transform.DOMove((Vector2)endPos, .25f);
        }

        private void OnSwipe(Touch touch, Vector2 dir)
        {
            WorldManager.SetObjectPosition(_wo, Mathf.FloorToInt(dir.x), Mathf.FloorToInt(dir.y));
        }
    }
}

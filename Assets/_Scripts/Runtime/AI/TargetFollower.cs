using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TargetFollower : MonoBehaviour, ITurnListener
    {
        private readonly static List<Vector2> _pendingMoves = new();

        [SerializeField] private Transform target;
        [SerializeField] private Camera cam;
        [SerializeField][Min(0.01f)] private float moveDistance = 1f;

        private void Awake() => TurnManager.AddTurnListener(this);
        private void OnDestroy() => TurnManager.RemoveTurnListener(this);

        private void Start()
        {
            DisableWhenOffScreen();
        }

        public void OnTurnAction(ulong turnCount)
        {
            if (DisableWhenOffScreen())
                return;

            if (turnCount % 2 == 0)
                Move();
        }

        private bool DisableWhenOffScreen()
        {
            if (transform.OnScreen2D(cam, Vector2.one * 2))
            {
                gameObject.SetActive(true);
                return false;
            }

            gameObject.SetActive(false);
            return true;
        }

        private void Move()
        {
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

    }
}

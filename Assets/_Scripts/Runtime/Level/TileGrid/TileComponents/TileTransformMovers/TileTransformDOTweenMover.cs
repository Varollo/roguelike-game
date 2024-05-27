using DG.Tweening;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileTransformDOTweenMover : ITileTransformMover
    {
        public Vector3? CurrentMoveTarget { get; private set; }
        public float MoveDuration { get; set; }
        public Ease MoveEasing { get; set; }

        public TileTransformDOTweenMover(float moveDuration, Ease moveEasing = Ease.InOutQuad)
        {
            MoveDuration = moveDuration;
            MoveEasing = moveEasing;
        }

        private void SetupMove(Transform transform, Vector3 newMoveTarget)
        {
            transform.DOKill();

            if (CurrentMoveTarget.HasValue)
                transform.position = CurrentMoveTarget.Value;

            CurrentMoveTarget = newMoveTarget;
        }

        private void ClearMoveTarget()
        {
            CurrentMoveTarget = null;
        }

        public void MoveTransform(Transform transform, Vector3 position)
        {
            SetupMove(transform, position);

            transform.DOMove(position, MoveDuration)
                     .SetEase(MoveEasing)
                     .OnKill(ClearMoveTarget)
                     .OnComplete(ClearMoveTarget);
        }
    }
}

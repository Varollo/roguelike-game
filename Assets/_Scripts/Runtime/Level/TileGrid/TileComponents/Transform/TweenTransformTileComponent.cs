using DG.Tweening;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TweenTransformTileComponent : TransformTileComponent
    {
        public const float DEFAULT_DURATION = 0.25f;

        public Vector3? CurrentMoveTarget { get; private set; }
        public float MoveDuration { get; set; }
        public Ease MoveEasing { get; set; }

        public TweenTransformTileComponent() : this(null)
        {            
        }

        public TweenTransformTileComponent(Transform transform, float moveDuration = DEFAULT_DURATION, Ease moveEasing = Ease.InOutQuad) : base(transform)
        {
            MoveDuration = moveDuration;
            MoveEasing = moveEasing;
        }

        private void SetupMove(Transform transform, Vector3 newMoveTarget)
        {
            transform.DOKill();

            if (CurrentMoveTarget.HasValue && transform != null)
                transform.position = CurrentMoveTarget.Value;

            CurrentMoveTarget = newMoveTarget;
        }

        private void ClearMoveTarget()
        {
            CurrentMoveTarget = null;
        }

        protected override void MoveTransform(Transform transform, Vector3 position)
        {
            SetupMove(transform, position);

            transform.DOMove(position, MoveDuration)
                     .SetEase(MoveEasing)
                     .OnKill(ClearMoveTarget)
                     .OnComplete(ClearMoveTarget);
        }
    }
}

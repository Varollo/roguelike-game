using DG.Tweening;
using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    class TweenMoveAnimator : IMoveAnimator
    {
        private const float MOVE_DURATION = 0.25f;

        private Tween _lastTween;

        public void OnTileMove(Transform tileTransform, Vector2Int from, Vector2Int to)
        {
            if (IsTweening())
                _lastTween = GetTweenFromMove(tileTransform, from, to, _lastTween.ElapsedPercentage());

            else
                _lastTween = GetTweenFromMove(tileTransform, from, to);
        }

        protected virtual Tween GetTweenFromMove(Transform tileTransform, Vector2Int from, Vector2Int to, float interpolator = 1f)
        {
            return DOTween.Sequence()

            .Append(tileTransform.DOMove(new()
            {
                x = from.x,
                y = from.y,
                z = tileTransform.position.z
            }, MOVE_DURATION * (1f - interpolator)))

            .Append(tileTransform.DOMove(new()
            {
                x = to.x,
                y = to.y,
                z = tileTransform.position.z
            }, MOVE_DURATION * interpolator))

            .Play();
        }

        protected virtual bool IsTweening()
        {
            return _lastTween != null && _lastTween.IsPlaying();
        }
    }
}

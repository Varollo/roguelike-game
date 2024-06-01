using DG.Tweening;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class MovementBehaviour : TurnBasedBehaviour
    {
        protected Vector2 LastMovePos { get; private set; }

        protected override void OnInit()
        {
            LastMovePos = Transform.position;
        }

        public override void Act(ulong turnCount)
        {
            if (CanMove(turnCount))
            {
                Vector2 endPos = GetMovePos(LastMovePos);
                float moveDur = GetMoveDuration();

                Tween move = Move(endPos, moveDur);
                OnMove(move);
            }
        }

        private Tween Move(Vector2 endPos, float dur)
        {
            Transform.position = LastMovePos;
            return Transform.DOMove(LastMovePos = endPos, dur);
        }

        protected virtual bool CanMove(ulong turnCount) => true;
        protected virtual float GetMoveDuration() => 0.25f;

        protected virtual void OnMove(Tween moveTween) { }

        protected abstract Vector2 GetMovePos(Vector2 iniPos);
    }
}

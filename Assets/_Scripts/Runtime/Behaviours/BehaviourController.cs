using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class BehaviourController : MonoBehaviour, ITurnListener
    {
        private TurnBasedBehaviour _behaviour;

        public TurnBasedBehaviour Behaviour => _behaviour ??= InitNewBehaviour();

        protected virtual void OnEnable() => TurnManager.AddTurnListener(this);
        protected virtual void OnDisable() => TurnManager.RemoveTurnListener(this);
        protected virtual void OnDestroy() => Behaviour.Kill();

        private TurnBasedBehaviour InitNewBehaviour()
        {
            var behaviour = GetNewBehaviour();
            behaviour.InitObject(transform);

            return behaviour;
        }

        public void OnTurnAction(ulong turnCount)
        {
            Behaviour.Act(turnCount);
        }

        public void OnTurnStart(ulong turnCount) { }
        public void OnTurnEnd(ulong turnCount) { }

        protected abstract TurnBasedBehaviour GetNewBehaviour();
    }
}

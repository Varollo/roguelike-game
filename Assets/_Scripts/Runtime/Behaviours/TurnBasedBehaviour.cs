using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class TurnBasedBehaviour
    {
        protected Transform Transform { get; private set; }

        public void InitObject(Transform objectTransform)
        {
            Transform = objectTransform;
            OnInit();
        }

        public void Kill()
        {
            Transform = null;
            OnKill();
        }

        protected virtual void OnInit() { }
        protected virtual void OnKill() { }

        public abstract void Act(ulong turnCount);
    }
}

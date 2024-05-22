using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TurnChangeManager : UnitySingleton<TurnChangeManager>
    {
        protected override bool KeepOnLoadScene => true;

        private void OnEnable() => InputManager.GetController<SwipeInputController>().OnSwipe += OnSwipe;
        private void OnDisable() => InputManager.GetController<SwipeInputController>().OnSwipe -= OnSwipe;

        private void OnSwipe(Touch touch, Vector2 dir)
        {
            TurnManager.NextTurn();
        }
    }
}

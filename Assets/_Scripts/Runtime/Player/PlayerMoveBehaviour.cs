using UnityEngine;

namespace Ribbons.RoguelikeGame.Player
{
    public class PlayerMoveBehaviour : MovementBehaviour
    {
        private SwipeInputController _swipeController;

        protected SwipeInputController SwipeController => 
            _swipeController ??= InputManager.GetController<SwipeInputController>();

        protected override Vector2 GetMovePos(Vector2 iniPos)
        {
            return iniPos + SwipeController.Direction;
        }
    }
}

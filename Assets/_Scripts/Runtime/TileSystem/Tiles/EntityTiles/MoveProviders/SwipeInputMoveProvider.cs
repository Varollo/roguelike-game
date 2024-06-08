using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class SwipeInputMoveProvider : ManagedMoveProvider
    {
        private readonly SwipeInputController _swipeController;

        public SwipeInputMoveProvider() : this(InputManager.GetController<SwipeInputController>()) { }

        public SwipeInputMoveProvider(SwipeInputController swipeController)
        {
            _swipeController = swipeController;
        }

        protected override bool TryGetMovePosition(Vector2Int origin, IMoveValidator moveValidator, out Vector2Int resultPos)
        {
            Vector3 dir = _swipeController.Direction;
            Vector2Int movePos = origin + dir.ToVec2Int();
            
            resultPos = origin;

            if (!moveValidator.CanMove(origin, movePos))
                return false;

            resultPos = movePos;
            return true;
        }
    }
}

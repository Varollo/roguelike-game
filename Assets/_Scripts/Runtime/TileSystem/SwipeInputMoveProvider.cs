using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class SwipeInputMoveProvider : IMoveProvider
    {
        private readonly SwipeInputController _swipeController;

        public SwipeInputMoveProvider() : this(InputManager.GetController<SwipeInputController>()) { }

        public SwipeInputMoveProvider(SwipeInputController swipeController)
        {
            _swipeController = swipeController;
        }

        public Vector2Int GetMovePosition(Vector2Int origin, IMoveValidator moveValidator)
        {
            Vector3 dir = _swipeController.Direction;
            Vector2Int movePos = origin + dir.ToVec2Int();

            return moveValidator.CanMove(origin, movePos) 
                ? movePos 
                : origin;
        }
    }
}

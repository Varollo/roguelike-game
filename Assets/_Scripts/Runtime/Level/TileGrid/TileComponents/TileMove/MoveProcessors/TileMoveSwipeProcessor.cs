using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileMoveSwipeProcessor : ITileMoveProcessor
    {
        private SwipeInputController _swipeController;

        private SwipeInputController SwipeController => _swipeController ??= InputManager.GetController<SwipeInputController>();

        public Vector2Int ProcessMove(Vector2Int currentPos, Vector2Int targetPos)
        {
            return SwipeController.Direction.ToVec2Int(round: true);
        }
    }
}

using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class SwipeToMoveTileComponent : MoveTileComponent
    {
        private readonly SwipeInputController _swipeController;

        public SwipeToMoveTileComponent()
        {
            _swipeController = InputManager.GetController<SwipeInputController>();
        }

        protected override Vector2Int ComputeMove()
        {
            return _swipeController.Direction.ToVec2Int(true);
        }
    }
}

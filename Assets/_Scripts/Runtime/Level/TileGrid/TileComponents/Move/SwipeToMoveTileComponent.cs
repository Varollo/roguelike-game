using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class SwipeToMoveTileComponent : MoveTileComponent
    {
        private readonly SwipeInputController _swipeController;

        public SwipeToMoveTileComponent(BaseTile parentTile, IMoveValidator validator) : base(parentTile, validator)
        {
            _swipeController = InputManager.GetController<SwipeInputController>();
        }

        protected override Vector2Int ComputeMove()
        {
            return _swipeController.Direction.ToVec2Int(true);
        }
    }
}

using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class PlayerTile : EntityTile
    {
        private readonly SwipeInputController _swipeController;

        #region Constructors
        public PlayerTile(Transform transform, bool enabled = true) : this(transform, validator: new(), enabled) { }

        public PlayerTile(Transform transform, int layerMask, bool enabled = true) : this(transform, validator: new(layerMask), enabled) { }

        private PlayerTile(Transform transform, OverlapPointMoveValidator validator, bool enabled) : base(transform, validator, enabled)
        {
            _swipeController = InputManager.GetController<SwipeInputController>();
        } 
        #endregion

        #region Screen Callbacks
        protected override void OnEnterScreen(Camera camera)
        {
            base.OnEnterScreen(camera);
            _swipeController.OnSwipe += OnSwipeInput;
        }

        protected override void OnExitScreen(Camera camera)
        {
            base.OnExitScreen(camera);
            _swipeController.OnSwipe -= OnSwipeInput;
        } 
        #endregion

        private void OnSwipeInput(Touch touch, Vector2 dir)
        {
            Vector2Int movePos = new Vector2()
            {
                x = Transform.position.x + dir.x,
                y = Transform.position.y + dir.y,
            }.ToVec2Int(); // this way keeps conversion consistent

            if (MoveValidator.CanMove(Position, movePos))
                Move(movePos);
        }
    }
}

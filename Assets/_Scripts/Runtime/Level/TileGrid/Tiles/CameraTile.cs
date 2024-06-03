using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class CameraTile : MovableTile
    {
        public static event TilePositionDelegate OnCameraMoveEvent;

        private readonly TileView _cameraTargetView;

        public CameraTile(Transform tileTransform, TileView cameraTargetView, params ITileComponent[] components) : base(tileTransform, components)
        {
            _cameraTargetView = cameraTargetView;
        }

        protected TargetedMoveTileComponent TargetedMoveComponent { get; private set; }

        protected override void OnTurnStart(ulong turnCount)
        {
            if (!TargetedMoveComponent.HasTarget)
                TargetedMoveComponent.SetTarget(_cameraTargetView.Tile as BaseTile);
        }

        protected override void OnTurnEnd(ulong turnCount)
        {
            Position += TargetedMoveComponent.GetMove();
        }

        protected override void OnSetPosition(Vector2Int position)
        {
            OnCameraMoveEvent?.Invoke(position);
        }

        protected override MoveTileComponent CreateMoveTileComponent()
        {
            return TargetedMoveComponent = new TargetedMoveTileComponent(this);
        }
    }
}

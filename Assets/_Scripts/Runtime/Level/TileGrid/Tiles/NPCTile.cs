using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class NPCTile : MovableTile
    {
        private readonly FollowerMoveTileComponent _followComponent;
        private BaseTile _playerTile;

        public NPCTile(Transform npcTransform, params ITileComponent[] components) : base(npcTransform, components)
        {
            _followComponent = MoveTileComponent as FollowerMoveTileComponent;
        }

        protected override void OnTurnAction(ulong turnCount)
        {
            if (PlayerTile.Instance == null)
                return;

            if (_playerTile == null)
                _followComponent.SetTarget(_playerTile = PlayerTile.Instance);

            Position += MoveTileComponent.GetMove();
        }

        protected override MoveTileComponent CreateMoveTileComponent() => new FollowerMoveTileComponent(this, new CollisionMoveValidator());
        protected override TransformTileComponent CreateTransformComponent() => new TweenTransformTileComponent(this);
    }
}

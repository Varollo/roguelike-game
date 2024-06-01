using System.Collections.Generic;
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

        protected TileTurnListenerComponent TurnListenerComponent { get; set; }

        private void OnTurnAction(ulong turnCount)
        {
            if (_playerTile == null)
                _followComponent.SetTarget(_playerTile = Object.FindAnyObjectByType<PlayerTileView>().Tile as BaseTile);

            if (_playerTile != null)
                SetPosition(Position + MoveTileComponent.GetMove());
        }

        protected override MoveTileComponent CreateMoveTileComponent() => new FollowerMoveTileComponent(this, new CollisionMoveValidator());
        protected override TransformTileComponent CreateTransformComponent() => new TweenTransformTileComponent(this);

        protected override List<ITileComponent> GetDefaultComponents() => new(base.GetDefaultComponents())
        {
            (TurnListenerComponent = new TileTurnListenerComponent(this, OnTurnAction)),
        };
    }
}

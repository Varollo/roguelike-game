using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class NPCTile : BaseTile
    {
        private BaseTile _playerTile;

        public NPCTile(Transform npcTransform, params ITileComponent[] components) : base(npcTransform.position.ToVec2Int(), components)
        {
            TransformComponent.SetTransform(npcTransform);
            TurnListenerComponent.SetAction(OnTurnAction);
        }

        protected virtual TransformTileComponent TransformComponent { get; } = new TweenTransformTileComponent();
        protected virtual TileTurnListenerComponent TurnListenerComponent { get; } = new TileTurnListenerComponent();
        protected virtual FollowerMoveTileComponent MoveTileComponent { get; } = new FollowerMoveTileComponent();

        private void OnTurnAction(ulong turnCount)
        {
            if (_playerTile == null)
                MoveTileComponent.SetTarget(_playerTile = Object.FindAnyObjectByType<PlayerTileView>().Tile as BaseTile);

            if (_playerTile != null)
                SetPosition(Position + MoveTileComponent.GetMove());
        }

        protected override List<ITileComponent> GetDefaultComponents() => new()
        {
            TransformComponent,
            TurnListenerComponent,
            MoveTileComponent
        };        
    }
}

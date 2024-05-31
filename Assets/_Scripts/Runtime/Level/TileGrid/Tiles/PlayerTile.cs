using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class PlayerTile : BaseTile
    {
        public PlayerTile(Transform playerTransform, params ITileComponent[] components) : base(playerTransform.position.ToVec2Int(), components)
        {
            TransformComponent.SetTransform(playerTransform);
            TurnListenerComponent.SetAction(OnTurnAction);
        }

        protected virtual MoveTileComponent MoveTileComponent { get; } = new SwipeToMoveTileComponent();
        protected virtual TransformTileComponent TransformComponent { get; } = new TweenTransformTileComponent();
        protected virtual TileTurnListenerComponent TurnListenerComponent { get; } = new TileTurnListenerComponent();

        private void OnTurnAction(ulong turnCount) => SetPosition(Position + MoveTileComponent.GetMove());

        protected override List<ITileComponent> GetDefaultComponents() => new()
        {
            TransformComponent,
            TurnListenerComponent,
            MoveTileComponent,
        };
    }
}

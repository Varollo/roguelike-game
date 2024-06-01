using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class PlayerTile : MovableTile
    {
        public PlayerTile(Transform playerTransform, params ITileComponent[] components) : base(playerTransform, components)
        {
        }

        protected virtual TileTurnListenerComponent TurnListenerComponent { get; set; }

        private void OnTurnAction(ulong turnCount)
        {
            SetPosition(Position + MoveTileComponent.GetMove());
        }

        protected virtual TileTurnListenerComponent CreateTurnListenerComponent() => new(this, OnTurnAction);

        protected override MoveTileComponent CreateMoveTileComponent() => new SwipeToMoveTileComponent(this, new CollisionMoveValidator());
        protected override TransformTileComponent CreateTransformComponent() => new TweenTransformTileComponent(this);

        protected override List<ITileComponent> GetDefaultComponents() => new(base.GetDefaultComponents())
        {
            (TurnListenerComponent = CreateTurnListenerComponent()),
        };
    }
}

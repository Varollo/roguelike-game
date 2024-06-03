using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class PlayerTile : MovableTile
    {
        public PlayerTile(Transform playerTransform, params ITileComponent[] components) : base(playerTransform, components)
        {
        }

        protected override void OnTurnAction(ulong turnCount)
        {
            Position += MoveTileComponent.GetMove();
        }

        protected override TransformTileComponent CreateTransformComponent() => new TweenTransformTileComponent(this);
        protected override MoveTileComponent CreateMoveTileComponent() => new SwipeToMoveTileComponent(this, new CollisionMoveValidator());
    }
}

using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class PlayerTile : MovableTile
    {
        public static PlayerTile Instance { get; private set; }

        public PlayerTile(Transform playerTransform, params ITileComponent[] components) : base(playerTransform, components)
        {
            Instance = this;
        }

        protected override void OnTurnAction(ulong turnCount)
        {
            Position += MoveTileComponent.GetMove();
        }

        protected override TransformTileComponent CreateTransformComponent() => new TweenTransformTileComponent(this);
        protected override MoveTileComponent CreateMoveTileComponent() => new SwipeToMoveTileComponent(this, new CollisionMoveValidator());
    }
}

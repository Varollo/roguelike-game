using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class PlayerTile : MovingTransformTile
    {
        public PlayerTile(Transform playerTransform, params ITileComponent[] components) : base(playerTransform, components)
        {
        }

        private void OnTurnAction(ulong turnCount)
        {
            SetPosition(Position);
        }

        protected override IEnumerable<ITileComponent> SetupComponents() => CombineComponents(
            base.SetupComponents(),
            new TileTurnListenerComponent(OnTurnAction)
        );

        protected override TileMotorComponent CreateMotorComponent() => new(new TileMoveSwipeProcessor());
        protected override TileTransformComponent CreateTransformComponent() => new(TileTransform, new TileTransformDOTweenMover(.25f));
    }
}

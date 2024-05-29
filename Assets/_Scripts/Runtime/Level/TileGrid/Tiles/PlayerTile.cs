using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class PlayerTile : TransformTile
    {
        public PlayerTile(Transform playerTransform, params ITileComponent[] components) : base(playerTransform, components)
        {
        }

        private void OnTurnAction(ulong turnCount) => MoveTile(Motor.GetMove());

        protected override ITileTransformMover CreateTransformMover() => new TileTransformDOTweenMover();
        protected override ITileMoveProcessor CreateMoveProcessor() => new TileMoveSwipeProcessor();

        protected override IEnumerable<ITileComponent> SetupComponents() => CombineComponents(
            base.SetupComponents(),
            new TileTurnListenerComponent(OnTurnAction));
    }
}

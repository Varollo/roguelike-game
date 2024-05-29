using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class NPCTile : TransformTile
    {
        private Tile _playerTile;

        public NPCTile(Transform tileTransform, params ITileComponent[] components) : base(tileTransform, components)
        {
        }

        private void OnTurnAction(ulong turnCount)
        {
            _playerTile ??= Object.FindObjectOfType<PlayerTileView>().Tile as Tile;
            MoveTile(Motor.GetMove(_playerTile.Position));

            //Vector2Int move = _playerTile.Position - Position;

            //SetPosition(Position + new Vector2Int(
            //    move.x == 0 ? 0 : (int)Mathf.Sign(move.x),
            //    move.y == 0 ? 0 : (int)Mathf.Sign(move.y)
            //));
        }

        protected override IEnumerable<ITileComponent> SetupComponents() => CombineComponents(
            base.SetupComponents(),
            new TileTurnListenerComponent(OnTurnAction)
        );

        protected override ITileMoveRule CreateMoveRule() => new TileCollisionMoveRule();
        protected override ITileMoveProcessor CreateMoveProcessor() => new TileFollowerProcessor();
        protected override ITileTransformMover CreateTransformMover() => new TileTransformDOTweenMover();
    }
}

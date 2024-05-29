using System;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileTransformComponent : TileComponent
    {
        private readonly ITileTransformMover _mover;
        private Transform _transform;

        public TileTransformComponent(Transform transform) : this(transform, new TileTransformInstantMover())
        {            
        }

        public TileTransformComponent(Transform transform, ITileTransformMover mover)
        {
            _transform = transform;
            _mover = mover;
        }

        public override void OnTilePositionMove(Vector2Int newPos)
        {
            base.OnTilePositionMove(newPos);
            _mover.MoveTransform(_transform, (Vector2) TilePosition);
        }

        public void SetTransform(Transform transform)
        {
            _transform = transform;
        }
    }
}

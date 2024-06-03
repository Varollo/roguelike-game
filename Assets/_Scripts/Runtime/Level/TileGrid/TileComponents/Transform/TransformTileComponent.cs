using System;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class TransformTileComponent : BaseTileComponent
    {
        private Transform _transform;

        public TransformTileComponent(BaseTile parentTile) : this(parentTile, null) 
        { 
        }

        protected TransformTileComponent(BaseTile parentTile, Transform transform) : base(parentTile)
        {
            SetTransform(transform);
        }

        public override void OnTilePositionMove(Vector2Int newPos)
        {
            base.OnTilePositionMove(newPos);

            if (_transform)
                MoveTransform(_transform, new()
                {
                    x = TilePosition.x, 
                    y = TilePosition.y, 
                    z = _transform.position.z
                });
        }

        public void SetTransform(Transform transform)
        {
            _transform = transform;
        }

        protected abstract void MoveTransform(Transform transform, Vector3 position);

        public virtual void SetObjectActive(bool active)
        {
            _transform.gameObject.SetActive(active);
        }
    }
}

using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class TransformTileComponent : BaseTileComponent
    {
        private Transform _transform;

        public TransformTileComponent() : this(null)
        {
        }

        public TransformTileComponent(Transform transform)
        {
            _transform = transform;
        }

        public override void OnTilePositionMove(Vector2Int newPos)
        {
            base.OnTilePositionMove(newPos);
            MoveTransform(_transform, (Vector2)TilePosition);
        }

        public void SetTransform(Transform transform)
        {
            _transform = transform;
        }

        protected abstract void MoveTransform(Transform transform, Vector3 position);
    }
}

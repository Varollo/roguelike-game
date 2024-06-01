using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class SetTransformTileComponent : TransformTileComponent
    {
        public SetTransformTileComponent(BaseTile parentTile, Transform transform) : base(parentTile, transform)
        {
        }

        protected override void MoveTransform(Transform transform, Vector3 position)
        {
            transform.position = position;
        }
    }
}

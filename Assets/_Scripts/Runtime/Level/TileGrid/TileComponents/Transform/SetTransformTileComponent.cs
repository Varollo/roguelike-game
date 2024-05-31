using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class SetTransformTileComponent : TransformTileComponent
    {
        public SetTransformTileComponent(Transform transform) : base(transform)
        {
        }

        protected override void MoveTransform(Transform transform, Vector3 position)
        {
            transform.position = position;
        }
    }
}

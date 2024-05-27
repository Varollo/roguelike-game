using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public interface ITileTransformMover
    {
        void MoveTransform(Transform transform, Vector3 position);
    }
}

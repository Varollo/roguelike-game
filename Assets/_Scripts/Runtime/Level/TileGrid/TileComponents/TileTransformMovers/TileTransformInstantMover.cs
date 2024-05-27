using System;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileTransformInstantMover : ITileTransformMover
    {
        public void MoveTransform(Transform transform, Vector3 position)
        {
            if (transform != null)
                transform.position = position;
        }
    }
}

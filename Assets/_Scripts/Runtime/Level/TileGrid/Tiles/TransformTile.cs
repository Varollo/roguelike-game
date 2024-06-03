using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TransformTile : BaseTile
    {
        public TransformTile(Transform tileTransform, params ITileComponent[] components) : base(tileTransform.position.ToVec2Int(), components)
        {
            TransformComponent.SetTransform(tileTransform);
            CameraTile.OnCameraMoveEvent += CheckIfOffScreen;
        }

        ~TransformTile() => CameraTile.OnCameraMoveEvent -= CheckIfOffScreen;

        protected virtual TransformTileComponent TransformComponent { get; set; }

        protected virtual TransformTileComponent CreateTransformComponent()
        {
            return new TweenTransformTileComponent(this);
        }

        protected override List<ITileComponent> GetDefaultComponents() => new(base.GetDefaultComponents())
        {
            (TransformComponent = CreateTransformComponent())
        };

        private void CheckIfOffScreen(Vector2Int cameraPos)
        {
            const int CAMERA_SIZE_CHECK = 8;

            TransformComponent.SetObjectActive(Enabled =
                Mathf.Abs(cameraPos.x - Position.x) < CAMERA_SIZE_CHECK
                && Mathf.Abs(cameraPos.y - Position.y) < CAMERA_SIZE_CHECK);
        }
    }
}

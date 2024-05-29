using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TransformTile : MovableTile
    {
        public TransformTile(Transform tileTransform, params ITileComponent[] components) : base(tileTransform.position.ToVec2Int(), components)
        {
            SetTransform(tileTransform);
        }

        protected Transform TileTransform { get; private set; }

        public void SetTransform(Transform tileTransform)
        {
            TileTransformComponent transCom = GetComponent<TileTransformComponent>();

            if (transCom != null)
            {
                TileTransform = tileTransform;
                transCom.SetTransform(TileTransform);
            }
        }

        protected override IEnumerable<ITileComponent> SetupComponents() => CombineComponents(
            base.SetupComponents(), 
            CreateTransformComponent(CreateTransformMover())
        );

        protected virtual TileTransformComponent CreateTransformComponent(ITileTransformMover mover)
        {
            return new(TileTransform, mover);
        }

        protected virtual ITileTransformMover CreateTransformMover()
        {
            return new TileTransformInstantMover();
        }
    }
}

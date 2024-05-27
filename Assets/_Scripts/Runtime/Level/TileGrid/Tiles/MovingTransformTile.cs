using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class MovingTransformTile : MovingTile
    {
        public MovingTransformTile(Transform tileTransform, params ITileComponent[] components) : base(tileTransform.position.ToVec2Int(), components)
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
            CreateTransformComponent()
        );

        protected virtual TileTransformComponent CreateTransformComponent() => new(TileTransform);
    }
}

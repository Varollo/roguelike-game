using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TransformTile : BaseTile
    {
        public TransformTile(Transform tileTransform, params ITileComponent[] components) : base(tileTransform.position.ToVec2Int(), components)
        {
            TransformComponent.SetTransform(tileTransform);
        }

        protected virtual TransformTileComponent TransformComponent { get; set; }

        protected virtual TransformTileComponent CreateTransformComponent()
        {
            return new TweenTransformTileComponent(this);
        }

        protected override List<ITileComponent> GetDefaultComponents() => new(base.GetDefaultComponents())
        {
            (TransformComponent = CreateTransformComponent())
        };
    }
}

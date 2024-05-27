using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class MovingTransformTile : MovingTile
    {
        private readonly Transform _tileTransform;

        public MovingTransformTile(Transform tileTransform, params ITileComponent[] components) : base(tileTransform.position.ToVec2Int(), components)
        {
            var transCom = GetComponent<TileTransformComponent>();
            if (transCom != null)
            {
                _tileTransform = tileTransform;
                transCom.SetTransform(_tileTransform);
            }
        }

        protected override IEnumerable<ITileComponent> SetupComponents() => base.SetupComponents().Concat(new List<ITileComponent>()
        {
            new TileTransformComponent(_tileTransform)
        });
    }
}

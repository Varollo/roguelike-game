using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class MovableTile : TurnBasedTile
    {
        public MovableTile(Transform tileTransform, params ITileComponent[] components) : base(tileTransform, components)
        {
        }

        protected virtual MoveTileComponent MoveTileComponent { get; set; }

        protected override List<ITileComponent> GetDefaultComponents() => new(base.GetDefaultComponents())
        {
            (MoveTileComponent = CreateMoveTileComponent()),
        };

        protected abstract MoveTileComponent CreateMoveTileComponent();
    }
}

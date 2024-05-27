using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class MovingTile : Tile
    {
        public MovingTile(Vector2Int tilePosition = default, params ITileComponent[] components) : base(tilePosition, components)
        {
        }

        protected virtual Vector2Int GetMove(Vector2Int newPos)
        {
            return GetComponent<TileMotorComponent>().GetMove(newPos);
        }

        public override void SetPosition(Vector2Int value)
        {
            Vector2Int bestPos = Position + GetMove(value);
            base.SetPosition(bestPos);

        }

        protected override IEnumerable<ITileComponent> SetupComponents() => CombineComponents(
            base.SetupComponents(),
            CreateMotorComponent()
        );

        protected virtual TileMotorComponent CreateMotorComponent() => new();
    }
}

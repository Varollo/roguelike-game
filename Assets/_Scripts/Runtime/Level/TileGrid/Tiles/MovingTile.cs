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
            return Position + GetComponent<TileMotorComponent>().GetMove(newPos);
        }

        public override void OnPositionMove(Vector2Int newPos)
        {
            Vector2Int move = GetMove(newPos);

            if (move != newPos)
                SetPosition(move);

            else
                base.OnPositionMove(move);
        }

        protected override IEnumerable<ITileComponent> SetupComponents() => new ITileComponent[]
        {
            new TileMotorComponent(),
        };
    }
}
